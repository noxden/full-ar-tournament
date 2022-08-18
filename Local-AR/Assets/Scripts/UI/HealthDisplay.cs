//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 18-08-22
//! Terrible code quality.
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    //# Public Variables 
    public bool isYourMonster;
    public TextMeshProUGUI textBoxName;
    public TextMeshProUGUI textBoxHPCurrent;
    public TextMeshProUGUI textBoxHPMax;
    public Image sliderFill;
    public Slider HPSlider;

    //# Private Variables 
    [SerializeField] private Monster monster = null;
    private Coroutine activeUpdateCurrentHP = null;

    //# Monobehaviour Events 

    //> Subscribe to & Unsubscribe from delegates
    private void OnEnable()
    {
        Player.OnMonsterOnFieldSwapped += OnMonsterSwapped;
        Monster.OnDamageTaken += OnDamageTaken;
    }
    private void OnDisable()
    {
        Player.OnMonsterOnFieldSwapped -= OnMonsterSwapped;
        Monster.OnDamageTaken -= OnDamageTaken;
    }

    //# Public Methods 

    //# Private Methods 

    // private void UpdateDisplay()  //< DEPRECATED: Split up, one part into OnMonsterSwapped and the other into OnDamageTaken.
    // {
    //     textBoxName.text = monster.name;
    //     textBoxHPMax.text = monster.hpMax.ToString();
    //     //textBoxHPCurrent.text = monster.hpCurrent.ToString();

    //     HPSlider.maxValue = monster.hpMax;
    //     StartCoroutine(UpdateCurrentHP());
    // }

    private IEnumerator UpdateCurrentHP()
    {
        float lerpDuration = 3;
        float startValue = HPSlider.value;       //< sliderValue is the hpCurrent from before UpdateSlider was called.
        float endValue = monster.hpCurrent;

        float delta = 0;
        while (delta <= 1)
        {
            //Debug.Log($"HealthDisplay.UpdateSlider: StartValue {startValue}, EndValue is {endValue} and Delta is {delta}, resulting in a current SliderValue of {HPSlider.value}.");
            HPSlider.value = Mathf.Lerp(startValue, endValue, delta);
            textBoxHPCurrent.text = ((int)Mathf.Lerp(startValue, endValue, delta)).ToString();

            //> Sadly, dynamically changing slider fill colors seems to be impossible, as the displayed color always glitches to white...
            // Debug.Log($"Current Color should be {GetHPBarColor(HPSlider.value, monster.hpMax).ToString()}.");
            // sliderFill.color = GetHPBarColor(HPSlider.value, monster.hpMax);

            delta += Time.deltaTime / lerpDuration;
            yield return new WaitForEndOfFrame();
        }
        HPSlider.value = endValue;  //< As the lerp never reaches endValue, because it stops running when delta finally reaches 1.
        activeUpdateCurrentHP = null;
        yield return null;
    }

    private Color GetHPBarColor(float currentHP, float maxHP)   //< Returns color based on the percentage of HP (based on the two parameters)
    {
        if (currentHP > maxHP * 0.50)
            return new Color(46, 176, 54, 1);  //< Green
        else if (currentHP > maxHP * 0.20)
            return new Color(255, 226, 88, 1); //< Yellow
        else
            return new Color(255, 96, 88, 1);  //< Red
    }

    private IEnumerator UpdateDisplayAfterCoroutineEnds()   //< Waits for coroutine "UpdateCurrentHP" to finish
    {
        while (activeUpdateCurrentHP != null)
        {
            yield return new WaitForEndOfFrame();
            Debug.Log($"Waited for Coroutine to end.");
        }
        yield return new WaitForSeconds(1f);    //< To give the user a second to look at the 0 HP

        Debug.Log($"Test point in code was reached.");
        UpdateDisplayInstantly();
    }

    private void UpdateDisplayInstantly()
    {
        textBoxName.text = monster.name;
        textBoxHPMax.text = monster.hpMax.ToString();
        textBoxHPCurrent.text = monster.hpCurrent.ToString();
        HPSlider.maxValue = monster.hpMax;
        HPSlider.value = monster.hpCurrent;
    }


    //# Input Event Handlers 
    // private void OnMonsterSwapped(Player invokingPlayer)
    // {
    //     if (isYourMonster && invokingPlayer == CombatHandler.Instance.GetYourPlayer())
    //     {
    //         monster = invokingPlayer.GetMonsterOnField();
    //     }
    //     else if (!isYourMonster && invokingPlayer == CombatHandler.Instance.GetEnemyPlayer())
    //     {
    //         monster = invokingPlayer.GetMonsterOnField();
    //     }

    //     UpdateDisplay();
    // }

    private void OnMonsterSwapped(Player invokingPlayer)    //! Very poor code quality
    {
        if (isYourMonster)
        {
            if (CombatHandler.Instance.GetYourPlayer() != null)
                if (monster == CombatHandler.Instance.GetYourPlayer().GetMonsterOnField())  //< If that monster was not swapped, don't bother updating the display.
                    return;
                else
                    monster = CombatHandler.Instance.GetYourPlayer().GetMonsterOnField();
        }
        else if (!isYourMonster)
        {
            if (CombatHandler.Instance.GetEnemyPlayer() != null)
                if (monster == CombatHandler.Instance.GetEnemyPlayer().GetMonsterOnField())
                    return;
                else
                    monster = CombatHandler.Instance.GetEnemyPlayer().GetMonsterOnField();
        }
        if (monster == null)    //< Stop function here, if it could not fetch any "monster on field" yet.
            return;

        //> Set every value instantly on monster swap, unless the current healthbar update is still in progress
        if (activeUpdateCurrentHP == null)
            UpdateDisplayInstantly();
        else
            StartCoroutine(UpdateDisplayAfterCoroutineEnds());    //< Waits for coroutine "UpdateCurrentHP" to finish
    }

    private void OnDamageTaken(Monster invokingMonster)
    {
        if (invokingMonster == monster)
        {
            //UpdateDisplay();

            //> Set hp visualisation (counter and healthbar) with a lerp on damage taken.
            activeUpdateCurrentHP = StartCoroutine(UpdateCurrentHP());
        }
    }
}
