//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 01-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CombatHandler : MonoBehaviour
{
    //# Public Variables 
    public static CombatHandler Instance { set; get; }
    public GameObject playerPrefab;

    //# Private Variables 
    [SerializeField] private Player you;
    [SerializeField] private Player enemy;
    [SerializeField] private Action yourAction;
    [SerializeField] private Action enemyAction;
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
        you.username = user.name;
        you.Monsters = user.MonstersInBag;
        you.monsterOnField = you.GetFirstValidMonster();

        //TODO: Create JoinPackage containing own Player (you)
    }

    //# Public Methods 
    public void SelectActionAtIndex(int actionIndex)    //> Selects an action for "yourAction" -> is only to be used for your player, never for the enemy.
    {
        if (you.monsterOnField == null)
        {
            Debug.LogError($"CombatHandler: You do not have a monster on field. ERROR_CH1");
            return;
        }

        yourAction = GetActionAtIndex(you.monsterOnField, actionIndex);     //< Using this specific overload here is just for clarification purposes.
        if (yourAction != null)
            Debug.Log($"CombatHandler: Your selected action is now {yourAction.name}.");

        //TODO: Create CombatPackage containing Action(actionAtIndex) (and possible it's user & enemy?) here.
        ResolveTurn();
    }

    //> Used primarily for button labels.
    public Action GetActionAtIndex(int actionIndex)
    {
        return GetActionAtIndex(you.monsterOnField, actionIndex);
    }

    public Action GetActionAtIndex(Monster monster, int actionIndex)    //- Overload that can be used to get the action of a monster other than your current active one
    {
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
        Monster yourMonster = you.monsterOnField;
        Monster enemyMonster = enemy.monsterOnField;
        List<Monster> turnOrder = new List<Monster>();
        turnOrder.Add(yourMonster);
        turnOrder.Add(enemyMonster);

        //> Check monster speeds
        yourMonster.speed += yourAction.speedBonus;
        enemyMonster.speed += enemyAction.speedBonus;
        turnOrder.Sort((x, y) => x.speed.CompareTo(y.speed));   //< See https://stackoverflow.com/questions/3309188/how-to-sort-a-listt-by-a-property-in-the-object
        //! Doesn't sort them properly.
        //TODO: FIX if both speeds are equal, then both clients show different orders, because they both add themselves first
        yourMonster.speed -= yourAction.speedBonus;     //< Remove temporary speed increases again
        enemyMonster.speed -= enemyAction.speedBonus;

        //> Execute actions in order of turnOrder
        foreach (Monster monster in turnOrder)  //TODO: Here I need to implement the check if they are still alive before they attack, otherwise a just downed monster can still attack!
        {
            if (monster == yourMonster)         //? looks scuffed, maybe put all of this in another foreach loop?
                monster.UseAction(yourAction, yourMonster, enemyMonster);
            else if (monster == enemyMonster)
                monster.UseAction(enemyAction, enemyMonster, yourMonster);
        }

        //> Cleanup and continue to next turn
        Debug.Log($"CombatHandler: End of turn {turn}!");
        yourAction = null;
        enemyAction = null;
        turn += 1;
    }

    //# Input Event Handlers 
    public void OnPlayerDataReceived(Player playerData)
    {
        // if (otherPlayer.username == You.username)  //< Guard clause to make sure that you didn't receive your own player data <- Actually, nevermind. This should be preventable on the server side.
        //     return;

        //> Set enemy's player and monsterOnField as soon as that data is received.
        enemy.Set(playerData);
        enemy.monsterOnField = enemy.GetFirstValidMonster();

        MenuHandler menuHandler = FindObjectOfType<MenuHandler>();
        menuHandler.SwitchToMenu(MenuName.Combat_Menu);
    }

    public void OnActionDataReceived(Action actionData)
    {
        Debug.Log($"CombatHandler: Received Action \"{actionData.name}\".");
        enemyAction = actionData;
        ResolveTurn();
    }
}