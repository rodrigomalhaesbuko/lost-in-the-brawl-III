using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Bolt;
using System;
using Bolt.Matchmaking;
using UdpKit;

public class LocalGameLoader : GlobalEventListener
{

    public void LoadLocalGame()
    {
        BoltLauncher.StartServer();
    }

    public override void BoltStartDone()
    {
        Debug.Log("VAI ROLAR UM ANIME ");
        BoltMatchmaking.CreateSession(sessionID: UnityEngine.Random.Range(-10f, 10f).ToString(), sceneToLoad: "LocalTest");
        Destroy(gameObject);
    }

}
