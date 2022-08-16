//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 04-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //# Public Variables 
    public static GameManager Instance { set; get; }
    public List<MonsterData> MonsterLibrary;
    public List<Action> ActionLibrary;
    public UserProfile user;
    public List<MonsterData> VISUALISERMonstersInBag;
    public List<MonsterData> VISUALISERMonstersInBox;

    //# Private Variables 

    //# Monobehaviour Events 
    private void Awake()
    {
        //> Singleton-Setup
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
        //user = new UserProfile("Test User");
        user = new UserProfile(new List<MonsterData>(), new List<MonsterData>(MonsterLibrary));    //< For this version of the game, the player can have access to all implemented monsters.
        Debug.Log($"GameManager.Start: Your name is \"{user.name}\" and you currently have {user.MonstersInBox.Count} monster{(user.MonstersInBox.Count == 1 ? "" : "s")} in your box.");

        //> Debug Visualisation
        VISUALISERMonstersInBag = user.MonstersInBag;
        VISUALISERMonstersInBox = user.MonstersInBox;
    }

    //# Public Methods 
    public Action GetActionByLibraryIndex(int index)    //< Is not really the index, rather the "number" of the action, as index 0 is empty.
    {
        if (ActionLibrary[index] != null)
            return ActionLibrary[index];  //< All attacks mirror the indexes of their official counterpart on https://bulbapedia.bulbagarden.net/wiki/List_of_moves
        else
            Debug.LogWarning($"GameManager.GetActionAtLibraryIndex: There is no action at library index {index}. This result might be expected, for example when one of a monster's action fields is empty.");

        return null;
    }

    public int GetLibraryIndexOfAction(Action action)
    {
        return ActionLibrary.IndexOf(action);
    }

    public MonsterData GetMonsterByLibraryIndex(int index)
    {
        if (MonsterLibrary[index] == null || index + 1 > MonsterLibrary.Count)
            Debug.LogWarning($"GameManager.GetMonsterByLibraryIndex: There is no monster at library index {index}.");
        else
            return MonsterLibrary[index];

        return null;
    }

    public int GetLibraryIndexOfMonster (MonsterData monsterData)
    {
        return MonsterLibrary.IndexOf(monsterData);
    }

    public static void QueueFlavourText(string newText, Object sender)
    {
        Debug.Log($"<color=#F7C8A1>Flavour:</color> {newText}", sender);
        FlavourTextHandler.Instance.QueueText($"{newText}");
    }

    //# Input Event Handlers 
}