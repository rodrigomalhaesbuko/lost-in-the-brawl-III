﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 99;
    public bool timerIsRunning = false;
    public Text timeText;

    private void Start()
    {
        // Starts the timer automatically
        PlayerPrefs.SetFloat("gameDuration", timeRemaining);
        timerIsRunning = false;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime((int) timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                // Ver quem tem mais vida e acabar
            }
        }
    }

    void DisplayTime(int timeToDisplay)
    {
        timeToDisplay -= 1;

        if(timeToDisplay < 10)
        {
            timeText.text += '0';
        }

        timeText.text = timeToDisplay.ToString();
    }
}
