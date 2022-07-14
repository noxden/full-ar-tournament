//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 24-06-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//> Takes care of tracking turns and telling the players when it's their turn
public class CombatHandler : MonoBehaviour
{
    public static CombatHandler Instance { set; get; }

    //# Public Variables 
    public Player PlayerA;
    public Player PlayerB;
    public int turn;

    //# Private Variables 
    private Monster MonsterA;
    private Monster MonsterB;

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
        SwapMonsterOnField('A', PlayerA.bag.MonstersInBag[0]);
        SwapMonsterOnField('B', PlayerB.bag.MonstersInBag[0]);
    }

    //# Public Methods 
    public Player GetEnemyData(Player requestingPlayer)  //> Returns the opponents Player data, depending on input Player data
    {
        if (PlayerA == null || PlayerB == null)  // Guard-clause
        {
            Debug.LogError($"CombatHandler.GetEnemyData: Participating players are not set correctly. ERROR_CH01");
            return null;
        }

        if (requestingPlayer == PlayerA)
            return PlayerB;
        else if (requestingPlayer == PlayerB)
            return PlayerA;
        else
            Debug.LogError($"CombatHandler.GetEnemyData: Requesting player \"{requestingPlayer.userName}\" is invalid. ERROR_CH02");
        return null;
    }

    public void SwapMonsterOnField(char participant, Monster newMonster)  //< This method is very messy
    {
        Player player;
        switch (participant)
        {
            case 'A':
                player = PlayerA;
                if (MonsterA != null)
                    MonsterA.isOnField = false;
                MonsterA = newMonster;
                break;
            case 'B':
                player = PlayerB;
                if (MonsterA != null)
                    MonsterA.isOnField = true;
                MonsterB = newMonster;
                break;
            default:
                player = null;
                Debug.LogError($"CombatHandler.PutMonsterOnField: Player{participant} does not exist, please choose 'A' or 'B'. ERROR_CH03");
                break;
        }
        newMonster.isOnField = true;
        Debug.Log($"{player.userName} sent out {newMonster.GetName()}!");
    }

    public void NewTurn()
    {

    }
}
