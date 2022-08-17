//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 16-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersInBoxVisualiser : MonoBehaviour
{
    //# Public Variables 
    public GameObject ButtonPrefab;

    //# Private Variables 

    //# Monobehaviour Events 
    // private void OnEnable() => UserProfile.MonsterBagStateChanged += UpdateButtonList;   //< Deprecated, as the Buttons are now based on the MonsterData from MonsterLibrary.
    // private void OnDisable() => UserProfile.MonsterBagStateChanged += UpdateButtonList;

    private void Start()
    {
        UpdateButtonList();
    }

    //# Public Methods 

    //# Private Methods 
    private void UpdateButtonList()
    {
        List<MonsterData> MonstersInBox = GameManager.Instance.MonsterLibrary;
        foreach (MonsterData monsterData in MonstersInBox)
        {
            //> Instantiate new button and add it to the vertical layout group
            GameObject buttonGameObject = Instantiate(ButtonPrefab);
            buttonGameObject.transform.SetParent(this.gameObject.transform);

            //> Hand over monsterData to newly created button
            buttonGameObject.GetComponent<Button_MonsterToBag>().SetMonsterData(monsterData);
        }
    }

    //# Input Event Handlers 
}
