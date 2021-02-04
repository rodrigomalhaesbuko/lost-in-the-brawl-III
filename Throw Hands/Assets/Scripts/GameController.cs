﻿using System.Collections;
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
    public GameObject roomName;

    public GameObject playerPrefab;
    public GameObject playerPrefab2;

    public GameObject lifeHost;
    public GameObject lifeClient;

    public GameObject RematchBox;
    public GameObject Camera;

    private GameObject CameraPriv;
    
    private GameObject DouglasInstance;
    private GameObject CarlousInstance;

    public GameObject WaitingPlayer;

    private bool gameStarted = false;

    public float battleOffset = 5f;

    private bool p1AcceptRematch = false;

    private bool p2AcceptRematch = false;

    public void createGame()
    {
        // AQUI TEM QUE TER 0 THROW ARMS E DEPOIS QUE DE FATO COMECA O JOGO

        WaitingPlayer.SetActive(false);

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

        if (BoltNetwork.IsClient)
        {
            bola1.SetActive(false);
        }
        else
        {
            bola2.SetActive(false);
        }
    }

    private void Start()
    {
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
        p1AcceptRematch = false;
        p2AcceptRematch = false;
        RematchBox.SetActive(false);
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
                if(CarlousInstance.GetComponent<PlayerStatus>().isFlipped && DouglasInstance.GetComponent<PlayerStatus>().isFlipped)
                {
                    Flip();
                }
            }
        }

        if (false)
        {
            gameStarted = false;
            createGame();

            StartCoroutine(noia());
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