//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 16-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_SaveMonsterSelection : MonoBehaviour
{
    public void OnButtonPressed()
    {
        string stringOfMonsterIndexes = "";
        foreach (var entry in GameManager.Instance.user.MonstersInBag)
        {
            int libraryIndex = GameManager.Instance.GetLibraryIndexOfMonster(entry);
            stringOfMonsterIndexes += $"{(string.IsNullOrWhiteSpace(stringOfMonsterIndexes) ? "" :",")}{libraryIndex}";
        }
        Debug.Log($"Button_SaveMonsterSelection: Saving stringOfMonsterIndexes: \"{stringOfMonsterIndexes}\"", this);
        SaveDataManager.monstersInBag = stringOfMonsterIndexes;
    }
}