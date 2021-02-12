using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bolt;
using UdpKit;
using System;
using Bolt.Matchmaking;
using UnityEngine.UI;
using Cinemachine;

public class GameController : GlobalEventListener
{
    public GameState gameState;

    public GameObject counter;
    public GameObject roomName;

    public GameObject playerPrefab;
    public GameObject playerPrefab2;

    public GameObject lifeHost;
    public GameObject lifeClient;
    
    private GameObject DouglasInstance;
    private GameObject CarlousInstance;

    public GameObject WaitingPlayer;

    private bool gameStarted = false;

    public float battleOffset = 5f;

    public AudioSource bgm;

    //CAMERA
    public CinemachineTargetGroup targetGroup;

    //endbattle vars
    public GameObject youwin;
    public GameObject youlose;
    public GameObject youDraw;

    public GameObject ThrowHands;

    public GameObject P1;
    public GameObject P2;

    public GameObject seta;
    public GameObject RematchBox;
    public GameObject healthBar;
    private const float stepSeta = 580;
    private const int setavel = 100;
    private InputMaster controls;
    private int setapos;
    private float posxseta = 0;
    private int dirseta = 1;
    private bool p1AcceptRematch = false;
    private bool p2AcceptRematch = false;
    
    public static bool armFlip = false;

    public float slowSeconds = 4.0f;

    private float slowTimer = 0f;

    private void getInstances()
    {
        CarlousInstance = GameObject.FindGameObjectWithTag("carlous");
        targetGroup.AddMember(CarlousInstance.transform, 1f, 4f);

        DouglasInstance = GameObject.FindGameObjectWithTag("douglas");
        targetGroup.AddMember(DouglasInstance.transform, 1f, 4f);
    }

    public void createGame()
    {
        //gameEnded = false;

        armFlip = false;

        gameState = GameState.intro;

        audioControl.PlaySound(SFXType.Intro);

        if (GameObject.FindGameObjectWithTag("musicMenu") != null)
        {
            GameObject.FindGameObjectWithTag("musicMenu").GetComponent<AudioSource>().Stop();
        }        
        
        //Player 1
        playerPrefab.GetComponent<PlayerStatus>().lifeHost = lifeHost;
        playerPrefab.GetComponent<PlayerStatus>().lifeClient = lifeClient;
        playerPrefab.GetComponent<PlayerStatus>().GameController = gameObject;
        playerPrefab.GetComponent<PlayerStatus>().playerType = PlayerType.Douglas;

        

        //Player 2
        playerPrefab2.GetComponent<PlayerStatus>().lifeHost = lifeHost;
        playerPrefab2.GetComponent<PlayerStatus>().lifeClient = lifeClient;
        playerPrefab2.GetComponent<PlayerStatus>().GameController = gameObject;
        playerPrefab2.GetComponent<PlayerStatus>().playerType = PlayerType.Carlous;


        //Bug
        GameObject bola1 = BoltNetwork.Instantiate(playerPrefab, new Vector2(
            this.transform.position.x + (battleOffset * -1.5f),
                this.transform.position.y
                ), Quaternion.identity);


        GameObject bola2 = BoltNetwork.Instantiate(playerPrefab2, new Vector2(
        this.transform.position.x + (battleOffset * 1.5f),
        this.transform.position.y
            ), Quaternion.identity);

        if (BoltNetwork.IsClient)
        {
            //bola1.SetActive(false);
            BoltNetwork.Destroy(bola1);
        }
        else
        {
            //bola2.SetActive(false);
            BoltNetwork.Destroy(bola2);
        }
        //endBug

        WaitingPlayer.SetActive(false);
        bgm.Play();
        StartCoroutine(WaitCreateGame());
    }
    
    private IEnumerator WaitCreateGame()
    {
        ThrowHands.SetActive(true);
        ThrowHands.GetComponent<Animator>().Play("ThrowHandsCutIn");
        yield return new WaitForSeconds(1.82f);
        counter.GetComponent<Timer>().timerIsRunning = true;
        getInstances();
        gameState = GameState.play;
        DouglasInstance.GetComponent<PlayerController>().enableControls();
        CarlousInstance.GetComponent<PlayerController>().enableControls();
        ThrowHands.SetActive(false);
    }

    private void Awake()
    {
        controls = new InputMaster();

        controls.StaticScene.Move.performed += ctx =>
        {
            if(gameState == GameState.rematch)
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
            }
        };

