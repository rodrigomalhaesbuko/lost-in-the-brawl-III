using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bolt;

public class GameController : GlobalEventListener
{
    public GameObject playerPrefab;
    public GameObject hostSlider;
    public GameObject clientSlider;
    public GameObject RematchBox;

    [System.Obsolete]
    public override void SceneLoadLocalDone(string scene)
    {
        playerPrefab.GetComponent<PlayerStatus>().clientSlider = clientSlider;
        playerPrefab.GetComponent<PlayerStatus>().hostSlider = hostSlider;
        playerPrefab.GetComponent<PlayerStatus>().GameController = gameObject;
        float randomValue = Random.Range(-3.5f, 3.5f);
        BoltNetwork.Instantiate(playerPrefab, new Vector2(
                this.transform.position.x * randomValue,
                this.transform.position.y
                ), Quaternion.identity
        );
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
