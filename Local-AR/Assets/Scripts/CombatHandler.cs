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

    //# Public Methods 
    public Player GetEnemyData(Player requestingPlayer)
    {
        // Todo: Return different player, depending on who asked.
        return PlayerA;
    }

    public void newTurn()
    {
        MonsterA = PlayerA.GetMonsterIsOnField();
        MonsterB = PlayerB.GetMonsterIsOnField();
    }
}
