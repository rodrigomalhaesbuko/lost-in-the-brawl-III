using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string versionName = "0.1";
    [SerializeField] private GameObject conectPanel;
    [SerializeField] private InputField CreateGameInput;
    [SerializeField] private InputField JoinGameInput;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(versionName);
    }

    void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("connected");
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { maxPlayers = 2 }, null);
        Debug.Log("obaaa");
    }

    public void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.maxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);

    }

    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameScene");
        Debug.Log("mudou de scena");
    }
}
