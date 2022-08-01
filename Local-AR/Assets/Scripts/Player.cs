//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 31-07-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //# Constructors
    public void Set(Player player)
    {
        this.username = player.username;
        this.Monsters = new List<Monster>(player.Monsters);
        this.monsterOnField = player.monsterOnField;
    }

    //# Public Variables 
    public string username;
    public List<Monster> Monsters;
    public int NumberOfMonsters { get { return Monsters.Count; } }
    public Monster monsterOnField; /*{ private set; get; }*/   //< Set with SwapMonsterOnField()

    //# Private Variables 

    //# Public Methods 
    public void SwapMonsterOnField(Monster newMonster)  //< Is used instead of standard set() to provide an interface for Monster's OnSwapped events, maybe?
    {
        if (monsterOnField == null)  //< Should only be the case when the battle just started or the former monsterOnField died
            Debug.Log($"{username} sent out {newMonster.GetName()}!");
        else
            Debug.Log($"{username} swapped out {monsterOnField.GetName()} with {newMonster.GetName()}!");

        monsterOnField = newMonster;
    }

    public Monster GetFirstValidMonster()
    {
        foreach (Monster monster in Monsters)
        {
            if (monster.hpCurrent > 0)
                return monster;
        }
        Debug.Log($"Player.GetFirstValidMonster: Cannot get any valid monster.", this);    //< Usually, if this happens, the player has lost / should lose the match.
        return null;
    }
}
