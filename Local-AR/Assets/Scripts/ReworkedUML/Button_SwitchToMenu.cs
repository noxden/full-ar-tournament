//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 30-07-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum MenuName { Welcome, MainMenu, ChangeMonsters, Lobby, Credits }

public class Button_SwitchToMenu : MonoBehaviour
{
    //# Public Variables 
    public int newMenuIndex;

    //# Private Variables
    private MenuHandler menuHandler;

    //# Monobehaviour Events 
    private void Awake()
    {
        menuHandler = FindObjectOfType<MenuHandler>();
    }

    //# Public Methods 

    //# Private Methods 

    //# Input Event Handlers 
    public void OnButtonPressed()
    {
        menuHandler.SwitchToMenu(newMenuIndex);
    }
}
