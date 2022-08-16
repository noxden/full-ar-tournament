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
using TMPro;

public class Button_MonsterToBag : MonoBehaviour
{
    //# Public Variables 
    public TextMeshProUGUI textMonsterName;
    public TextMeshProUGUI textMonsterDescription;

    //# Private Variables 
    private Image buttonImage;
    private MonsterData monsterData;

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

    //# Public Methods 
    public void SetMonsterData(MonsterData _monsterData)
    {
        this.monsterData = _monsterData;
        UpdateButtonVisuals();
    }

    //# Private Methods 
    private void UpdateButtonVisuals()
    {
        buttonImage.sprite = monsterData.icon;
        textMonsterName.text = monsterData.species;
        textMonsterDescription.text = $"Type {(monsterData.Types.Count == 1 ? $"{monsterData.Types[0]}" : $"{monsterData.Types[0]}/{monsterData.Types[1]}")}{(string.IsNullOrWhiteSpace(monsterData.nickname) ? "" :  $" | Nickname: \"{monsterData.nickname}\"")}";
        //textMonsterDescription.text = $"{monsterData.hpMax} HP | ATK: {monsterData.attack} | SATK: {monsterData.specialAttack} | DEF: {monsterData.defense} | SDEF: {monsterData.specialDefense} | SPD: {monsterData.speed}";
    }

    //# Input Event Handlers 
    public void OnButtonPressed()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError($"Could not find GameManager in scene. ERROR_BTN1", this);
            return;
        }

        GameManager.Instance.user.ChangeIsInBag(monsterData, true);
    }
}
