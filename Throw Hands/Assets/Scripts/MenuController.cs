using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using System;
using Bolt.Matchmaking;
using UdpKit;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuController: GlobalEventListener
{
    //    [SerializeField] private string versionName = "0.1";
    //    [SerializeField] private GameObject conectPanel;
    [SerializeField] private InputField CreateGameInput;
    [SerializeField] private InputField JoinGameInput;
    private bool foundHost = false;
    [SerializeField] private GameObject AlertBox;
    [SerializeField] private GameObject Disclaimer;

    //Loading
    [SerializeField] private GameObject Loading;
    [SerializeField] private GameObject RoomName;
    [SerializeField] private GameObject RoomImageClient;
    [SerializeField] private GameObject RoomImageHost;

    private InputMaster controls;

    private float posx = 0;
    private float posy = 0;

    private int pos = 0;

    public GameObject seta;

    public void Awake()
    {
        controls = new InputMaster();
        controls.StaticScene.Enable();

        controls.StaticScene.Move.performed += ctx =>
        {
            posx += ctx.ReadValue<Vector2>().x;
            posy += ctx.ReadValue<Vector2>().y;

            if(posx > 1)
                posx = 1;
            else if (posx < 0)
                posx = 0;
            if(posy > 1)
                posy = 1;
            else if(posy < 0)
                posy = 0;
            
        };

        controls.StaticScene.Select.performed += _ => go();
          
    }

    public void Update()
    {

        if (posy == 1)
        {
            seta.GetComponent<RectTransform>().localPosition = new Vector3(-890, 430, 0);
            pos = 0;
        }
        else if (posx == 0)
        {
            seta.GetComponent<RectTransform>().localPosition = new Vector3(-775, -240, 0);
            pos = 1;
        }
        else
        {
            seta.GetComponent<RectTransform>().localPosition = new Vector3(-220, -240, 0);
            pos = 2;
        }

    }

    public void go()
    {
        controls.StaticScene.Disable();
        seta.SetActive(false);

        if (pos == 0)
        {
            SceneManager.LoadScene("initialScreen");
        }
        else
        if(pos == 1)
        {
            JoinGame();
        }
        else
        if(pos == 2)
        {
            CreateGame();
        }

        
    }

    public void CreateGame()
    {
        if(JoinGameInput.text.Length > 0)
        {
            Loading.SetActive(true);
            RoomImageHost.SetActive(true);
            RoomName.GetComponent<Text>().text = JoinGameInput.text;
            PlayerPrefs.SetString("roomName", JoinGameInput.text);
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
            Loading.SetActive(true);
            RoomImageClient.SetActive(true);
            RoomName.GetComponent<Text>().text = JoinGameInput.text;

            PlayerPrefs.SetString("roomName", JoinGameInput.text);

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
        Loading.SetActive(false);
        RoomImageHost.SetActive(false);
        RoomImageClient.SetActive(false);
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