using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockComponent : MonoBehaviour
{

    public PlayerStatus player;
    public PlayerType myLimbs;
    public Rigidbody2D myRgbody;
    public float impactForce = 30.0f;

    private void Start()
    {
        if (BoltNetwork.IsClient)
        {
            impactForce = 30f;
        }
        else
        {
            impactForce = -30f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("limbHitbox"))
        {
            if(collision.gameObject.GetComponent<LimbHitComponent>().LimbComponent.playerType != myLimbs && collision.gameObject.GetComponent<LimbHitComponent>().Damaging)
            {
                if (player.isFlipped)
                {
                    impactForce *= -1;
                }

                collision.gameObject.GetComponent<LimbHitComponent>().myColider.isTrigger = true;
                collision.gameObject.GetComponent<LimbHitComponent>().Damaging = false;
                collision.gameObject.GetComponent<LimbHitComponent>().rdbody.velocity = Vector2.zero;
                collision.gameObject.GetComponent<LimbHitComponent>().rdbody.AddForce(new Vector2(impactForce, 0f), ForceMode2D.Impulse);
            }
           
        }
    }
}
