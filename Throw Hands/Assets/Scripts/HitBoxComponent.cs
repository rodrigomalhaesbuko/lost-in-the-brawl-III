using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxComponent : Bolt.EntityBehaviour<ICustomPlayerState>
{
    // Start is called before the first frame update
    public PlayerStatus player;
    public PlayerType myLimbs;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("limbHitbox"))
        {
            if (collision.gameObject.GetComponent<LimbComponent>().playerType != myLimbs)
            {
                Debug.Log("ME ACERTOUU");
                player.TakeDamage();
            }
            
        }
    }
}
