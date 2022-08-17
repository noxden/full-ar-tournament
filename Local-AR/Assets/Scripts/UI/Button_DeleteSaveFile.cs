//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 16-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_DeleteSaveFile : MonoBehaviour
{
    //# Input Event Handlers 
    public void OnButtonPressed()
    {
        SaveDataManager.DeleteSaveFile();
        Debug.Log($"Button_DeleteSaveFile: Shutting down game for your changes to take effect, please restart.");
        Application.Quit();
    }
}
