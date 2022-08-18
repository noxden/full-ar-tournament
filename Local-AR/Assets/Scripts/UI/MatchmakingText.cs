//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 16-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchmakingText : MonoBehaviour
{
    //# Public Variables 
    public List<string> splashes;

    //# Private Variables 
    private TextMeshProUGUI textBox;
    private string selectedSplash = "";

    //# Monobehaviour Events 
    private void Awake()
    {
        textBox = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable() => CombatHandler.MatchStart += OnMatchStart;
    private void OnDisable() => CombatHandler.MatchStart -= OnMatchStart;

    private void Start()
    {
        SelectRandomSplash();
        StartCoroutine(UpdateTextWithLoadingDots());
    }

    //# Private Methods 
    private void SelectRandomSplash()
    {
        int randomIndex = Random.Range(0, splashes.Count);  //< not "splashes.Count-1" as Random.Range for ints is maxExclusive!
        selectedSplash = splashes[randomIndex];
    }

    private IEnumerator UpdateTextWithLoadingDots()
    {
        int dotAmount = 0;

        while (dotAmount <= 3)
        {
            string dotString = "";
            for (int i = 0; i < dotAmount; i++)
            {
                dotString += ".";
            }

            UpdateText($"{selectedSplash}{dotString}");
            yield return new WaitForSeconds(1);
            
            dotAmount += 1;
        }
        StartCoroutine(UpdateTextWithLoadingDots());
    }

    private void UpdateText(string newText)
    {
        textBox.text = newText;
    }

    //# Input Event Handlers 
    private void OnMatchStart()
    {
        StopAllCoroutines();
    }
}
