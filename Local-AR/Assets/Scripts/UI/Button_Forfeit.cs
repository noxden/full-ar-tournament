using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Forfeit : MonoBehaviour
{
    public void OnButtonPressed()
    {
        WebSocketConnection.Instance.CreateLeavePackage();
        CombatHandler.Instance.FinishGame(false);
    }
}
