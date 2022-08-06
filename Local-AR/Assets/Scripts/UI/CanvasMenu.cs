//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 06-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
public class CanvasMenu : MonoBehaviour
{
    //# Public Variables 
    public new MenuName name;
    public bool isPersistent;
    public bool isVisible { get; private set; } = true;

    //# Private Variables 
    private CanvasGroup canvasGroup;

    //# Monobehaviour Events 
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //# Private Methods 
    public void SetVisibility(bool visibility)
    {
        if (visibility == isVisible)
            return;

        switch (visibility)
        {
            case true:
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                break;
            case false:
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                break;
        }
        isVisible = visibility;
    }
}
