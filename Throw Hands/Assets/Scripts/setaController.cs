﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class setaController : MonoBehaviour
{
    InputMaster controls;
    int pos = 0;

    float posx = -790f;
    float posy = 80f;

    int dir = 1;

    const int dist = 50;
    float vel = dist * 1.5f;

    void Awake()
    {
        controls = new InputMaster();
        controls.MainMenu.Move.performed += ctx => Move( ctx.ReadValueAsButton() );

        controls.MainMenu.Select.performed += _ => go();

        //controls.MainMenu.Move.performed += _ => Debug.Log("Mexeu");
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
            //create/join room
            SceneManager.LoadScene("SampleScene");
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
        Debug.Log("bla");

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
