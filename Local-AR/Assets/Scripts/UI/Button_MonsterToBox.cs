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
    public int BagSlot;

    //# Private Variables 
    private Image buttonImage;
    [SerializeField] private MonsterData monsterData;

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

    //> Subscribe to & Unsubscribe from delegates
    private void OnEnable() => UserProfile.MonsterBagStateChanged += UpdateButtonMonsterData;
    private void OnDisable() => UserProfile.MonsterBagStateChanged -= UpdateButtonMonsterData;

    private void Start()
    {
        if (BagSlot > GlobalSettings.maxMonstersInBag)
        {
            Image backgroundImage = GetComponentInChildren<Image>();
            backgroundImage.color = new Color(1, 1, 1, 0);
        }

        UpdateButtonMonsterData();
    }

    private void UpdateButtonMonsterData()
    {
        if (BagSlot <= GameManager.Instance.user.MonstersInBag.Count)
            monsterData = GameManager.Instance.user.MonstersInBag[BagSlot - 1];
        else
            monsterData = null;

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
        if (monsterData == null)
        {
            Debug.LogWarning($"This button does not contain any monster data.", this);
            return;
        }

        GameManager.Instance.user.ChangeIsInBag(monsterData, false);
    }
}
