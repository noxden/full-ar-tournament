//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144) & Jan Alexander
// Last changed: 08-07-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;

public class WebSocketConnection : MonoBehaviour
{
    private WebSocket _webSocket;
    private string _serverUrl = "ws://noxden.uber.space:42960/nodejs-server"; // REPLACE [username] & [port] with yours
    private int _serverErrorCode;

    async void Start()
    {
        _webSocket = new WebSocket(_serverUrl);

        _webSocket.OnOpen += OnOpen;
        _webSocket.OnMessage += OnMessage;
        _webSocket.OnClose += OnClose;
        _webSocket.OnError += OnError;

        await _webSocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        _webSocket.DispatchMessageQueue();
#endif
    }


    //# Event Handlers 

    private void OnOpen()
    {
        print("Connection opened");
        Invoke("SendEmptyMessageToServer", 0f);
    }

    private void OnMessage(byte[] incomingBytes)
    {
        print("Message received");
    }

    private void OnClose(WebSocketCloseCode closeCode)
    {
        print($"Connection closed: {closeCode}");
    }

    private void OnError(string errorMessage)
    {
        print($"Connection error: {errorMessage}");
    }

    private async void OnApplicationQuit()
    {
        await _webSocket.Close();
    }

    //# Public Methods 
    private async void SendEmptyMessageToServer()
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            byte[] bytes = new byte[1] { 1 };
            await _webSocket.Send(bytes);
        }
    }
}