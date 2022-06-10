//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR
// Script by:    Daniel Heilmann (771144)
// Last changed: 10-06-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class TouchManager : MonoBehaviour
{
    //# Public Variables 
    private GameManager gameManager;

    //# Monobehaviour Events 
    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    void OnEnable()
    {
        LeanTouch.OnFingerTap += OnTap;
    }

    void OnDisable()
    {
        LeanTouch.OnFingerTap -= OnTap;
    }

    //# Private Methods 
    void OnTap(LeanFinger finger)
    {
        Debug.Log($"You just tapped the screen with finger \"{finger.Index}\" at {finger.ScreenPosition}.", this);
        gameManager.OnTap(finger);
    }
}
