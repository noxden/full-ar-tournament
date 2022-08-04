//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144) & Jan Alexander
// Last changed: 03-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using System.Text;
using System.IO;

public class WebSocketConnection : MonoBehaviour
{
    //# Public Variables 
    public static WebSocketConnection Instance { set; get; }

    //# Private Variables 
    private WebSocket _webSocket;
    private string _serverUrl = "ws://noxden.uber.space:42960/nodejs-server"; // REPLACE [username] & [port] with yours
    private int _serverErrorCode;

    //# Monobehaviour Events 
    private void Awake()
    {
        if (Instance == null)   //< With this if-structure it is IMPOSSIBLE to create more than one instance.
            Instance = this;
        else
            Destroy(this.gameObject);   //< If you somehow still get to create a new singleton gameobject regardless, destroy the new one.
    }

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

    //# Public Methods 
    public void CreateJoinPackage(Player playerData)
    {
        JoinPackage joinPackage = new JoinPackage(playerData);
        SendMessage(joinPackage);
    }

    public void CreateCombatPackage(Action actionData, float tieBreaker)
    {
        CombatPackage combatPackage = new CombatPackage(actionData, tieBreaker);
        SendMessage(combatPackage);
    }

    public async void DisconnectFromServer()
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            await _webSocket.Close();
        }
    }

    //# Private Methods 
    private async void SendEmptyMessageToServer()
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            byte[] bytes = new byte[1] { 1 };
            await _webSocket.Send(bytes);
        }
    }

    private void SendMessage(Package package)   //< FOR DEBUG ONLY
    {
        Debug.Log($"DEBUG: Sending message {package.packageType}.");
    }

    // private async void SendMessage(Package package)    //< Can be called with: Invoke("SendMessage", 0f)
    // {
    //     if (_webSocket.State == WebSocketState.Open)
    //     {
    //         string json = JsonUtility.ToJson(package);
    //         byte[] bytes = Encoding.UTF8.GetBytes(json);
    //         await _webSocket.Send(bytes);
    //     }
    // }

    //# Event Handlers 
    private void OnOpen()
    {
        print("Connection opened");
        Invoke("SendEmptyMessageToServer", 0f);
    }

    private void OnMessage(byte[] incomingBytes)
    {
        // Follow steps in Jan's tutorial
        // Convert bytes to string
        // Convert string to json
        // Check for json.packageType variable
        // Depending on packageType, convert json to appropriate package class
        // if (packageType == JoinPackage)
            // Convert json to C# class "JoinPackage"
            // Read class
            // Player unpackagedPlayer.username = joinPackage.username
            // ...
            // ...
            // CombatHandler.Instance.OnPlayerDataReceived(unpackagedPlayer)
        // else if (packageType == CombatPackage)
            // Convert json to C# class "CombatPackage"
            // Read class
            // Action unpackagedAction = combatPackage.action
            // float unpackagedTieBreaker = combatPackage.tieBreaker
            // CombatHandler.Instance.OnActionDataReceived(unpackagedAction, unpackagedTieBreaker)
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
}