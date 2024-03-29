//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144) & Jan Alexander
// Last changed: 04-08-22
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

    private int myUUID;
    private JoinPackage outgoingJoinPackage;
    private CombatPackage outgoingCombatPackage;

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
        if (GameManager.Instance != null)
            myUUID = GameManager.Instance.user.UUID;

        webSocket = new WebSocket(serverUrl);

        webSocket.OnOpen += OnOpen;
        webSocket.OnMessage += OnMessageReceived;
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
        CreateLeavePackage();
        await webSocket.Close();
    }

    //# Public Methods 
    public async void DisconnectFromServer()
    {
        if (webSocket.State == WebSocketState.Open)
        {
            CreateLeavePackage();
            await webSocket.Close();
        }
    }

    public void CreateJoinPackage(Player playerData)
    {
        Debug.Log("<color=#5EE8A5>Creating JoinPackage.</color>");
        outgoingJoinPackage = new JoinPackage(myUUID, playerData);
        SendJoinPackage();
    }

    public void CreateCombatPackage(Action actionData, float tieBreaker)
    {
        Debug.Log("<color=#5EE8A5>Creating CombatPackage.</color>");
        outgoingCombatPackage = new CombatPackage(myUUID, actionData, tieBreaker);
        SendCombatPackage();
    }

    public void CreateLeavePackage()
    {
        Debug.Log("<color=#5EE8A5>Creating LeavePackage.</color>");
        SendLeavePackage(new LeavePackage(myUUID));
    }

    //# Private Methods 
    private async void SendEmptyMessageToServer()   //< DEBUG
    {
        if (webSocket.State == WebSocketState.Open)
        {
            byte[] bytes = new byte[1] { 1 };
            await webSocket.Send(bytes);
        }
    }

    private async void SendJoinPackage()
    {
        if (webSocket.State == WebSocketState.Open)
        {
            Debug.Log("<color=#5EE8A5>Sending JoinPackage.</color>");
            string json = JsonUtility.ToJson(outgoingJoinPackage);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            await webSocket.Send(bytes);
            outgoingJoinPackage = null;     //< Clear after use to prevent any issues further down the line.
        }
    }

    private async void SendCombatPackage()
    {
        if (webSocket.State == WebSocketState.Open)
        {
            Debug.Log("<color=#5EE8A5>Sending CombatPackage.</color>");
            string json = JsonUtility.ToJson(outgoingCombatPackage);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            await webSocket.Send(bytes);
            outgoingCombatPackage = null;   //< Clear after use to prevent any issues further down the line.
        }
    }

    private async void SendLeavePackage(LeavePackage outgoingLeavePackage)
    {
        if (webSocket.State == WebSocketState.Open)
        {
            Debug.Log("<color=#5EE8A5>Sending LeavePackage.</color>");
            string json = JsonUtility.ToJson(outgoingLeavePackage);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            await webSocket.Send(bytes);
        }
    }

    //# Event Handlers 
    private void OnOpen()
    {
        Debug.Log("<color=#5EE8A5>Connection opened.</color>");
        if (outgoingJoinPackage != null)    //< This allows for the outgoingJoinPackage to be queued until server connection is established.
            Invoke("SendJoinPackage", 0f);
        //Invoke("SendEmptyMessageToServer", 0f);     //< DEBUG
    }

    private void OnMessageReceived(byte[] inboundBytes)
    {
        Debug.Log("<color=#5EE8A5>Message received.</color>");
        //Debug.Log($"<color=#5EE8A5>bytes: {inboundBytes}</color>");
        string inboundString = System.Text.Encoding.UTF8.GetString(inboundBytes);
        Debug.Log($"<color=#5EE8A5>Incoming message: {inboundString}</color>");


        if (int.TryParse(inboundString, out serverErrorCode))
        {
            Debug.Log($"<color=#5EE8A5>Server Error: {serverErrorCode}</color>");    //< If server returns an integer, it is an error
        }
        else
        {
            JSONNode json = JSON.Parse(inboundString);

            if (json["packageType"].Value == "JoinPackage")
            {
                JoinPackage unpackedJoinPackage = JsonUtility.FromJson<JoinPackage>(inboundString);
                if (unpackedJoinPackage.packageAuthorUUID == myUUID)
                {
                    Debug.Log($"<color=#5EE8A5>Received own JoinPackage, discarding information.</color>");
                    return;
                }
                else
                {
                    //> For debug message
                    string MonstersOnList = "";
                    foreach (int entry in unpackedJoinPackage.MonsterDataIndexList)
                    {
                        MonstersOnList += $"{GameManager.Instance.GetMonsterByLibraryIndex(entry).name}{(unpackedJoinPackage.MonsterDataIndexList.IndexOf(entry) >= unpackedJoinPackage.MonsterDataIndexList.Count - 1 ? "" : ", ")}";
                    }
                    Debug.Log($"<color=#5EE8A5>Received JoinPackage || Username: {unpackedJoinPackage.username}, Monsters: {MonstersOnList}.</color>");
                    // TODO: Do something with that data.
                    List<MonsterData> receivedMonsterDataList = new List<MonsterData>();   //< Convert back all MonsterData library indexes to MonsterDatas
                    foreach (int entry in unpackedJoinPackage.MonsterDataIndexList)
                    {
                        MonsterData receivedMonsterData = GameManager.Instance.GetMonsterByLibraryIndex(entry);
                        receivedMonsterDataList.Add(receivedMonsterData);
                    }
                    CombatHandler.Instance.OnPlayerDataReceived(unpackedJoinPackage.username, receivedMonsterDataList);
                }

            }
            else if (json["packageType"].Value == "CombatPackage")
            {
                CombatPackage unpackedCombatPackage = JsonUtility.FromJson<CombatPackage>(inboundString);
                if (unpackedCombatPackage.packageAuthorUUID == myUUID)
                {
                    Debug.Log($"<color=#5EE8A5>Received own CombatPackage, discarding information.</color>");
                }
                else
                {
                    Debug.Log($"<color=#5EE8A5>Received CombatPackage || Action: {GameManager.Instance.ActionLibrary[unpackedCombatPackage.libraryIndexOfAction].name}, Tiebreaker: {unpackedCombatPackage.tieBreaker}.</color>");
                    // TODO: Do something with that data.
                    Action receivedAction = GameManager.Instance.GetActionByLibraryIndex(unpackedCombatPackage.libraryIndexOfAction);
                    CombatHandler.Instance.OnActionDataReceived(receivedAction, unpackedCombatPackage.tieBreaker);
                }

                unpackedCombatPackage = null;
                return;
            }
            else if (json["packageType"].Value == "LeavePackage")
            {
                LeavePackage unpackedLeavePackage = JsonUtility.FromJson<LeavePackage>(inboundString);
                if (unpackedLeavePackage.packageAuthorUUID == myUUID)
                {
                    Debug.Log($"<color=#5EE8A5>Received own LeavePackage, discarding information.</color>");
                }
                else
                {
                    CombatHandler.Instance.OnEnemyLeft();
                }
            }
            else
                Debug.LogError($"<color=#5EE8A5>Something went wrong during parsing, as neither viable packageType was recognized. Parsed JSON file is: {json}</color>");
        }
    }

    private void OnClose(WebSocketCloseCode closeCode)
    {
        Debug.Log($"<color=#5EE8A5>Connection closed: {closeCode}</color>");
    }

    private void OnError(string errorMessage)
    {
        Debug.Log($"<color=#5EE8A5>Connection error: {errorMessage}</color>");
    }
}