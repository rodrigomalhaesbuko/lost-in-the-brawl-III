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

    public float battleOffset = 5f;
    [Obsolete]

    private void Start()
    {
        CameraPriv = Camera;
        hostSliderPriv = hostSlider;
        clientSliderPriv = clientSlider;
    }

    [Obsolete]
    public override void SceneLoadLocalDone(string scene)
    {
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

        if (BoltMatchmaking.CurrentSession.ConnectionsCurrent == 1)
        {

            bola2.SetActive(false);

        }
        else if(BoltMatchmaking.CurrentSession.ConnectionsCurrent == 2)
        {
            bola1.SetActive(false);
        }


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

    public void Rematch()
    {
    }

    public void Leave()
    {
        BoltLauncher.Shutdown();
        SceneManager.LoadScene("SampleScene");
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
