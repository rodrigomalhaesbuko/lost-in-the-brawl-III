using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxComponent : Bolt.EntityBehaviour<ICustomPlayerState>
{
    // Start is called before the first frame update
    public PlayerStatus player;
    public PlayerType myLimbs;
    public Rigidbody2D myRgbody;
    public float impactForce = 30.0f;

    private void Start()
    {
       
        if (BoltNetwork.IsClient)
        {
            impactForce = 30f;
        }else
        {
            impactForce = -30f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("limbHitbox"))
        {
            if(collision.gameObject.GetComponent<LimbHitComponent>().LimbComponent.playerType != myLimbs && collision.gameObject.GetComponent<LimbHitComponent>().Damaging )
            {
                collision.gameObject.GetComponent<LimbHitComponent>().Damaging = false;

                if(collision.gameObject.GetComponent<LimbHitComponent>().rdbody.velocity.x > 0.0f)
                {
                    myRgbody.AddForce(new Vector2(-impactForce, 0f), ForceMode2D.Impulse);
                }
                else
                {
                    myRgbody.AddForce(new Vector2(impactForce, 0f), ForceMode2D.Impulse);
                }
                collision.gameObject.GetComponent<LimbHitComponent>().rdbody.velocity = Vector2.zero;
                collision.gameObject.GetComponent<LimbHitComponent>().rdbody.AddForce(new Vector2(0f, -10.0f),ForceMode2D.Impulse);

                collision.gameObject.GetComponent<LimbHitComponent>().myColider.isTrigger = true;
                player.TakeDamage();
                state.Animator.SetTrigger("Damage");
            }
            
        }
    }
}
