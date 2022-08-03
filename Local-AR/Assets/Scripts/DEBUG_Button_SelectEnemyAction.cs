//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 01-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DEBUG_Button_SelectEnemyAction : MonoBehaviour
{
    //# Public Variables 
    public Player enemyPlayer;
    public int actionNumber;

    //# Private Variables 
    private Monster enemyMonster;
    private TextMeshProUGUI buttonText;

    //# Monobehaviour Events 
    private void Awake()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        enemyMonster = enemyPlayer.GetFirstValidMonster();

        string actionName;
        if (CombatHandler.Instance.GetActionAtIndex(enemyMonster, actionNumber - 1) == null)      //< If monster does not have an action at this index.
            actionName = "-";
        else
            actionName = CombatHandler.Instance.GetActionAtIndex(enemyMonster, actionNumber - 1).name;

        buttonText.text = actionName;
    }

    //# Input Event Handlers 
    public void OnButtonPressed()
    {
        //Debug.Log($"DEBUG_Button_SelectEnemyAction.OnButtonPressed: Enemy selected Action \"{enemyMonster.AvailableActions[actionNumber-1]}\".");
        CombatHandler.Instance.OnActionDataReceived(enemyMonster.AvailableActions[actionNumber - 1]);
    }
}
