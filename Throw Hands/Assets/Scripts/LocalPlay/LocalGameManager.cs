using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class LocalGameManager : MonoBehaviour
{
    public GameState gameState;

    public GameObject counter;
    public GameObject roomName;

    public GameObject playerPrefab;
    public GameObject playerPrefab2;

    public GameObject lifeHost;
    public GameObject lifeClient;

    public GameObject DouglasInstance;
    public GameObject CarlousInstance;

    public GameObject WaitingPlayer;

    private bool gameStarted = false;

    public float battleOffset = 5f;

    public AudioSource bgm;

    //CAMERA
    public CinemachineTargetGroup targetGroup;

    //endbattle vars
    public GameObject hostWins;
    public GameObject clientWins;
    private int hostScore = 0;
    private int clientScore = 0;

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
    private bool p1AcceptRematch = false;
    private bool p2AcceptRematch = false;

    public static bool armFlip = false;

    public float slowSeconds = 4.0f;

    private float slowTimer = 0f;


    private void Awake()
    {
        controls = new InputMaster();

        controls.StaticScene.Move.performed += ctx =>
        {
            if (gameState == GameState.rematch)
            {
                Vector2 movesetadir = ctx.ReadValue<Vector2>();

                if (movesetadir.x == 1)
                {
                    setapos = 1;
                }
                else if (movesetadir.x == -1)
                {
                    setapos = 0;
                }
            }
        };

        controls.StaticScene.Select.performed += _ =>
        {
            if (gameState == GameState.rematch)
            {
                gameState = GameState.rematchClick;

                if (setapos == 0)
                {
                    RestartGame();
                }
                else if (setapos == 1)
                {
                    SceneManager.LoadScene("SampleScene");
                }
            }
        };
    }

    void CreateGame()
    {
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

        DouglasInstance = BoltNetwork.Instantiate(playerPrefab, new Vector2(
                this.transform.position.x + (battleOffset * -1.5f), this.transform.position.y), Quaternion.identity);

        CarlousInstance = BoltNetwork.Instantiate(playerPrefab2, new Vector2(
                this.transform.position.x + (battleOffset * 1.5f), this.transform.position.y), Quaternion.identity);


        targetGroup.AddMember(DouglasInstance.transform, 1f, 4f);
        targetGroup.AddMember(CarlousInstance.transform, 1f, 4f);

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
        gameState = GameState.play;
        DouglasInstance.GetComponent<PlayerController>().enableControls();
        CarlousInstance.GetComponent<PlayerController>().enableControls();
        ThrowHands.SetActive(false);
    }


    private void RestartGame()
    {
        GameObject[] limbs = GameObject.FindGameObjectsWithTag("limb");

        foreach (GameObject limb in limbs)
        {
            Destroy(limb);
        }


        if (DouglasInstance != null)
            Destroy(DouglasInstance);
   
        if (CarlousInstance != null)
            Destroy(CarlousInstance);
    
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
        counter.SetActive(true);
        counter.GetComponent<Timer>().timeRemaining = PlayerPrefs.GetFloat("gameDuration");

        audioControl.audioSource.Stop();
        controls.StaticScene.Disable();
        Time.timeScale = 1f;

        CreateGame();
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

        //// jump
        //if (armFlip)
        //{

        //    CarlousInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2( -5f , 0f), ForceMode2D.Impulse);
        //    //DouglasInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10f), ForceMode2D.Impulse);
        //}
        //else
        //{
        //    //CarlousInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10f), ForceMode2D.Impulse);
        //    DouglasInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(5f, 0f), ForceMode2D.Impulse);
        //}


        armFlip = !armFlip;
    }

    private void Update()
    {
        if (gameState == GameState.rematch)
        {
            posxseta += setavel * Time.deltaTime * dirseta;

            if (posxseta > 30)
            {
                dirseta = -1;
            }
            else if (posxseta < 0)
            {
                dirseta = 1;
            }

            seta.GetComponent<RectTransform>().localPosition = new Vector3(-443 + posxseta + setapos * stepSeta, -440, 0);
        }

        if (gameState == GameState.play)
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

        if (gameState == GameState.slowed)
        {
            Time.timeScale = .05f;

            slowTimer += Time.unscaledDeltaTime;

            if (slowTimer > slowSeconds)
            {
                Time.timeScale = 1.0f;
            }

        }

        Time.fixedDeltaTime = Time.timeScale * .02f;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        audioControl.audioSource.pitch = Time.timeScale;
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

        if (DouglasInstance != null)
        {
            DouglasInstance.GetComponent<PlayerController>().disableControls();
        }

        if (CarlousInstance != null)
        {
            CarlousInstance.GetComponent<PlayerController>().disableControls();
        }

        yield return new WaitForSeconds(1.2f);

        audioControl.audioSource.Stop();
        audioControl.PlaySound(SFXType.End);

        if (draw)
        {
            youDraw.SetActive(true);
            audioControl.PlaySound(SFXType.Draw);
        }

        if (hostWon)
            hostScore++;
        else
            clientScore++;

        hostWins.GetComponent<Text>().text = hostScore.ToString();
        clientWins.GetComponent<Text>().text = clientScore.ToString();

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
        if (gameState != GameState.slowed)
        {
            gameState = GameState.slowed;
            StartCoroutine(afterEndGame(hostWon, draw));
        }
    }

}
