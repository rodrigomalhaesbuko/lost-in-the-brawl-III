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
    private bool foundHost = false;
    [SerializeField] private GameObject AlertBox;
    [SerializeField] private GameObject Disclaimer;

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
        BoltMatchmaking.CreateSession(sessionID: CreateGameInput.text, sceneToLoad: "GameScene");
    }


    public void JoinGame()
    {
        BoltLauncher.StartClient();
        Debug.Log(JoinGameInput.text);
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        foreach(var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;
            Debug.Log(photonSession.HostName.ToString());
            if(photonSession.Source == UdpSessionSource.Photon)
            {
                if (photonSession.HostName.ToString() == JoinGameInput.text)
                {
                    BoltMatchmaking.JoinSession(photonSession);
                    foundHost = true;
                }
              
            }
        }
        StartCoroutine(CannotConectWithHost());


    }

    IEnumerator CannotConectWithHost()
    {

        yield return new WaitForSecondsRealtime(20.0f);
        if (!foundHost)
        {
            //mostrar que não achou a sala com o nome
            Debug.Log("NAO ACHOU A SALA COM O NOME" + " " + JoinGameInput.text);
            BoltLauncher.Shutdown();
            OpenAlertBox();
        }

    }

    public void CloseAlertBox()
    {
        AlertBox.SetActive(false);
    }

    public void OpenAlertBox()
    {
        Disclaimer.GetComponent<Text>().text = "NAO FOI POSSIVEL ENCONTRAR A SALA COM O NOME" + JoinGameInput.text;
        AlertBox.SetActive(true);
    }
}