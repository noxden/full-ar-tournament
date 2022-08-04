//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144) & Jan Alexander
// Last changed: 03-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;  //< Source: https://github.com/endel/NativeWebSocket
using System.Text;
using System.IO;
using SimpleJSON;   //< Source: https://github.com/Bunny83/SimpleJSON/blob/master/SimpleJSON.cs

public class WebSocketConnection : MonoBehaviour
{
    //# Public Variables 
    public static WebSocketConnection Instance { set; get; }

    //# Private Variables 
    private WebSocket webSocket;
    private string serverUrl = "ws://noxden.uber.space:42960/nodejs-server"; // REPLACE [username] & [port] with yours
    private int serverErrorCode;

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
        webSocket = new WebSocket(serverUrl);

        webSocket.OnOpen += OnOpen;
        webSocket.OnMessage += OnMessage;
        webSocket.OnClose += OnClose;
        webSocket.OnError += OnError;

        await webSocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        webSocket.DispatchMessageQueue();
#endif
    }

    private async void OnApplicationQuit()
    {
        await webSocket.Close();
    }

    //# Public Methods 
    public async void DisconnectFromServer()
    {
        if (webSocket.State == WebSocketState.Open)
        {
            await webSocket.Close();
        }
    }

    public void CreateJoinPackage(Player playerData)
    {
        JoinPackage joinPackage = new JoinPackage(playerData);
        SendJoinPackage(joinPackage);
    }

    public void CreateCombatPackage(Action actionData, float tieBreaker)
    {
        CombatPackage combatPackage = new CombatPackage(actionData, tieBreaker);
        SendCombatPackage(combatPackage);
    }

    //# Private Methods 
    private async void SendEmptyMessageToServer()
    {
        if (webSocket.State == WebSocketState.Open)
        {
            byte[] bytes = new byte[1] { 1 };
            await webSocket.Send(bytes);
        }
    }

    //> This implementation derives from package an is thereby less redundant.
    // private async void SendMessage(Package package)    //< Can be called with: Invoke("SendMessage", 0f)
    // {
    //     Debug.Log($"WebSocketConnection.SendMessage: Attempting to send message {package.packageType}."};
    //     if (_webSocket.State == WebSocketState.Open)
    //     {
    //         string json = JsonUtility.ToJson(package);
    //         byte[] bytes = Encoding.UTF8.GetBytes(json);
    //         await _webSocket.Send(bytes);
    //     }
    // }

    private async void SendJoinPackage(JoinPackage outgoingJoinPackage)
    {
        if (webSocket.State == WebSocketState.Open)
        {
            string json = JsonUtility.ToJson(outgoingJoinPackage);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            await webSocket.Send(bytes);
        }
    }

    private async void SendCombatPackage(CombatPackage outgoingCombatPackage)
    {
        if (webSocket.State == WebSocketState.Open)
        {
            string json = JsonUtility.ToJson(outgoingCombatPackage);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            await webSocket.Send(bytes);
        }
    }

    //# Event Handlers 
    private void OnOpen()
    {
        Debug.Log("Connection opened");
        Invoke("SendEmptyMessageToServer", 0f);
    }

    // private void OnMessage(byte[] incomingBytes)     //< My initial pseudo-code
    // {
    //     // Follow steps in Jan's tutorial
    //     // Convert bytes to string
    //     // Convert string to json
    //     // Check for json.packageType variable
    //     // Depending on packageType, convert json to appropriate package class
    //     // if (packageType == JoinPackage)
    //         // Convert json to C# class "JoinPackage"
    //         // Read class
    //         // Player unpackagedPlayer.username = joinPackage.username
    //         // ...
    //         // ...
    //         // CombatHandler.Instance.OnPlayerDataReceived(unpackagedPlayer)
    //     // else if (packageType == CombatPackage)
    //         // Convert json to C# class "CombatPackage"
    //         // Read class
    //         // Action unpackagedAction = combatPackage.action
    //         // float unpackagedTieBreaker = combatPackage.tieBreaker
    //         // CombatHandler.Instance.OnActionDataReceived(unpackagedAction, unpackagedTieBreaker)
    //     Debug.Log("Message received");
    // }

    private void OnMessage(byte[] inboundBytes)
    {
        Debug.Log("Message received");
        Debug.Log($"bytes: {inboundBytes}");
        string inboundString = System.Text.Encoding.UTF8.GetString(inboundBytes);
        Debug.Log($"message: {inboundString}");


        if (int.TryParse(inboundString, out serverErrorCode))
        {
            Debug.Log($"Server Error: {serverErrorCode}");    //< If server returns an integer, it is an error
        }
        else
        {
            JSONNode json = JSON.Parse(inboundString);

            if (json["packageType"].Value == JoinPackage.packageType)
            {
                JoinPackage unpackedJoinPackage = JsonUtility.FromJson<JoinPackage>(inboundString);
                string MonstersOnList = "";
                foreach (var entry in unpackedJoinPackage.Monsters)
                {
                    MonstersOnList += $"{entry.GetName()}{(unpackedJoinPackage.Monsters.FindIndex(m => m == entry) >= unpackedJoinPackage.Monsters.Count - 1 ? "" : ", ")}";  //! This log message has not been tested yet.
                }
                Debug.Log($"Received JoinPackage (containing Player) || Username: {unpackedJoinPackage.username}, Monsters: {MonstersOnList}.");
                // TODO: Do something with that data.
            }
            else if (json["packageType"].Value == CombatPackage.packageType)
            {
                CombatPackage unpackedCombatPackage = JsonUtility.FromJson<CombatPackage>(inboundString);
                Debug.Log($"Received CombatPackage (containing Action)|| Action name: {unpackedCombatPackage.action.name}, Tiebreaker: {unpackedCombatPackage.tieBreaker}.");
                // TODO: Do something with that data.
            }
        }
    }

    private void OnClose(WebSocketCloseCode closeCode)
    {
        Debug.Log($"Connection closed: {closeCode}");
    }

    private void OnError(string errorMessage)
    {
        Debug.Log($"Connection error: {errorMessage}");
    }
}