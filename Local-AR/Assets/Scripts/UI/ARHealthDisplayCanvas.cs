//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 18-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
public class ARHealthDisplayCanvas : MonoBehaviour
{
    //# Public Variables 
    public bool isVisible { get; private set; }

    //# Private Variables 
    [SerializeField] private Camera ARCamera;
    private CanvasGroup canvasGroup;

    //# Monobehaviour Events 
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Update()
    {
        LookAtCamera();
    }

    //# Public Methods 
    public void SetVisibility(bool visibility)
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
        isVisible = visibility;
    }

    public void SetARCamera(Camera camera)
    {
        ARCamera = camera;
    }

    //# Private Methods 
    private void LookAtCamera()
    {
        if (!isVisible) //< Guard clause to cancel out of function if invisible
            return;

        if (ARCamera == null)
        {
            Debug.LogWarning($"ARHealthDisplayCanvas.LookAtCamera: ARCamera is null. Cannot look at camera.");
            return;
        }

        canvasGroup.gameObject.transform.LookAt(ARCamera.transform);
    }

}