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
    public GameObject hostSlider;
    public GameObject clientSlider;
    public GameObject RematchBox;
    public GameObject Camera;

    public float battleOffset = 5f;
    [System.Obsolete]
    public override void SceneLoadLocalDone(string scene)
    {
        playerPrefab.GetComponent<PlayerStatus>().clientSlider = clientSlider;
        playerPrefab.GetComponent<PlayerStatus>().hostSlider = hostSlider;
        playerPrefab.GetComponent<PlayerStatus>().GameController = gameObject;
        playerPrefab.GetComponent<PlayerController>().Camera = Camera;
        if (!BoltNetwork.IsClient)
        {
            battleOffset *= -1;
            Camera.GetComponent<CameraHandler>().hostPositionX = playerPrefab;
        }
       GameObject realPlayer = BoltNetwork.Instantiate(playerPrefab, new Vector2(
                this.transform.position.x + battleOffset,
                this.transform.position.y
                ), Quaternion.identity
        );
        if (BoltMatchmaking.CurrentSession.ConnectionsCurrent == 1)
        {
            Camera.GetComponent<CameraHandler>().hostPositionX = realPlayer;
        }
        else if (BoltMatchmaking.CurrentSession.ConnectionsCurrent == 2)
        {
            Camera.GetComponent<CameraHandler>().clientPositionX = realPlayer;
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

    public void Update()
    {
        Debug.Log(BoltMatchmaking.CurrentSession.ConnectionsCurrent);
    }

    public void OpenRematchBox()
    {
        RematchBox.SetActive(true);
    }

    public void Rematch()
    {
        Debug.Log("Making Remacth");
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
