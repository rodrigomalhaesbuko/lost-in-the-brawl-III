using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalPlayerStatus : MonoBehaviour
{
    public int localHealth = 5;
    public GameObject lifeHost;
    public GameObject lifeClient;
    public float hatForce = 4f;

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

    private void restartLife()
    {

        for (int i = 0; i < 5; i++)
        {
            lifeClient.transform.GetChild(i).GetComponent<Image>().color = greenColor;
            lifeHost.transform.GetChild(i).GetComponent<Image>().color = greenColor;
        }

    }

    public void TakeDamage()
    {
        if (playerType == PlayerType.Douglas)
        {
            if (localHealth < 5 && localHealth >= 0)
            {
                for (int i = localHealth; i < 5; i++)
                    lifeHost.transform.GetChild(i).GetComponent<Image>().color = redColor;
            }

            if (localHealth <= 0)
            {
                GameController.GetComponent<LocalGameManager>().endGame(false, false);
            }
        }
        else
        {
            if (localHealth < 5 && localHealth >= 0)
            {
                for (int i = localHealth; i < 5; i++)
                    lifeClient.transform.GetChild(i).GetComponent<Image>().color = redColor;
            }

            if (localHealth <= 0)
            {
                GameController.GetComponent<LocalGameManager>().endGame(false, false);
            }
        }

        audioControl.PlaySound(SFXType.Damage);
    }

}
