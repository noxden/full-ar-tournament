//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 30-07-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_Button_EnemyJoined : MonoBehaviour
{
    //# Public Variables 
    public string DEBUGUsername;
    public List<MonsterData> DEBUGMonsterDatas;

    //# Input Event Handlers 
    public void OnButtonPressed()
    {
        CombatHandler.Instance.OnPlayerDataReceived(DEBUGUsername, DEBUGMonsterDatas);
    }
}