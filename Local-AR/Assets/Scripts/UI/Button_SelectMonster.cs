//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 30-06-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_SelectMonster : MonoBehaviour
{
    //# Public Variables 
    public MonsterData monsterData;
    public bool newBagState;

    //# Private Variables

    //# Monobehaviour Events 

    //# Public Methods 

    //# Private Methods 

    //# Input Event Handlers 
    public void OnButtonPressed()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError($"Could not find GameManager in scene. ERROR_BTN1", this);
            return;
        }

        GameManager.Instance.user.ChangeIsInBag(monsterData, newBagState);
    }
}
