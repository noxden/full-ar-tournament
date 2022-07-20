//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 14-07-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    //# Public Variables 
    public List<Monster> MonstersInBag; // maybe { get; private set; }
    public List<Monster> MonstersInBox;
    public List<Item> Items;

    //# Public Methods 
    public void ChangeIsInBag(Monster target, bool bagState)
    {
        switch (bagState)
        {
            case true: // Add to Bag and remove from Box
                if (MonstersInBag.Count >= 6)   //< Guard clause
                {
                    Debug.LogWarning($"You are already carrying 6 Monsters, your bag is full!");
                    break;
                }

                // TODO: Still needs to be tested, if this shorter version works
                if (MonstersInBox.Contains(target))
                {
                    MonstersInBag.Add(target);
                    MonstersInBox.Remove(target);
                }
                else
                {
                    Debug.LogError($"The monster selected is not present in your box anymore.\nERROR-B01", this);
                }
                

                // Monster targetInBox = MonstersInBox[MonstersInBox.IndexOf(target)];
                // if (targetInBox == null)
                // {
                //     Debug.LogError($"The monster selected is not present in your box anymore.\nERROR-B01");
                //     return;
                // }
                // MonstersInBag.Add(targetInBox);
                // MonstersInBox.Remove(targetInBox);
                break;

            case false: // Add to Box and remove from Bag
                Monster targetInBag = MonstersInBag[MonstersInBag.IndexOf(target)];
                if (targetInBag == null)
                {
                    Debug.LogError($"The monster selected is not present in your bag anymore. ERROR_B02", this);
                    return;
                }
                MonstersInBox.Add(targetInBag);
                MonstersInBag.Remove(targetInBag);
                break;
        }
    }

    public Monster GetFirstValidMonster()
    {
        foreach (Monster monster in MonstersInBag)
        {
            if (monster.hp_current > 0)
                return monster;
        }
        Debug.LogError($"Cannot get any valid monster. ERROR_B03", this);
        return null;
    }
}
