//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 31-07-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    //# Tutorial Flags 
    public static bool didShowTutorial
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsKey.didShowTutorial, 0) == 1;  //< "PlayerPrefsKey.didShowTutorial" is just a String variable (see line 49)
        }
        set
        {
            if (value == true)
                PlayerPrefs.SetInt(PlayerPrefsKey.didShowTutorial, 1);
            else
                PlayerPrefs.SetInt(PlayerPrefsKey.didShowTutorial, 0);
        }
    }
    //> Not needed anymore, but as it is a function without any parameters, which might be useful if need to be triggered by a button?
    // public static void SetTutorialAsShown()      
    // {
    //     PlayerPrefs.SetInt(PlayerPrefsKey.didShowTutorial, 1);
    // }

    //# CameraPermission Flags 
    public static bool didTriggerCameraPermissionRequest
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsKey.didTriggerCameraPermissionRequest, 0) == 1;
        }
        set
        {
            if (value == true)
                PlayerPrefs.SetInt(PlayerPrefsKey.didTriggerCameraPermissionRequest, 1);
            else
                PlayerPrefs.SetInt(PlayerPrefsKey.didTriggerCameraPermissionRequest, 0);
        }
    }
    // public static void SetCameraPermissionRequestAsTriggered()
    // {
    //     PlayerPrefs.SetInt(PlayerPrefsKey.didTriggerCameraPermissionRequest, 1);
    // }

    //# Username 
    public static string localUsername
    {
        get
        {
            return PlayerPrefs.GetString(PlayerPrefsKey.localUsername, "Default Name");
        }
        set
        {
            PlayerPrefs.SetString(PlayerPrefsKey.localUsername, value);
        }
    }

    //# Username 
    public static string monstersInBag
    {
        get
        {
            return PlayerPrefs.GetString(PlayerPrefsKey.monstersInBag, "");
        }
        set
        {
            PlayerPrefs.SetString(PlayerPrefsKey.monstersInBag, value);
        }
    }

    //* Additional info:
    // PlayerPrefs supports the following functions:
    // PlayerPrefs.SetInt()
    // PlayerPrefs.SetFloat()
    // PlayerPrefs.SetString()
}

public class PlayerPrefsKey
{
    public static string didShowTutorial = "didShowTutorial";
    public static string didTriggerCameraPermissionRequest = "didTriggerCameraPermissionRequest";
    public static string localUsername = "localUsername";
    public static string monstersInBag = "monstersInBag";
}
