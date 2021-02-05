using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UnityEngine.UI;

public class PlayerStatus : Bolt.EntityBehaviour<ICustomPlayerState>
{
    public int localHealth = 5;
    public GameObject lifeHost;
    public GameObject lifeClient;

    public GameObject GameController;
    public bool isFlipped = false;

    public PlayerType playerType;

    private Color greenColor;
    private Color redColor;

    private void Start()
    {
        ColorUtility.TryParseHtmlString("#B8D19C", out greenColor);
        ColorUtility.TryParseHtmlString("#DB9DA1", out redColor);

        restartLife();
    }

    // void Start do Bolt
    public override void Attached()
    {
        state.Health = localHealth;
        state.EnemyHealth = localHealth;
        state.AddCallback("Health", HealthCallBack);
        state.AddCallback("EnemyHealth", EnemyHealthCallBack);
    }


    private void HealthCallBack()
    {
        lifeHost.transform.GetChild(state.Health).GetComponent<Image>().color = redColor;

        if (state.Health <= 0)
        {
            Debug.Log("GameOver Player 1 ganhou");
            GameController.GetComponent<GameController>().endGame(false, false);
        }

    }

    private void EnemyHealthCallBack()
    {
        lifeClient.transform.GetChild(state.EnemyHealth).GetComponent<Image>().color = redColor;

        if (state.EnemyHealth <= 0)
        {
            Debug.Log("GameOver Player 2 ganhou");
            GameController.GetComponent<GameController>().endGame(true, false);
        }
    }

    private void restartLife()
    {

        for(int i = 0; i<5; i++)
        {
            lifeClient.transform.GetChild(i).GetComponent<Image>().color = greenColor;
            lifeHost.transform.GetChild(i).GetComponent<Image>().color = greenColor;
        }

    }

    public void TakeDamage()
    {
        audioControl.PlaySound(SFXType.Damage);
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
