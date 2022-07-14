//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 14-07-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//> Takes care of tracking turns and telling the players when it's their turn
public class CombatHandler : MonoBehaviour
{
    public static CombatHandler Instance { set; get; }

    //# Public Variables 

    //# Private Variables 
    [SerializeField] private List<Player> PlayersInCombat;
    [SerializeField] private List<Monster> MonstersInCombat;
    private int turn;

    //# Monobehaviour Events 
    private void Awake()
    {
        if (Instance == null)   //< With this if-structure it is IMPOSSIBLE to create more than one instance.
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); //< Referring to the gameObject, this singleton script (class) is attached to.
        }
        else
        {
            Destroy(this.gameObject);   //< If you somehow still get to create a new singleton gameobject regardless, destroy the new one.
        }
    }

    private void Start()
    {
        Player[] PlayersInScene = FindObjectsOfType<Player>();

        if (PlayersInScene.Length > 2)
        {
            Debug.LogError($"There are more than 2 Players in the scene. It is recommended to remove all but 2 players from the scene.", this);
            return;
        }
        else if (PlayersInScene.Length < 2)
        {
            Debug.LogError($"There are less than 2 Players in the scene. Please add more players to the scene and restart. ERROR_CH03", this);
            return;
        }
        else
            Debug.Log($"There are 2 Players in the scene. Initiating combat between {PlayersInScene[0].username} and {PlayersInScene[1].username}.", this);

        //> By this point, it is guaranteed that we have exactly two players
        foreach (Player player in FindObjectsOfType<Player>())
        {
            PlayersInCombat.Add(player);
            MonstersInCombat.Add(player.bag.GetFirstValidMonster());

            if (PlayersInCombat.IndexOf(player) != MonstersInCombat.IndexOf(player.bag.GetFirstValidMonster()))
                Debug.LogError($"Player and monster indeces do not match (which is bad)! ERROR_CH04", this);
            else
                player.SwapMonsterOnField(player.bag.GetFirstValidMonster());
        }
    }

    //# Public Methods 
    public Player GetEnemyData(Player requestingPlayer)  //> Returns the opponents Player data, depending on input Player data
    {
        if (PlayersInCombat[0] == null || PlayersInCombat[1] == null)  // Guard-clause
        {
            Debug.LogError($"CombatHandler.GetEnemyData: Participating players are not set correctly. ERROR_CH01", this);
            return null;
        }

        if (requestingPlayer == PlayersInCombat[0])
            return PlayersInCombat[1];
        else if (requestingPlayer == PlayersInCombat[1])
            return PlayersInCombat[0];
        else
            Debug.LogError($"Requesting player \"{requestingPlayer.username}\" is invalid. ERROR_CH02", this);
        return null;
    }

    public void NewTurn()
    {
        foreach (Monster monster in MonstersInCombat)
        {
            if (monster.hp_current <= 0)
            {
                Debug.Log($"{monster.owner.username} has to swap out their {monster.GetName()} as their HP dropped to 0.", this);
                // Prompt Player A to select new monster.
            }
        }
        turn += 1;
    }
}
