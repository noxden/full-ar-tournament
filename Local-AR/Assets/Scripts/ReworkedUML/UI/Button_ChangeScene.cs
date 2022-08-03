//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 30-07-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_ChangeScene : MonoBehaviour
{
    //# Public Variables 
    public Scene sceneToLoad;

    //# Input Event Handlers 
    public void OnButtonPressed()
    {
        GameManager.Instance.BeforeSceneChange();   //! Should not be in the button but in the SceneTransitionManager instead. Otherwise, scene changes called by script will not run BeforeSceneChange()!
        SceneTransitionManager.LoadScene(sceneToLoad);
    }
}
