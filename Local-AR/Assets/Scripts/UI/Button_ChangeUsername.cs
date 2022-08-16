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

public class Button_ChangeUsername : MonoBehaviour
{
    //# Public Variables 
    //public TextMeshProUGUI inputField;
    public TMP_InputField inputField;

    //# Monobehaviour Events 
    private void Start()
    {
        inputField.text = SaveDataManager.localUsername;
    }

    //# Input Event Handlers 
    public void OnButtonPressed()
    {
        if (string.IsNullOrWhiteSpace(inputField.text))
            inputField.text = SaveDataManager.localUsername;
        else
            GameManager.Instance.user.ChangeUsername(inputField.text);
    }
}