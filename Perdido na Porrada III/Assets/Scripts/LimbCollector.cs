using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbCollector : Bolt.EntityBehaviour<ICustomPlayerState>
{
    public bool hasLeftArm = true;
    public bool hasRightArm = true;
    public Collider2D limbCollider;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (entity.IsOwner)
            {
                if (collision.gameObject.GetComponent<LimbComponent>().limbType == LimbType.leftArm)
                {
                    hasLeftArm = true;
                }

                if (collision.gameObject.GetComponent<LimbComponent>().limbType == LimbType.rightArm)
                {
                    hasRightArm = true;
                }
            }
           

            BoltNetwork.Destroy(collision.gameObject);
        }
    }

}
