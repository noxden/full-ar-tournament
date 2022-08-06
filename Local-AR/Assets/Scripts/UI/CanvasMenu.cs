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
    public bool isVisible { get { return isVisible_; } set { SetVisibility(value); } }

    //# Private Variables 
    private CanvasGroup canvasGroup;
    private bool isVisible_;    //< Needed to prevent SetVisibility() to start a recursive loop of calling itself.

    //# Monobehaviour Events 
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //# Private Methods 
    private void SetVisibility(bool visibility)
    {
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
        isVisible_ = visibility;
    }
}
