using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_Button_EnemyJoins : MonoBehaviour
{
    //# Public Variables 
    public Player player;

    //# Input Event Handlers 
    public void OnButtonPressed()
    {
        CombatHandler.Instance.OnPlayerDataReceived(player.username, player.MonsterDataList);
    }
}
