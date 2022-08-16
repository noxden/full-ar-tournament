//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 16-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_MonsterToBox : MonoBehaviour
{
    //# Public Variables 
    public MonsterData monsterData;

    //# Private Variables 
    private Image buttonImage;

    //# Monobehaviour Events 
    private void Awake()
    {
        List<Image> ImageComponentsInChildren = new List<Image>(GetComponentsInChildren<Image>());
        foreach (Image image in ImageComponentsInChildren)
        {
            if (image.CompareTag("MonsterSprite"))
            {
                buttonImage = image;
                break;
            }
        }
    }

    private void Start()
    {
        if (monsterData == null)
        {
            buttonImage.sprite = null;
            buttonImage.color = new Color(1, 1, 1, 0);
        }
        else
        {
            buttonImage.sprite = monsterData.icon;
            buttonImage.color = new Color(1, 1, 1, 1);
        }
    }

    //# Public Methods 

    //# Private Methods 

    //# Input Event Handlers 
    public void OnButtonPressed()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError($"Could not find GameManager in scene. ERROR_BTN1", this);
            return;
        }

        GameManager.Instance.user.ChangeIsInBag(monsterData, false);
    }
}
