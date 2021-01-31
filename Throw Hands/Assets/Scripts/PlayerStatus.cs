using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UnityEngine.UI;

public class PlayerStatus : Bolt.EntityBehaviour<ICustomPlayerState>
{
    public int localHealth = 5;
    public GameObject hostSlider;
    public GameObject clientSlider;
    public GameObject GameController;

    public GameObject currentSlider;

    // void Start
    public override void Attached()
    {
        state.Health = localHealth;
        state.EnemyHealth = localHealth;
        state.Color = gameObject.GetComponent<SpriteRenderer>().color;
        state.AddCallback("Health", HealthCallBack);
        state.AddCallback("EnemyHealth", EnemyHealthCallBack);
        state.AddCallback("Color", DamageCallBack);
    }

    private void DamageCallBack()
    {
        gameObject.GetComponent<SpriteRenderer>().color = state.Color;
    }

    private void HealthCallBack()
    {
        localHealth = state.Health;

        Debug.Log("host");
        Debug.Log(state.Health);
        Debug.Log("Client");
        Debug.Log(state.EnemyHealth);

        hostSlider.GetComponent<Slider>().value = 0.20f * state.Health;

        if (localHealth <= 0)
        {
            Debug.Log("GameOver Player 1 ganhou");
            state.Color = Color.red;
            GameController.GetComponent<GameController>().OpenRematchBox();
        }
    }

    private void EnemyHealthCallBack()
    {
        localHealth = state.EnemyHealth;

        Debug.Log("host");
        Debug.Log(state.Health);
        Debug.Log("Client");
        Debug.Log(state.EnemyHealth);

        clientSlider.GetComponent<Slider>().value = 0.20f * state.EnemyHealth;

        if (localHealth <= 0)
        {
            Debug.Log("GameOver Player 2 ganhou");
            state.Color = Color.red;
            GameController.GetComponent<GameController>().OpenRematchBox();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (BoltNetwork.IsClient)
            {
                state.EnemyHealth -= 1;
                
            }
            else
            {
                state.Health -= 1;
            }
                
        }
    }

}
