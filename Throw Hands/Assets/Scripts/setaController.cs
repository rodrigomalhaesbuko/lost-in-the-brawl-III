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

public class setaController : MonoBehaviour
{
    InputMaster controls;
    public GameObject loading;
    public bool playmodes = false; 
    int pos = 0;

    float posx = -790f;
    float posy = 80f;

    int dir = 1;

    const int dist = 50;
    float vel = dist * 1.5f;


    [SerializeField] private GameObject AlertBox;
    [SerializeField] private GameObject Disclaimer;
    public LocalGameLoader localGameLoader;

    void Awake()
    {
        controls = new InputMaster();
        controls.MainMenu.Enable();
        if (playmodes)
        {
            controls.MainMenu.Move.performed += ctx => MovePlayModes(ctx.ReadValueAsButton());

            controls.MainMenu.Select.performed += _ => goPlayModes();
        }
        else
        {
            controls.MainMenu.Move.performed += ctx => Move(ctx.ReadValueAsButton());

            controls.MainMenu.Select.performed += _ => go();
        }


        //controls.MainMenu.Move.performed += _ => Debug.Log("Mexeu");
    }

    private void Start()
    {
        loading.SetActive(false);
    }

    private void Move(bool m)
    {
        if (m)
            pos--;
        else
            pos++;

        if (pos > 3)
            pos = 3;
        else if (pos < 0)
            pos = 0;


        posy = 80 - 155 * pos;
        
    }

    void go()
    {
        if(pos == 0)
        {
            //Go to Playmodes
            controls.MainMenu.Disable();
            SceneManager.LoadScene("PlayModes");
        }

        if(pos == 1)
        {
            //config
            SceneManager.LoadScene("config");
        }

        if(pos == 2)
        {
            //credits
            SceneManager.LoadScene("credits");
        }

        if(pos == 3)
        {
            //quit
            Application.Quit();
            

        }

    }

    private void MovePlayModes(bool m)
    {
        if (m)
            pos--;
        else
            pos++;

        if (pos > 2)
            pos = 2;
        else if (pos < 0)
            pos = 0;


        posy = 80 - 155 * pos;

    }

    void goPlayModes()
    {
        if (pos == 0)
        {
            //create/join room

            loading.SetActive(true);
            localGameLoader.LoadLocalGame();
            controls.MainMenu.Disable();
            StartCoroutine(CannotConectCreateRoom());  
        }

        if (pos == 1)
        {
            controls.MainMenu.Disable();
            SceneManager.LoadScene("SampleScene");
        }

        if (pos == 2)
        {
            //initial screen 
            SceneManager.LoadScene("initialScreen");
        }

    }

    IEnumerator CannotConectCreateRoom()
    {

        yield return new WaitForSecondsRealtime(100.0f);

        BoltLauncher.Shutdown();
        OpenAlertBox();

    }


    public void OpenAlertBox()
    {
        Disclaimer.GetComponent<Text>().text = "In order to play this game you need to be connect with the internet";
        AlertBox.SetActive(true);
    }

    public void CloseAlertBox()
    {
        controls.MainMenu.Enable();
        pos = 0;
        AlertBox.SetActive(false);
        loading.SetActive(false);
    }

    public void Update()
    {
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(posx, posy, 0);

        if(posx < -850)
        {
            dir = 1;
        }

        if(posx > -800)
        {
            dir = -1;
        }

        posx += dir * Time.deltaTime * vel;
    }

    private void OnEnable()
    {
        controls.MainMenu.Enable();
    }

    private void OnDisable()
    {
        controls.MainMenu.Disable();
    }


}
