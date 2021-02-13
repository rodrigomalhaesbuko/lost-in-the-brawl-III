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
    public GameObject playerObject;

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
                Vector2 vel = collision.gameObject.GetComponent<LimbHitComponent>().rdbody.velocity;
                collision.gameObject.GetComponent<LimbHitComponent>().rdbody.velocity = Vector2.Lerp(vel, new Vector2(0f,vel.y), 2f);
                collision.gameObject.GetComponent<LimbHitComponent>().rdbody.AddForce(new Vector2(0f, -5.0f),ForceMode2D.Impulse);
                collision.gameObject.GetComponent<LimbHitComponent>().limb.layer = LayerMask.NameToLayer("TransparentFX");
            
                collision.gameObject.GetComponent<LimbHitComponent>().Damaging = false;

                if(!BoltNetwork.IsClient && myLimbs == PlayerType.Carlous)
                {
                    var evnt = Damaged.Create();
                    evnt.WasHost = false;
                    evnt.Send();
                }
                else if(BoltNetwork.IsClient && myLimbs == PlayerType.Douglas)
                {
                    var evnt = Damaged.Create();
                    evnt.WasHost = true;
                    evnt.Send();
                }
  

            }
            
        }
    }

    

}
