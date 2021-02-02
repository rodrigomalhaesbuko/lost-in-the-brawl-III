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

    public PlayerType playerType;

    // void Start
    public override void Attached()
    {
        state.Health = localHealth;
        state.EnemyHealth = localHealth;
        state.AddCallback("Health", HealthCallBack);
        state.AddCallback("EnemyHealth", EnemyHealthCallBack);
    }


    private void HealthCallBack()
    {
        localHealth = state.Health;

        Debug.Log("FROM Host");
        Debug.Log("host");
        Debug.Log(state.Health);
        Debug.Log("Client");
        Debug.Log(state.EnemyHealth);

        hostSlider.GetComponent<Slider>().value = 0.20f * state.Health;

        if (localHealth <= 0)
        {
            Debug.Log("GameOver Player 1 ganhou");
            GameController.GetComponent<GameController>().OpenRematchBox();
        }
    }

    private void EnemyHealthCallBack()
    {
        localHealth = state.EnemyHealth;
        //Debug.Log(state.Health);
        //Debug.Log(state.EnemyHealth);
        //Debug.Log(localHealth);
        Debug.Log("FROM ENEMY");
        Debug.Log("host");
        Debug.Log(state.Health);
        Debug.Log("Client");
        Debug.Log(state.EnemyHealth);

        clientSlider.GetComponent<Slider>().value = 0.20f * state.EnemyHealth;

        if (localHealth <= 0)
        {
            Debug.Log("GameOver Player 2 ganhou");
            GameController.GetComponent<GameController>().OpenRematchBox();
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        if (BoltNetwork.IsClient)
    //        {
    //            state.EnemyHealth -= 1;
    //        }
    //        else
    //        {
    //            state.Health -= 1;
    //        }

    //    }
    //}

    public void TakeDamage()
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

public enum PlayerType { 
    Carlous,
    Douglas
}