        controls.StaticScene.Select.performed += _ =>
        {
            if(gameState == GameState.rematch)
            {
                gameState = GameState.rematchClick;

                if(setapos == 0)
                {
                    Rematch();
                }
                else if(setapos == 1)
                {
                    LeaveButton();
                }
            }
        };
    }

    private void Start()
    {
        controls.StaticScene.Disable();
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
        StartCoroutine(noia());
    }

    private IEnumerator noia()
    {
        yield return new WaitForSeconds(.5f);
        createGame();
    }

    public void OpenRematchBox()
    {
        RematchBox.SetActive(true);
    }

    private IEnumerator RestartGameCourotine()
    {
        GameObject[] limbs = GameObject.FindGameObjectsWithTag("limb");

        foreach (GameObject limb in limbs)
        {
            BoltNetwork.Destroy(limb);
        }

        if (BoltNetwork.IsClient)
        {
            if(CarlousInstance != null)
            {
               BoltNetwork.Destroy(CarlousInstance);
            }
          
        }
        else
        {
            if (DouglasInstance != null)
            {
                BoltNetwork.Destroy(DouglasInstance);
            }
        }

        yield return new WaitForSeconds(0.2f);

        RestartGame();
    }

    private void RestartGame()
    {
        gameState = GameState.restart;
        slowTimer = 0f;
        Time.timeScale = 1f;
        youwin.SetActive(false);
        youlose.SetActive(false);
        youDraw.SetActive(false);
        p1AcceptRematch = false;
        p2AcceptRematch = false;
        RematchBox.SetActive(false);
        lifeHost.SetActive(true);
        lifeClient.SetActive(true);
        healthBar.SetActive(true);
        P1.SetActive(false);
        P2.SetActive(false);
        counter.SetActive(true);
        counter.GetComponent<Timer>().timeRemaining = PlayerPrefs.GetFloat("gameDuration");

        audioControl.audioSource.Stop();
        controls.StaticScene.Disable();
        Time.timeScale = 1f;

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

    public override void OnEvent(P1Rematch evnt)
    {
        p1AcceptRematch = true;
        P1.SetActive(true);
        if (p2AcceptRematch)
        {
           Restart.Create().Send();
        }

    }

    public override void OnEvent(P2Rematch evnt)
    {
        
        p2AcceptRematch = true;
        P2.SetActive(true);
        if (p1AcceptRematch)
        {
           Restart.Create().Send();
        }

    }

    public override void OnEvent(Restart evnt)
    {
        StartCoroutine(RestartGameCourotine());
    }

    public void LeaveButton()
    {
        Leave.Create().Send();
    }

    // EVENT FOR LEAVE THE GAME 
    public override void OnEvent(Leave evnt)
    {
        controls.StaticScene.Disable();
        BoltLauncher.Shutdown();
        SceneManager.LoadScene("SampleScene");
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

        armFlip = !armFlip;
    }


    private void Update()
    {
        if (gameState == GameState.rematch)
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
       
        if (gameState == GameState.play)
        {
            if(BoltMatchmaking.CurrentSession.ConnectionsCurrent == 1)
            {
                LeaveButton();
            }

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

        if(gameState == GameState.slowed)
        {
            Time.timeScale = .05f;

            slowTimer += Time.unscaledDeltaTime;

            if(slowTimer > slowSeconds)
            {
                Time.timeScale = 1.0f;
            }

        }

        Time.fixedDeltaTime = Time.timeScale * .02f;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        audioControl.audioSource.pitch = Time.timeScale;
    }

    public override void Disconnected(BoltConnection connection)
    {
        controls.StaticScene.Disable();
        SceneManager.LoadScene("SampleScene");
    }

    public void CheckDraw()
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

    private void MakeSlowMotion()
    {
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    private IEnumerator afterEndGame(bool hostWon, bool draw)
    {
        MakeSlowMotion();

        if(DouglasInstance != null)
        {
            DouglasInstance.GetComponent<PlayerController>().disableControls();
        }

        if(CarlousInstance != null)
        {
            CarlousInstance.GetComponent<PlayerController>().disableControls();
        }
       
        yield return new  WaitForSeconds(1.2f);

        audioControl.audioSource.Stop();
        audioControl.PlaySound(SFXType.End);

        if (draw)
        {
            youDraw.SetActive(true);
            audioControl.PlaySound(SFXType.Draw);
        }
        else
        {
            if (BoltNetwork.IsClient)
            {
                if (hostWon)
                {
                    youlose.SetActive(true);
                    audioControl.PlaySound(SFXType.Lose);
                }
                else
                {
                    youwin.SetActive(true);
                    audioControl.PlaySound(SFXType.Win);
                }
            }
            else
            {
                if (hostWon)
                {
                    youwin.SetActive(true);
                    audioControl.PlaySound(SFXType.Win);
                }
                else
                {
                    youlose.SetActive(true);
                    audioControl.PlaySound(SFXType.Lose);
                }
            }
        }

        RematchBox.SetActive(true);
        lifeHost.SetActive(false);
        lifeClient.SetActive(false);
        healthBar.SetActive(false);
        counter.SetActive(false);
        counter.GetComponent<Timer>().timerIsRunning = false;

        controls.StaticScene.Enable();

        gameState = GameState.rematch;

    }

    public void endGame(bool hostWon, bool draw)
    {
        gameState = GameState.slowed;
        StartCoroutine(afterEndGame(hostWon, draw));
    }
}


public enum GameState
{
    intro,            // Rodando a animação throw hands
    play,            // Jogando o jogo
    rematch,        // Tela de pós jogo
    rematchClick,  // Clicou na tela de pós jogo
    restart,      // Restartando o jogo
    slowed       // Slowmotion do fim de jogo
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