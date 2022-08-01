//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 31-07-22
// TODO: Implement automatically overwriting the button text with the abiliy's name
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_SelectAction : MonoBehaviour
{
    //# Public Variables 
    public int actionNumber;

    //# Private Variables 

    //# Monobehaviour Events 

    //# Input Event Handlers 
    public void OnButtonPressed()
    {
        if (CombatHandler.Instance == null)
        {
            Debug.LogError($"Could not find CombatHandler in scene. ERROR_BTN2", this);
            return;
        }

            CombatHandler.Instance.SelectActionAtIndex(actionNumber-1);
    }
}
