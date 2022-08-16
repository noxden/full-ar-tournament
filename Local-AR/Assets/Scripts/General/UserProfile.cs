//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 16-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UserProfile
{
    //# Public Variables 
    public static Delegate MonsterBagStateChanged;
    public int UUID { get; private set; }
    public string name = SaveDataManager.localUsername;
    public int numberOfMonstersInBag { get { return MonstersInBag.Count; } }

    //# Private Variables 
    public List<MonsterData> MonstersInBag { get; private set; }
    public List<MonsterData> MonstersInBox { get; private set; }

    //# Constructors 
    public UserProfile()
    {
        GenerateUUID();
        MonstersInBag = new List<MonsterData>();
        MonstersInBox = new List<MonsterData>();
    }

    public UserProfile(string _name)
    {
        GenerateUUID();
        name = _name;
        MonstersInBag = new List<MonsterData>();
        MonstersInBox = new List<MonsterData>();
    }

    public UserProfile(List<MonsterData> _MonstersInBag, List<MonsterData> _MonstersInBox)
    {
        GenerateUUID();
        MonstersInBag = _MonstersInBag;
        MonstersInBox = _MonstersInBox;
    }

    public UserProfile(string _name, List<MonsterData> _MonstersInBag, List<MonsterData> _MonstersInBox)
    {
        GenerateUUID();
        name = _name;
        MonstersInBag = _MonstersInBag;
        MonstersInBox = _MonstersInBox;
    }

    //# Public Methods 
    public void ChangeIsInBag(MonsterData target, bool bagState)
    {
        switch (bagState)
        {
            //> Add to Bag and remove from Box
            case true:
                if (MonstersInBag.Count >= GlobalSettings.maxMonstersInBag)   //< Guard clause
                {
                    Debug.LogWarning($"UserProfile: You are already carrying {GlobalSettings.maxMonstersInBag} Monsters, your bag is full!");
                    break;
                }

                if (MonstersInBox.Contains(target))
                {
                    MonstersInBag.Add(target);
                    MonstersInBox.Remove(target);
                    Debug.Log($"UserProfile: Successfully moved \"{target.name}\" from box to bag.");
                    MonsterBagStateChanged();
                }
                else
                {
                    Debug.LogError($"UserProfile: The monster selected is not present in your box anymore. ERROR_UP1");
                }
                break;

            //> Add to Box and remove from Bag
            case false:
                if (MonstersInBag.Contains(target))    //< Guard clause
                {
                    MonstersInBox.Add(target);
                    MonstersInBag.Remove(target);
                    Debug.Log($"UserProfile: Successfully moved \"{target.name}\" from bag to box.");
                    MonsterBagStateChanged();
                    break;
                }
                else
                {
                    Debug.LogError($"UserProfile: The monster selected is not present in your bag anymore. ERROR_UP2");
                    break;
                }

        }
    }

    public void ChangeUsername(string newName)
    {
        name = newName;
        SaveDataManager.localUsername = newName;
        Debug.Log($"UserProfile: Changed username to \"{newName}\"!");
    }

    //# Private Methods 
    private void GenerateUUID()
    {
        UUID = Random.Range(0, 1000000);
        Debug.Log($"UserProfile: Your UUID for this session is {UUID}.");
    }

    //# Input Event Handlers 
}
