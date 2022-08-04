//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 03-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatHandler : MonoBehaviour
{
    //# Public Variables 
    public static CombatHandler Instance { set; get; }
    public GameObject playerPrefab;

    //# Private Variables 
    [SerializeField] private Player you;
    [SerializeField] private Player enemy;
    [SerializeField] private Action yourAction;
    [SerializeField] private float yourActionTieBreaker;
    [SerializeField] private Action enemyAction;
    [SerializeField] private float enemyActionTieBreaker;
    [SerializeField] private int turn = 1;

    //# Monobehaviour Events 
    private void Awake()
    {
        if (Instance == null)   //< With this if-structure it is IMPOSSIBLE to create more than one instance.
            Instance = this;
        else
            Destroy(this.gameObject);   //< If you somehow still get to create a new singleton gameobject regardless, destroy the new one.
    }

    private void Start()
    {
        UserProfile user = GameManager.Instance.user;

        enemy = InstantiatePlayer("Enemy Player");
        you = InstantiatePlayer("Your Player");

        //> Initialize your player based on values from your user (instance), which may have been modified in the menu.
        you.Set(user.name, user.MonstersInBag);

        //TODO: Create JoinPackage containing own Player (you)
    }

    //# Public Methods 
    public void SelectActionAtIndex(int actionIndex)    //> Selects an action for "yourAction" -> is only to be used for your player, never for the enemy.
    {
        yourAction = GetActionAtIndex(you.GetMonsterOnField(), actionIndex);     //< Using this specific overload here is just for clarification purposes.
        if (yourAction != null)
            Debug.Log($"CombatHandler.SelectActionAtIndex: Your selected action is now {yourAction.name}.");

        yourActionTieBreaker = Random.Range(0.00001f, 0.99999f);     //< Is a randomly determined value to serve as a tie breaker if speeds would be otherwise equal.
        //Debug.Log($"CombatHandler.SelectActionAtIndex: Your SpeedTieBreaker is {yourActionTieBreaker}.");

        ResolveTurn();
    }

    public void SelectItemAction(Action action)     //! WIP, very similar to SelectActionAtIndex() and definitely improvable.
    {
        yourAction = action;
        yourActionTieBreaker = 999f;  //< Items should always be applied before any action.  
                                      //  If both players use an item in the same turn, there will be a brief order desync, but it should not cause any issues.
                                      //Debug.Log($"CombatHandler.SelectItemAction: Your SpeedTieBreaker is {yourActionTieBreaker}, because you used an item.");

        //TODO: Create CombatPackage containing Action(actionAtIndex) (and possible it's user & enemy?) here.
        ResolveTurn();
    }

    public Action GetActionOfMonsterOnFieldAtIndex(int actionIndex)  //< Used primarily for button labels.
    {
        return GetActionAtIndex(you.GetMonsterOnField(), actionIndex);
    }

    public Action GetActionAtIndex(Monster monster, int actionIndex)    //- Overload that can be used to get the action of a monster other than your current active one
    {
        if (monster == null)
        {
            Debug.LogError($"CombatHandler.GetActionAtIndex: The monster you want to get an action from is null. ERROR_CH1");
            return null;
        }
        if (monster.AvailableActions[actionIndex] == null)
        {
            Debug.Log($"CombatHandler.GetActionAtIndex: {monster} does not have an action in slot {actionIndex + 1}.");
            return null;
        }
        return monster.AvailableActions[actionIndex];
    }

    //# Private Methods 
    private Player InstantiatePlayer(string gameObjectName)
    {
        GameObject go = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        go.name = gameObjectName;   //< To make it easier to distinguish the players in the editor
        Player player = go.GetComponent<Player>();
        return player;
    }

    private void ResolveTurn()
    {
        if (yourAction == null || enemyAction == null)  //< Guard clause -> Unless both actions are filled, cancel this function
            return;

        //> Initial setup of turnOrder
        Monster yourMonster = you.GetMonsterOnField();
        Monster enemyMonster = enemy.GetMonsterOnField();
        List<Monster> turnOrder = new List<Monster>();
        turnOrder.Add(yourMonster);
        turnOrder.Add(enemyMonster);

        //> Define monster speeds for this turn (based on base monster speed, action speedBonus and the tiebreaker)
        float yourInitiative = yourMonster.speed + yourAction.speedBonus + yourActionTieBreaker;
        float enemyInitiative = enemyMonster.speed + enemyAction.speedBonus + enemyActionTieBreaker;
        Debug.Log($"CombatHandler.ResolveTurn: {yourMonster.GetName()}'s initiative is {yourInitiative}. {enemyMonster.GetName()}'s initiative is {enemyInitiative}.");

        if (yourInitiative == enemyInitiative)      //< Very, very improbable, but still possible.
            Debug.LogError($"CombatHandler.ResolveTurn: Both monster speeds are exactly equal, risk of desynchronization very high! ERROR_CH2");

        //> Sort turnOrder by those speeds
        if (enemyInitiative > yourInitiative)
            turnOrder.Reverse();    //< This implementation is not good, as it is not scalable at all, but it works for the scope of this concept.

        //> Execute actions in order of turnOrder
        foreach (Monster monster in turnOrder)
        {
            if (monster == yourMonster)
            {
                monster.UseAction(yourAction, yourMonster, enemyMonster);
                if (isDefeated(enemyMonster))    //< If due to the last action, the opponent is now invalid (e.g. has fainted), do not process their attack anymore.
                    break;                              //< Get out of the foreach loop.
            }
            else if (monster == enemyMonster)
            {
                monster.UseAction(enemyAction, enemyMonster, yourMonster);
                if (isDefeated(yourMonster))
                    break;
            }
        }

        //> Cleanup / reset global variables and continue to next turn
        Debug.Log($"<color=#00FFFF>CombatHandler.ResolveTurn: End of turn {turn}.</color>");
        yourAction = null;
        yourActionTieBreaker = 0f;
        enemyAction = null;
        enemyActionTieBreaker = 0f;
        turn += 1;
        //> Prepare next turn if any monsters fainted.
        if (!yourMonster.isValid())
        {
            if (you.GetFirstValidMonster() == null)
            {
                // You lost.
                return;
            }

            you.SwapMonsterOnField(you.GetFirstValidMonster());
        }
        if (!enemyMonster.isValid())
        {
            if (enemy.GetFirstValidMonster() == null)
            {
                // You won.
                return;
            }

            enemy.SwapMonsterOnField(enemy.GetFirstValidMonster());
        }
    }

    private bool isDefeated(Monster monster)
    {
        Debug.Log($"CombatHandler.isDefeated: {monster.GetName()} {(monster.isValid() ? "is still standing" : "faints")}.");
        return (!monster.isValid());
        }
    }

    }

    //# Input Event Handlers 
    public void OnPlayerDataReceived(Player playerData)
    {
        // TODO: Actually, nevermind. This should be preventable on the server side.
        // if (otherPlayer.username == You.username)  //< Guard clause to make sure that you didn't receive your own player data.
        //     return;

        //> Set enemy's player and monsterOnField as soon as that data is received.
        enemy.Set(playerData);
        enemy.monsterOnField = enemy.GetFirstValidMonster();

        MenuHandler menuHandler = FindObjectOfType<MenuHandler>();
        menuHandler.SwitchToMenu(MenuName.Combat_Menu);
    }

    public void OnActionDataReceived(Action actionData, float tieBreakerData)
    {
        Debug.Log($"CombatHandler.OnActionDataReceived: Received Action \"{actionData.name}\" and tieBreaker {tieBreakerData}.");
        enemyAction = actionData;
        enemyActionTieBreaker = tieBreakerData;
        ResolveTurn();
    }
}