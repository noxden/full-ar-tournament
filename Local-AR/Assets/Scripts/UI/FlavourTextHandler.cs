//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 15-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlavourTextHandler : MonoBehaviour
{
    //# Public Variables 
    public static FlavourTextHandler Instance { set; get; }
    public int currentQueuePosition = 0;    // { private set; get; }

    //# Private Variables 
    [SerializeField] private bool automaticScrolling = false;
    [SerializeField] private float waitTimeBetwLetters;
    [SerializeField] private float waitTimeBetwMessages;
    [SerializeField] private List<string> Queue;     //< To queue all incoming flavourtexts     //! [SerializeField] for debug visualisation purposes
    [SerializeField] private List<FlavourTextBox> TextBoxes;                                    //! [SerializeField] for debug visualisation purposes
    private Coroutine activeWriteByLetter;
    private bool hasMatchStarted = false;

    //# Monobehaviour Events 
    private void Awake()
    {
        //> Setup Singleton
        if (Instance == null)   //< With this if-structure it is IMPOSSIBLE to create more than one instance.
            Instance = this;
        else
            Destroy(this.gameObject);   //< If you somehow still get to create a new singleton gameobject regardless, destroy the new one.

        //> Fill list "TextBoxes" with all FlavourTextBoxes in the scene.
        TextBoxes = new List<FlavourTextBox>(FindObjectsOfType<FlavourTextBox>());        
    }

    //> Subscribe to & Unsubscribe from delegates
    private void OnEnable() => CombatHandler.MatchStart += OnMatchStart;
    private void OnDisable() => CombatHandler.MatchStart -= OnMatchStart;

    private void OnMatchStart()
    {
        hasMatchStarted = true;
        Queue.Insert(0, $"You are challenged by {(CombatHandler.Instance.GetEnemyPlayer() != null ? CombatHandler.Instance.GetEnemyPlayer().username : "Unknown")}.");
        DisplayQueuedFlavourText();
    }

    //# Public Methods 
    public int QueueText(string newText)    //< Returns Queue index of the newly queued string (may be useful for later reference)
    {
        //Debug.Log($"FlavourTextHandler queued new text \"{newText}\"."); //< DEBUG
        Queue.Add(newText);

        if (activeWriteByLetter == null && hasMatchStarted)    // If no flavourText is currently being written... AND the match has already started
        {
            DisplayQueuedFlavourText(); //...restart the display loop
        }

        return (Queue.Count - 1);    //< Because that is always the index of the last entry
    }

    //# Private Methods 
    private void DisplayQueuedFlavourText()
    {
        if (currentQueuePosition < Queue.Count)  //< Hinders currentQueuePosition from going out of bounds
        {
            if (activeWriteByLetter == null)
            {
                string stringToDisplay = Queue[currentQueuePosition];
                activeWriteByLetter = StartCoroutine(WriteByLetter(stringToDisplay));
            }
            else //< If there is a flavourText currently being written
            {
                StopCoroutine(activeWriteByLetter);
                //Debug.Log($"Coroutine \"WriteByLetter\" has been stopped externally.");
                activeWriteByLetter = null;
                StartCoroutine(WriteEntireMessage(Queue[currentQueuePosition]));
            }
        }
    }

    private IEnumerator WriteByLetter(string inputString)
    {
        for (int i = 1; i <= inputString.Length; i++)
        {
            string outputString;
            outputString = ((i != inputString.Length) ? inputString.Remove(i) : inputString);     //< outputString = inputString.Remove(i), unless i = inputString.length
            UpdateAllTextBoxes(outputString);
            yield return new WaitForSeconds(waitTimeBetwLetters);  //< Wait time inbetween letters.
        }
        activeWriteByLetter = null;
        IncreaseCurrentQueuePosition();

        yield return new WaitForSeconds(waitTimeBetwMessages);  //< Wait so that the user can read the text.
        if (automaticScrolling)
            DisplayQueuedFlavourText();
        //Debug.Log($"Coroutine \"WriteByLetter\" has now stopped on its own.");
    }

    private IEnumerator WriteEntireMessage(string inputString)
    {
        UpdateAllTextBoxes(inputString);
        
        IncreaseCurrentQueuePosition();

        //yield return null;

        //> Turn ON the code below to enable scrolling [waitTimeBetwMessages] seconds after clicking the button. 
        //  If turned OFF, the button has to be clicked again to continue the queue.
        yield return new WaitForSeconds(waitTimeBetwMessages);  //< Wait so that the user can read the text.
        if (automaticScrolling)
            DisplayQueuedFlavourText();
    }

    private void UpdateAllTextBoxes(string newText)
    {
        foreach (FlavourTextBox textBox in TextBoxes)
        {
            //Debug.Log($"Updating text in {textBox.name} to \"{newText}\".");  //< DEBUG
            textBox.UpdateText(newText);
        }
    }

    private void IncreaseCurrentQueuePosition()
    {
        currentQueuePosition += 1;
    }

    //# Input Event Handlers 

    public void OnButtonPressed()
    {
        DisplayQueuedFlavourText();
    }
}