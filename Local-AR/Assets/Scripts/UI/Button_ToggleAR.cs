//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 04-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Button_ToggleAR : MonoBehaviour
{
    //# Public Variables 
    public bool isAREnabled;
    public GameObject ARSession;
    public MenuName nonARMenu;

    //# Private Variables
    

    //# Monobehaviour Events 
    private void Awake()
    {
        
    }

    private void Start()
    {
    }

    //# Public Methods 

    //# Private Methods 

    //# Input Event Handlers 
    public void OnButtonPressed()
    {
    }
}
