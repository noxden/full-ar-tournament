//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 18-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class Button_ToggleAR : MonoBehaviour
{
    //# Public Variables 
    public bool isAREnabled = false;
    public GameObject ARSessionBundlePrefab;
    public Camera nonARCamera;
    public GameObject nonARBackground;
    public List<ARHealthDisplayCanvas> ARHealthDisplays;

    //# Private Variables
    private TextMeshProUGUI buttonText;
    private GameObject ARSessionBundle;
    private ARSession ARSession = null;
    private Camera ARCamera = null;

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

    private void ToggleARMode(bool newState)    //< Overload to specify if the ARMode should be toggled on or off.
    {
        isAREnabled = newState;
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
        UpdateARSession();
        UpdateActiveCamera();
        UpdateButtonText();
        UpdateBackground();
        UpdateHealthDisplays();
    }

    private void UpdateARSession()
    {
        if (isAREnabled)
        {
            ARSessionBundle = Instantiate(ARSessionBundlePrefab);
            ARSession = ARSessionBundle.GetComponentInChildren<ARSession>();
            ARCamera = ARSessionBundle.GetComponentInChildren<Camera>();
        }
        else
        {
            if (ARSessionBundle != null)
                Destroy(ARSessionBundle);
        }
    }

    private void UpdateActiveCamera()
    {
        nonARCamera.gameObject.SetActive(!isAREnabled);
        if (ARCamera != null)
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
