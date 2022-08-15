//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 08-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Forfeit : MonoBehaviour
{
    public void OnButtonPressed()
    {
        WebSocketConnection.Instance.CreateLeavePackage();
        CombatHandler.Instance.FinishGame(false);
    }
}
