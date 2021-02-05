using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bolt;
using UdpKit;
using System;
using Bolt.Matchmaking;
using UnityEngine.UI;

public class GameController : GlobalEventListener
{
    public GameObject counter;
    public GameObject roomName;

    public GameObject playerPrefab;
    public GameObject playerPrefab2;

    public GameObject lifeHost;
    public GameObject lifeClient;

    public GameObject Camera;

    private GameObject CameraPriv;
    
    private GameObject DouglasInstance;
    private GameObject CarlousInstance;

    public GameObject WaitingPlayer;

    private bool gameStarted = false;

    public float battleOffset = 5f;

    //endbattle vars
    public GameObject youwin;
    public GameObject youlose;
    public GameObject youDraw;

    public GameObject ThrowHands;
    
    public GameObject seta;
    public GameObject RematchBox;
    public GameObject healthBar;
    private const float stepSeta = 580;
    private const int setavel = 100;
    private InputMaster controls;
    private int setapos;
    private float posxseta = 0;
    private int dirseta = 1;
    private bool gameEnded = false;
    private bool p1AcceptRematch = false;
    private bool p2AcceptRematch = false;

    public void createGame()
    {
        controls.StaticScene.Disable();
        counter.GetComponent<Timer>().timerIsRunning = true;
        // AQUI TEM QUE TER 0 THROW ARMS E DEPOIS QUE DE FATO COMECA O JOGO
        //Player1
        playerPrefab.GetComponent<PlayerStatus>().lifeHost = lifeHost;
        playerPrefab.GetComponent<PlayerStatus>().lifeClient = lifeClient;

        playerPrefab.GetComponent<PlayerController>().Camera = CameraPriv;

        playerPrefab.GetComponent<PlayerStatus>().GameController = gameObject;
        playerPrefab.GetComponent<PlayerStatus>().playerType = PlayerType.Douglas;
        GameObject bola1 = BoltNetwork.Instantiate(playerPrefab, new Vector2(
            this.transform.position.x + (battleOffset * -1.5f),
                this.transform.position.y
                ), Quaternion.identity);

        //Player 2
        playerPrefab2.GetComponent<PlayerStatus>().lifeHost = lifeHost;
        playerPrefab2.GetComponent<PlayerStatus>().lifeClient = lifeClient;

        playerPrefab2.GetComponent<PlayerController>().Camera = CameraPriv;

        playerPrefab2.GetComponent<PlayerStatus>().GameController = gameObject;
        playerPrefab2.GetComponent<PlayerStatus>().playerType = PlayerType.Carlous;
        GameObject bola2 = BoltNetwork.Instantiate(playerPrefab2, new Vector2(
        this.transform.position.x + (battleOffset * 1.5f),
        this.transform.position.y
            ), Quaternion.identity);

        counter.GetComponent<Timer>().timerIsRunning = true;

        if (BoltNetwork.IsClient)
        {
            bola1.SetActive(false);
        }
        else
        {
            bola2.SetActive(false);
        }

        WaitingPlayer.SetActive(false);
        StartCoroutine(WaitCreateGame());
    }
    
    private IEnumerator WaitCreateGame()
    {
        ThrowHands.SetActive(true);
        ThrowHands.GetComponent<Animator>().Play("ThrowHandsCutIn");
        yield return new WaitForSeconds(1.82f);
        DouglasInstance.GetComponent<PlayerController>().enableCOntrols();
        CarlousInstance.GetComponent<PlayerController>().enableCOntrols();
        ThrowHands.SetActive(false);
    }

    private void Awake()
    {
        controls = new InputMaster();

        controls.StaticScene.Move.performed += ctx =>
        {
            Vector2 movesetadir = ctx.ReadValue<Vector2>();

            if(movesetadir.x == 1)
            {
                setapos = 1;
            }
            else if(movesetadir.x == -1)
            {
                setapos = 0;
            }
        };

        controls.StaticScene.Select.performed += _ =>
        {
            gameEnded = false;

            if(setapos == 0)
            {
                Rematch();
            }
            else if(setapos == 1)
            {
                LeaveButton();
            }
        };
    }

    private void Start()
    {
        controls.StaticScene.Disable();

        CameraPriv = Camera;

        roomName.GetComponent<Text>().text = PlayerPrefs.GetString("roomName");
    }

    [Obsolete]
    public override void SceneLoadLocalDone(string scene)
    {
        if (BoltNetwork.IsClient)
        {
            ClientLogged.Create().Send();
        }
    }

    public override void OnEvent(ClientLogged evnt)
    {
        //gameStarted = true;

        StartCoroutine(noia());
    }

    public override void OnEvent(P1Rematch evnt)
    {
        p1AcceptRematch = true;
    }

    public override void OnEvent(P2Rematch evnt)
    {
        p2AcceptRematch = true;
    }

    public void OpenRematchBox()
    {
        RematchBox.SetActive(true);
    }

    private void Restart()
    {
        BoltNetwork.Destroy(DouglasInstance);
        BoltNetwork.Destroy(CarlousInstance);

        youwin.SetActive(false);
        youlose.SetActive(false);
        youDraw.SetActive(false);
        p1AcceptRematch = false;
        p2AcceptRematch = false;
        RematchBox.SetActive(false);
        lifeHost.SetActive(true);
        lifeClient.SetActive(true);
        healthBar.SetActive(true);
        counter.SetActive(true);

        // AQUI TEM QUE TER DE NOVO O 3 2 1
        // E O THROW ARMS
        createGame();
    }

    public void Rematch()
    {
        if (!BoltNetwork.IsClient)
        {
            P1Rematch.Create().Send();
        }
        else
        {
            P2Rematch.Create().Send();
        }
    }

