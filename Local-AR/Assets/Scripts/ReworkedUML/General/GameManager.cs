//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 30-06-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //# Public Variables 
    public static GameManager Instance { set; get; }
    public List<Monster> AllMonsters;
    public UserProfile user;
    public List<Monster> VISUALISERMonstersInBag;
    public List<Monster> VISUALISERMonstersInBox;

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
        user = new UserProfile(new List<Monster>(), new List<Monster>(AllMonsters));    //< For this version of the game, the player can have access to all implemented monsters.
        Debug.Log($"Your name is \"{user.name}\" and you are currently carrying {user.NumberOfMonstersInBag} monsters.");

        //> Debug Visualisation
        VISUALISERMonstersInBag = user.MonstersInBag;
        VISUALISERMonstersInBox = user.MonstersInBox;
    }

    //# Public Methods 

    //# Private Methods 

    //# Input Event Handlers 
}