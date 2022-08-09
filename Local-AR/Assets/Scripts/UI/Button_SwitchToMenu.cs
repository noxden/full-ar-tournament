//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 30-07-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_SwitchToMenu : MonoBehaviour
{
    //# Public Variables 
    public MenuName newMenu;

    //# Private Variables

    //# Monobehaviour Events 

    //# Public Methods 

    //# Private Methods 

    //# Input Event Handlers 
    public void OnButtonPressed()
    {
        MenuHandler.Instance.SwitchToMenu(newMenu);
    }
}
