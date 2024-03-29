//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 17-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button_SelectAction : MonoBehaviour
{
    //# Public Variables 
    public int actionNumber;

    //# Private Variables 
    private TextMeshProUGUI buttonText;

    //# Monobehaviour Events 
    private void Awake()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable() => Player.OnMonsterOnFieldSwapped += UpdateButtonText;
    private void OnDisable() => Player.OnMonsterOnFieldSwapped -= UpdateButtonText;

    //# Private Methods 
    private void UpdateButtonText(Player player)
    {
        if (player != CombatHandler.Instance.GetYourPlayer())
            return;

        string actionName;
        if (player.GetMonsterOnField().AvailableActions[actionNumber - 1] == null)      //< If monster does not have an action at this index.
            actionName = "-";
        else
            actionName = player.GetMonsterOnField().AvailableActions[actionNumber - 1].name;

        buttonText.text = actionName;
    }

    //# Input Event Handlers 
    public void OnButtonPressed()
    {
        if (CombatHandler.Instance == null)
        {
            Debug.LogError($"Could not find CombatHandler in scene. ERROR_BTN2", this);
            return;
        }
        CombatHandler.Instance.SelectAvailableActionAtIndex(actionNumber - 1);
    }
}