    public void LeaveButton()
    {
        Leave.Create().Send();
    }

    // EVENT FOR LEAVE THE GAME 
    public override void OnEvent(Leave evnt)
    {
        BoltLauncher.Shutdown();
        SceneManager.LoadScene("SampleScene");
    }

    private void Update()
    {     

        if (gameEnded)
        {            
            posxseta += setavel * Time.deltaTime * dirseta;

            if(posxseta > 30)
            {
                dirseta = -1;
            }
            else if(posxseta < 0)
            {
                dirseta = 1;
            }

            seta.GetComponent<RectTransform>().localPosition = new Vector3(-300 + posxseta + setapos * stepSeta, -440, 0);

        }
       

        if (GameObject.FindGameObjectWithTag("carlous") != null && CarlousInstance == null)
        {
            CarlousInstance = GameObject.FindGameObjectWithTag("carlous");
        }

        if (GameObject.FindGameObjectWithTag("douglas") != null && DouglasInstance == null)
        {
            DouglasInstance = GameObject.FindGameObjectWithTag("douglas");
        }

        if (DouglasInstance != null && CarlousInstance != null)
        {
            if (DouglasInstance.transform.position.x > CarlousInstance.transform.position.x)
            {
                if (!CarlousInstance.GetComponent<PlayerStatus>().isFlipped && !DouglasInstance.GetComponent<PlayerStatus>().isFlipped)
                {
                    Flip();
                }
            }

            if (CarlousInstance.transform.position.x > DouglasInstance.transform.position.x)
            {
                if (CarlousInstance.GetComponent<PlayerStatus>().isFlipped && DouglasInstance.GetComponent<PlayerStatus>().isFlipped)
                {
                    Flip();
                }
            }
        }
        

        if (p1AcceptRematch && p2AcceptRematch)
        {
            Restart();
        }

        if (!counter.GetComponent<Timer>().timerIsRunning)
        {
            CheckDraw();
        }
    }

    private void CheckDraw()
    {
        if (DouglasInstance.GetComponent<PlayerStatus>().state.Health > CarlousInstance.GetComponent<PlayerStatus>().state.EnemyHealth)
        {
            endGame(true, false);
        }
        else if (DouglasInstance.GetComponent<PlayerStatus>().state.Health < CarlousInstance.GetComponent<PlayerStatus>().state.EnemyHealth)
        {
            endGame(false, false);
        }
        else
        {
            endGame(false, true);
        }
    }

    public void endGame(bool hostWon, bool draw)
    {
        //BoltNetwork.Destroy(DouglasInstance);
        //BoltNetwork.Destroy(CarlousInstance);

        if (draw)
        {
            youDraw.SetActive(true);
        }
        else
        {
            if (BoltNetwork.IsClient)
            {
                if (hostWon)
                {
                    youlose.SetActive(true);
                }
                else
                {
                    youwin.SetActive(true);
                }
            }
            else
            {
                if (hostWon)
                {
                    youwin.SetActive(true);
                }
                else
                {
                    youlose.SetActive(true);
                }
            }
        }
        

        RematchBox.SetActive(true);
        lifeHost.SetActive(false);
        lifeClient.SetActive(false);
        healthBar.SetActive(false);
        counter.SetActive(false);
        counter.GetComponent<Timer>().timerIsRunning = false;
        counter.GetComponent<Timer>().timeRemaining = PlayerPrefs.GetFloat("gameDuration");

        controls.StaticScene.Enable();

        DouglasInstance.GetComponent<PlayerController>().disableControls();
        CarlousInstance.GetComponent<PlayerController>().disableControls();

        GameObject[] limbs = GameObject.FindGameObjectsWithTag("limb");

        foreach (GameObject limb in limbs)
        {

            BoltNetwork.Destroy(limb);
        }

        gameEnded = true;
    }

    private void Flip()
    {
        Vector3 newScaleDouglas = DouglasInstance.transform.localScale;
        newScaleDouglas.x *= -1;
        DouglasInstance.transform.localScale = newScaleDouglas;
        DouglasInstance.GetComponent<PlayerStatus>().isFlipped = !DouglasInstance.GetComponent<PlayerStatus>().isFlipped;

        Vector3 newScaleCarlous = CarlousInstance.transform.localScale;
        newScaleCarlous.x *= -1;
        CarlousInstance.transform.localScale = newScaleCarlous;
        CarlousInstance.GetComponent<PlayerStatus>().isFlipped = !CarlousInstance.GetComponent<PlayerStatus>().isFlipped;
    }

    private IEnumerator noia()
    {
        yield return new WaitForSeconds(2.0f);
        createGame();
    }

    private IEnumerator QuitWait()
    {
        yield return new WaitForSeconds(2.0f);
        WaitingPlayer.SetActive(false);

    }

    
}

//PHOTON NETWORK

//public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
//{
//    foreach (var session in sessionList)
//    {
//        UdpSession photonSession = session.Value as UdpSession;
//        Debug.Log(photonSession.ConnectionsCurrent);
//    }
//}

//{
//    public GameObject PlayerPrefab;
//    public GameObject GameCanvas;
//    public GameObject SceneCamera;

//    private void Start()
//    {
//        SpawnPlayer();
//    }

//    public void SpawnPlayer()
//    {
//        float randomValue = Random.Range(-1.5f, 1.5f);
//        PlayerPrefab.GetComponent<PlayerController>().PlayerCamera = SceneCamera;
//        SceneCamera.SetActive(false);
//        PhotonNetwork.Instantiate(
//            PlayerPrefab.name,
//            new Vector2(
//                this.transform.position.x * randomValue,
//                this.transform.position.y
//                ),
//            Quaternion.identity,
//            0
//        );
//    }
//}