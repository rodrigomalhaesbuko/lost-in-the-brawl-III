using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bolt;
using UdpKit;
using System;
using Bolt.Matchmaking;

public class GameController : GlobalEventListener
{
    public GameObject playerPrefab;
    public GameObject playerPrefab2;
    public GameObject hostSlider;
    public GameObject clientSlider;
    
    public GameObject RematchBox;
    public GameObject Camera;

    private GameObject CameraPriv;
    private GameObject hostSliderPriv;
    private GameObject clientSliderPriv;

    private GameObject DouglasInstance;
    private GameObject CarlousInstance;

    public GameObject WaitingPlayer;

    private bool gameStarted = false;

    public float battleOffset = 5f;

    private bool p1AcceptRematch = false;

    private bool p2AcceptRematch = false;

    public void createGame()
    {
        Debug.Log("JOGO CRIADO");
        //StartCoroutine(QuitWait());
        // AQUI TEM QUE TER 0 THROW ARMS E DEPOIS QUE DE FATO COMECA O JOGO
        // DA PARA FAZER ISSO COM CORROUTINA
        WaitingPlayer.SetActive(false);
        playerPrefab.GetComponent<PlayerStatus>().clientSlider = clientSliderPriv;
        playerPrefab.GetComponent<PlayerStatus>().hostSlider = hostSliderPriv;
        playerPrefab.GetComponent<PlayerController>().Camera = CameraPriv;

        playerPrefab.GetComponent<PlayerStatus>().GameController = gameObject;
        playerPrefab.GetComponent<PlayerStatus>().playerType = PlayerType.Douglas;
        GameObject bola1 = BoltNetwork.Instantiate(playerPrefab, new Vector2(
            this.transform.position.x + (battleOffset * -1.5f),
                this.transform.position.y
                ), Quaternion.identity);
        playerPrefab2.GetComponent<PlayerStatus>().clientSlider = clientSliderPriv;
        playerPrefab2.GetComponent<PlayerStatus>().hostSlider = hostSliderPriv;
        playerPrefab2.GetComponent<PlayerController>().Camera = CameraPriv;

        playerPrefab2.GetComponent<PlayerStatus>().GameController = gameObject;
        playerPrefab2.GetComponent<PlayerStatus>().playerType = PlayerType.Carlous;
        GameObject bola2 = BoltNetwork.Instantiate(playerPrefab2, new Vector2(
        this.transform.position.x + (battleOffset * 1.5f),
        this.transform.position.y
            ), Quaternion.identity);

        if (!BoltNetwork.IsClient)
        {
            bola2.SetActive(false);
        }
        else
        {
            bola1.SetActive(false);
        }
    }

    private void Start()
    {
        CameraPriv = Camera;
        hostSliderPriv = hostSlider;
        clientSliderPriv = clientSlider;
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
        //createGame();
        gameStarted = true;
        Debug.Log("client logged");
    }


    public override void OnEvent(P1Rematch evnt)
    {
        p1AcceptRematch = true;
    }

    public override void OnEvent(P2Rematch evnt)
    {
        p2AcceptRematch = true;
    }

    //public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    //{
    //    foreach (var session in sessionList)
    //    {
    //        UdpSession photonSession = session.Value as UdpSession;
    //        Debug.Log(photonSession.ConnectionsCurrent);
    //    }
    //}

    public void OpenRematchBox()
    {
        RematchBox.SetActive(true);
    }

    private void Restart()
    {
        BoltNetwork.Destroy(DouglasInstance);
        BoltNetwork.Destroy(CarlousInstance);
        p1AcceptRematch = false;
        p2AcceptRematch = false;
        RematchBox.SetActive(false);
        // AQUI TEM QUE TER DE NOVO O 3 2 1
        // E O THROW ARMS 
        createGame();
    }

    public void Rematch()
    {
        if (BoltNetwork.IsClient)
        {
            P2Rematch.Create().Send();
        }
        else
        {
            P1Rematch.Create().Send();
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
        if (GameObject.FindGameObjectWithTag("carlous") != null && CarlousInstance == null)
        {
            Debug.Log("ENTROU INSTANCE CARLOUS");
            CarlousInstance = GameObject.FindGameObjectWithTag("carlous");
        }

        if (GameObject.FindGameObjectWithTag("douglas") != null && DouglasInstance == null)
        {
            Debug.Log("ENTROU INSTANCE DOUGLAS");
            DouglasInstance = GameObject.FindGameObjectWithTag("douglas");

            DouglasInstance.GetComponent<PlayerStatus>().clientSlider = clientSliderPriv;
            DouglasInstance.GetComponent<PlayerStatus>().hostSlider = hostSliderPriv;
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
                if(CarlousInstance.GetComponent<PlayerStatus>().isFlipped && DouglasInstance.GetComponent<PlayerStatus>().isFlipped)
                {
                    Flip();
                }
            }
        }

        if(DouglasInstance != null)
        {
            DouglasInstance.GetComponent<PlayerStatus>().clientSlider = clientSliderPriv;
            DouglasInstance.GetComponent<PlayerStatus>().hostSlider = hostSliderPriv;

            Debug.LogWarning(DouglasInstance.GetComponent<PlayerStatus>().clientSlider == null);
        }

        if (gameStarted)
        {
            gameStarted = false;
            createGame();
        }

        if(p1AcceptRematch && p2AcceptRematch)
        {
            Restart();
        }
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

    private IEnumerator QuitWait()
    {
        yield return new WaitForSeconds(2.0f);
        WaitingPlayer.SetActive(false);

    }
}



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
