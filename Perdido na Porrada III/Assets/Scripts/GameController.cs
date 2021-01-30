using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class GameController : GlobalEventListener
{
    public GameObject playerPrefab;

    [System.Obsolete]
    public override void SceneLoadLocalDone(string scene)
    {
        float randomValue = Random.Range(-3.5f, 3.5f);
        BoltNetwork.Instantiate(playerPrefab, new Vector2(
                this.transform.position.x * randomValue,
                this.transform.position.y
                ), Quaternion.identity
        );
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
