//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 24-06-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    //# Public Variables 
    public string userName;
    public List<Player> Friends;
    public Bag bag;
    //public int NumberOfMonstersInBag;   //< Unsure if I want this information here, but this way, other players don't have to read the entire enemy bag

    //# Public Methods 
    public Monster GetMonsterIsOnField()
    {
        List<Monster> monsterBag = bag.MonstersInBag;
        foreach (Monster monster in monsterBag)
        {
            if (monster.isOnField)
                return monster;
        }
        Debug.LogError($"Player.GetMonsterIsOnField: There is no monster on the field.");
        return null;
    }

}
