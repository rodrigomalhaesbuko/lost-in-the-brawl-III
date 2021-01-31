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



    public void CreateGame()
    {
        if(JoinGameInput.text.Length > 0)
        {
            BoltLauncher.StartServer();
        }
        else
        {
            OpenAlertBox2();
        }
        
    }
    public override void BoltStartDone()
    {
        BoltMatchmaking.CreateSession(sessionID: JoinGameInput.text, sceneToLoad: "GameScene");
    }


    public void JoinGame()
    {
        if (JoinGameInput.text.Length > 0)
        {
            BoltLauncher.StartClient();
            //Debug.Log(JoinGameInput.text);
            StartCoroutine(CannotConectWithHost());
        }
        else
        {
            OpenAlertBox2();
        }

        Debug.Log("foi");
       
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        foreach (var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;
            if(photonSession.Source == UdpSessionSource.Photon)
            {
                if (photonSession.HostName.ToString() == JoinGameInput.text)
                {
                    BoltMatchmaking.JoinSession(photonSession);
                    foundHost = true;
                }
              
            }
        }

    }

    IEnumerator CannotConectWithHost()
    {

        yield return new WaitForSecondsRealtime(40.0f);
        if (!foundHost)
        {
            //mostrar que não achou a sala com o nome
            //Debug.Log("NAO ACHOU A SALA COM O NOME" + " " + JoinGameInput.text);
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
        Disclaimer.GetComponent<Text>().text = "NAO FOI POSSIVEL ENCONTRAR A SALA COM O NOME " + JoinGameInput.text;
        AlertBox.SetActive(true);
    }

    public void OpenAlertBox2()
    {
        Disclaimer.GetComponent<Text>().text = "You need to type a valid room name!";
        AlertBox.SetActive(true);
    }

}