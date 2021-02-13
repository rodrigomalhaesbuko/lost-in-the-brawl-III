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
    public float hatForce = 4f;

    public GameObject GameController;
    public bool isFlipped = false;

    public PlayerType playerType;

    public Color greenColor;
    public Color redColor;

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
        if(state.Health < 5 && state.Health >= 0)
        {
            lifeHost.transform.GetChild(state.Health).GetComponent<Image>().color = redColor;
        } 
      
        if (state.Health <= 0)
        {
            //Debug.Log("GameOver Player 1 ganhou");
            GameController.GetComponent<GameController>().endGame(false, false);
        }

    }

    private void EnemyHealthCallBack()
    {
        if (state.EnemyHealth < 5 && state.EnemyHealth >= 0)
        {
            lifeClient.transform.GetChild(state.EnemyHealth).GetComponent<Image>().color = redColor;
        }

        if (state.EnemyHealth <= 0)
        {
            //Debug.Log("GameOver Player 2 ganhou");
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("hat"))
        {
            
            if(playerType == PlayerType.Douglas)
            {
              
                if (GameController.GetComponent<GameController>().CarlousInstance.transform.position.x > gameObject.transform.position.x)
                {
                    Debug.Log("OBAAAAAAAA1");
                    //transform.Translate(new Vector2(-0.2f, 0f), Space.World);
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-hatForce, 0f), ForceMode2D.Impulse);
                }
                else
                {
                    Debug.Log("OBAAAAAAAA2");
                    //transform.Translate(new Vector2(-0.2f, 0f), Space.World);
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(hatForce, 0f), ForceMode2D.Impulse);
                }
            }else
            {
                if (GameController.GetComponent<GameController>().DouglasInstance.transform.position.x > gameObject.transform.position.x)
                {
                    Debug.Log("OBAAAAAAAA3");
                    //transform.Translate(new Vector2(-0.2f, 0f), Space.World);
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-hatForce, 0f), ForceMode2D.Impulse);
                }
                else
                {
                    Debug.Log("OBAAAAAAAA4");
                    //transform.Translate(new Vector2(-0.2f, 0f), Space.World);
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(hatForce, 0f), ForceMode2D.Impulse);
                }
            }

           
        }
    }

    public void TakeDamage()
    {
        if (BoltNetwork.IsClient)
        {
            Debug.Log("CLIENTE TOMOU DANO");
            state.EnemyHealth = state.EnemyHealth - 1;
        }
        else
        {
            Debug.Log("HOST TOMOU DANO");
            state.Health = state.Health - 1;
        }

        audioControl.PlaySound(SFXType.Damage);
        Debug.Log("CLIENTE" + " " + "VIDA" + " " + state.EnemyHealth.ToString());
        Debug.Log("host" + " " + "VIDA" + " " + state.Health.ToString());
    }

}

public enum PlayerType { 
    Carlous,
    Douglas
}
