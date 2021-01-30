using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using System;
using Bolt.Matchmaking;
using UdpKit;
using UnityEngine.UI;

public class MenuController: GlobalEventListener
{
    //    [SerializeField] private string versionName = "0.1";
    //    [SerializeField] private GameObject conectPanel;
    [SerializeField] private InputField CreateGameInput;
    [SerializeField] private InputField JoinGameInput;

    //    private void Awake()
    //    {
    //        PhotonNetwork.ConnectUsingSettings(versionName);
    //    }

    //    void OnConnectedToMaster()
    //    {
    //        PhotonNetwork.JoinLobby(TypedLobby.Default);
    //        PhotonNetwork.playerName = "rodrigo";
    //        Debug.Log("connected");
    //    }

    //    public void CreateGame()
    //    {
    //        PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { MaxPlayers = 2 }, null);
    //        Debug.Log("obaaa");
    //    }

    //    public void JoinGame()
    //    {
    //        RoomOptions roomOptions = new RoomOptions();
    //        roomOptions.MaxPlayers = 2;
    //        PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);

    //    }

    //    private void OnJoinedRoom()
    //    {
    //        PhotonNetwork.LoadLevel("GameScene");
    //        Debug.Log("mudou de scena");
    //    }
    //}

    /// <summary>
    /// USING BOLT
    /// </summary>

    public void CreateGame()
    {
        BoltLauncher.StartServer();
    }
    public override void BoltStartDone()
    {
        BoltMatchmaking.CreateSession(sessionID: "test", sceneToLoad: "GameScene");
    }


    public void JoinGame()
    {
        BoltLauncher.StartClient();
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        foreach(var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;
            if(photonSession.Source == UdpSessionSource.Photon)
            {
                BoltMatchmaking.JoinSession(photonSession);
            }
        }
    }
}