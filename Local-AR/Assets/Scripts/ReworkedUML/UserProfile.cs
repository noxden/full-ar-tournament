//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 30-06-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UserProfile
{
    //# Constructors 
    public UserProfile()
    {
        name = "Default Name";
        MonstersInBag = new List<Monster>();
        MonstersInBox = new List<Monster>();
    }

    public UserProfile(string _name)
    {
        name = _name;
        MonstersInBag = new List<Monster>();
        MonstersInBox = new List<Monster>();
    }

    public UserProfile(string _name, List<Monster> _MonstersInBag, List<Monster> _MonstersInBox)
    {
        name = _name;
        MonstersInBag = _MonstersInBag;
        MonstersInBox = _MonstersInBox;
    }

    //# Public Variables 
    public string name;
    public int NumberOfMonstersInBag { get { return MonstersInBag.Count; } }

    //# Private Variables 
    private List<Monster> MonstersInBag;
    private List<Monster> MonstersInBox;

    //# Public Methods 
    public void ChangeIsInBag(Monster target, bool bagState)
    {
        switch (bagState)
        {
            //> Add to Bag and remove from Box
            case true:
                if (MonstersInBag.Count >= GlobalSettings.maxMonstersInBag)   //< Guard clause
                {
                    Debug.LogWarning($"You are already carrying {GlobalSettings.maxMonstersInBag} Monsters, your bag is full!");
                    break;
                }

                if (MonstersInBox.Contains(target))
                {
                    MonstersInBag.Add(target);
                    MonstersInBox.Remove(target);
                    Debug.Log($"Successfully moved \"{target.GetName()}\" from box to bag.");
                }
                else
                {
                    Debug.LogError($"The monster selected is not present in your box anymore. ERROR_UP1");
                }
                break;

            //> Add to Box and remove from Bag
            case false:
                Monster targetInBag = MonstersInBag.Find(m => m.Equals(target));    //< See Predicate documentation: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.find?view=net-6.0
                Monster targetInBag = MonstersInBag.Find(m => m.Equals(target));    //! This has not been tested yet.
                if (targetInBag == null)    //< Guard clause
                {
                    Debug.LogError($"The monster selected is not present in your bag anymore. ERROR_UP2");
                    break;
                }
                MonstersInBox.Add(targetInBag);
                MonstersInBag.Remove(targetInBag);
                Debug.Log($"Successfully moved \"{target.GetName()}\" from bag to box.");
                break;
        }
    }

    //# Private Methods 

    //# Input Event Handlers 
}
