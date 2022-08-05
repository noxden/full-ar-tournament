using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_DisconnectFromServer : MonoBehaviour
{
    public void OnButtonPressed()
    {
        WebSocketConnection.Instance.DisconnectFromServer();
    }
}
