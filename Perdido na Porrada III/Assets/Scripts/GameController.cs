using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject GameCanvas;
    public GameObject SceneCamera;

    private void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        float randomValue = Random.Range(-1.5f, 1.5f);
        PlayerPrefab.GetComponent<PlayerController>().PlayerCamera = SceneCamera;

        PhotonNetwork.Instantiate(
            PlayerPrefab.name,
            new Vector2(
                this.transform.position.x * randomValue,
                this.transform.position.y
                ),
            Quaternion.identity,
            0
        );

        SceneCamera.SetActive(false);
    }
}
