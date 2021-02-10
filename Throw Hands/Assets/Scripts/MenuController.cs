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
    private float posy = 2;

    private float setamovx = 0;

    private int pos = 2;
    private int vel = 60;

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
            if(posy > 2)
                posy = 2;
            else if(posy < 0)
                posy = 0;
            
        };

        controls.StaticScene.Select.performed += _ => go();
          
    }

    public void Start()
    {
        posx = 0;
        posy = 2;

        setamovx = 0;

        pos = 2;
        vel = 60;
    }

    public void Update()
    {
        if (pos < 4)
        {
            if (posy == 2)
            {
                seta.GetComponent<RectTransform>().localPosition = new Vector3(-900 - setamovx, 430, 0);
                pos = 0;
            }
            else if (posy == 1)
            {
                JoinGameInput.ActivateInputField();
                seta.SetActive(false);
                pos = 1;
            }
            else if (posx == 0)
            {
                seta.GetComponent<RectTransform>().localPosition = new Vector3(-785 - setamovx, -240, 0);
                pos = 2;
            }
            else
            {
                seta.GetComponent<RectTransform>().localPosition = new Vector3(-220 - setamovx, -240, 0);
                pos = 3;
            }

            setamovx += Time.deltaTime * vel;

            if (setamovx > 30)
            {
                vel *= -1;
            }
            else if (setamovx < 0)
            {
                vel *= -1;
            }

            if (pos != 1)
            {
                seta.SetActive(true);
                JoinGameInput.DeactivateInputField();
            }
        }

        //Debug.Log(pos);
        
    }

    public void go()
    {

        if (pos == 0)
        {
            controls.StaticScene.Disable();
            SceneManager.LoadScene("initialScreen");
        }
        else if(pos == 1)
        {
            posy--;
        }
        else
        if(pos == 2)
        {
            JoinGame();
        }
        else
        if(pos == 3)
        {
            CreateGame();
        }
        
    }

    public void CreateGame()
    {
        if (JoinGameInput.text.Length > 0)
        {
            pos = 4;
            controls.StaticScene.Disable();
            seta.SetActive(false);

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
            pos = 4;
            controls.StaticScene.Disable();
            seta.SetActive(false);

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
        controls.StaticScene.Enable();
        seta.SetActive(true);
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