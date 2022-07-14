//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 14-07-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bag))]
public class Player : MonoBehaviour
{
    //# Public Variables 
    public string username;
    public Bag bag;
    public Monster monsterOnField = null;   //< Only set this via SwapMonsterOnField()

    //public List<Player> Friends;

    //# Private Variables 

    //# Public Methods 
    public void SwapMonsterOnField(Monster newMonster)
    {
        if (monsterOnField == null)  //< Should only be the case when the battle just started or the former monsterOnField died
            Debug.Log($"{username} sent out {newMonster.GetName()}!"); 
        else
            Debug.Log($"{username} swapped out {monsterOnField.GetName()} with {newMonster.GetName()}!");

        monsterOnField = newMonster;
    }

    public int GetNumberOfMonstersInBag()
    {
        return bag.MonstersInBag.Count;
    }
}
