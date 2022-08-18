//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 18-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Forfeit : MonoBehaviour
{
    //# Input Event Handlers 
    public void OnButtonPressed()
    {
        if (!CombatHandler.Instance.hasGameEnded)   //< You can only forfeit if the game has not ended yet.
        {
            GameManager.QueueFlavourText($"You have given up.", this);
            WebSocketConnection.Instance.CreateLeavePackage();
            CombatHandler.Instance.FinishGame(false);
        }
    }
}
