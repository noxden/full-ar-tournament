//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 11-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlavourTextBox : MonoBehaviour
{
    //# Public Variables 

    //# Private Variables 
    private TextMeshProUGUI textBox;

    //# Monobehaviour Events 
    private void Awake()
    {
        textBox = GetComponentInChildren<TextMeshProUGUI>();
    }

    //# Public Methods 
    public void UpdateText(string newText)
    {
        // if (textBox.textInfo.pageCount > 1)
        //     Debug.Log($"FlavourTextBox.UpdateTextBox: There are multiple pages present!");
        textBox.text = newText;
    }

    //# Private Methods 

    //# Input Event Handlers 
    public void OnButtonPressed()
    {
        FlavourTextHandler.Instance.OnButtonPressed();
    }
}


// //# Private Variables 
// private static Delegate OnNextText;
// private static Delegate OnUpdateTextBox;
// private TextMeshProUGUI textBox;
// [SerializeField] private static List<string> Queue = new List<string>();     //< To queue all incoming flavourtexts

// //# Monobehaviour Events 
// private void Awake()
// {
//     textBox = GetComponentInChildren<TextMeshProUGUI>();

//     OnNextText += NextText;
//     OnUpdateTextBox += UpdateTextBox;
// }

// private void Start()
// {
//     QueueText("Welcome to the Tournament!");
//     UpdateTextBox();
// }

// //# Public Methods 
// public static void QueueText(string newText)
// {
//     Queue.Add(newText);
// }

// //# Private Methods 
// private void NextText()
// {
//     if (Queue.Count > 1)    //< Do not scroll if there is nothing to scroll to anymore.
//     {
//         Queue.RemoveAt(0);
//         OnUpdateTextBox();
//     }
// }

// private void UpdateTextBox()
// {
//     if (textBox.textInfo.pageCount > 1)
//         Debug.Log($"FlavourTextBox.UpdateTextBox: There are multiple pages present!");
//     if (Queue.Count > 0)
//         textBox.text = Queue[0];
// }

// //# Input Event Handlers 
// public void OnButtonPressed()
// {
//     OnNextText();
// }