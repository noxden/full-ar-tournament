//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 18-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button_ToggleAR : MonoBehaviour
{
    //# Public Variables 
    public bool isAREnabled = false;
    public Camera NonARCamera;
    public Camera ARCamera;
    public GameObject ARSession;
    public GameObject nonARBackground;
    public List<ARHealthDisplayCanvas> ARHealthDisplays;

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
        UpdateHealthDisplays();
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

    private void UpdateHealthDisplays()
    {
        //> Toggle HUD HealthDisplay
        MenuHandler.Instance.TogglePersistentMenu(MenuName.PermHealthDisplay, !isAREnabled);

        //> Toggle AR HealthDisplay
        foreach (var entry in ARHealthDisplays)
        {
            entry.SetARCamera(ARCamera);
            entry.SetVisibility(isAREnabled);
        }
    }

    //# Input Event Handlers 
    public void OnButtonPressed()
    {
        ToggleARMode();
    }
}
