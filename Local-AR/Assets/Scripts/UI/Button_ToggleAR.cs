//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 05-08-22
// TODO: Maybe just solve the background canvas stuffs here?
// TODO: But that still wont account for the AR Button... I guess that could just be put on a non-CanvasMenu canvas... but then it'll always be visible, also during 
// TODO: loading and end screen... Unless those have their own backgrounds setup and are rendered on top...
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class Button_ToggleAR : MonoBehaviour
{
    //# Public Variables 
    public bool isAREnabled;
    public Camera NonARCamera;
    public Camera ARCamera;
    public GameObject ARSession;
    public GameObject nonARBackground;

    //# Private Variables
    private TextMeshProUGUI buttonText;

    //# Monobehaviour Events 
    private void Awake()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateComponents();
    }

    //# Public Methods 

    //# Private Methods 
    private void ToggleARMode()
    {
        isAREnabled = !isAREnabled;
        UpdateComponents();

        if (isAREnabled)
        {
            Debug.Log($"Button_ToggleAR.ToggleARMode: Turned on AR.");
        }
        else
        {
            ARCamera.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            Debug.Log($"Button_ToggleAR.ToggleARMode: Turned off AR.");
        }
    }

    private void UpdateComponents()
    {
        UpdateActiveCamera();
        UpdateButtonText();
        UpdateBackground();
    }

    private void UpdateActiveCamera()
    {
        NonARCamera.gameObject.SetActive(!isAREnabled);
        ARCamera.gameObject.SetActive(isAREnabled);
    }

    private void UpdateButtonText()
    {
        if (isAREnabled)
            buttonText.text = "Disable AR";
        else
            buttonText.text = "Enable AR";
    }

    private void UpdateBackground()
    {
        nonARBackground.SetActive(!isAREnabled);

    }

    //# Input Event Handlers 
    public void OnButtonPressed()
    {
        ToggleARMode();
    }
}
