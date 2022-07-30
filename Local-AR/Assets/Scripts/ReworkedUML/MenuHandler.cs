//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 30-07-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    //# Public Variables 
    public List<Canvas> MenuScreens;

    //# Monobehaviour Events 
    private void Start()
    {
        SwitchToMenu(0);
    }

    //# Public Methods 
    public Canvas SwitchToMenu(int index)
    {
        Canvas newScreen;
        newScreen = MenuScreens[index];

        foreach (var entry in MenuScreens)
        {
            ToggleVisibility(entry, false);
        }
        ToggleVisibility(newScreen, true);

        return newScreen;
    }

    //# Private Methods 
    private void ToggleVisibility(Canvas targetCanvas, bool visibility)
    {
        CanvasGroup canvasGroup = targetCanvas.GetComponent<CanvasGroup>();
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
    }
}
