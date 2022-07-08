//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 24-06-22
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
                if (MonstersInBag.Count >= 6)
                {
                    Debug.Log($"You are already carrying 6 Monsters, your bag is full!");
                    return;
                }
                else
                {
                    Monster targetInBox = MonstersInBox[MonstersInBox.IndexOf(target)];
                    if (targetInBox = null)
                    {
                        Debug.LogError($"The monster selected is not present in your box anymore.\nERROR-B01");
                        return;
                    }
                    MonstersInBag.Add(targetInBox);
                    MonstersInBox.Remove(targetInBox);
                }
                break;
            case false: // Add to Box and remove from Bag
                    Monster targetInBag = MonstersInBag[MonstersInBag.IndexOf(target)];
                    if (targetInBag = null)
                    {
                        Debug.LogError($"The monster selected is not present in your bag anymore.\nERROR-B02");
                        return;
                    }
                    MonstersInBox.Add(targetInBag);
                    MonstersInBag.Remove(targetInBag);
                break;
        }
        target.isInBag = bagState;
    }
}
