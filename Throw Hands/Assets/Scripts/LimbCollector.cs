using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbCollector : Bolt.EntityBehaviour<ICustomPlayerState>
{
    public bool hasLeftArm = true;
    public bool hasRightArm = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("NOIA");
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (entity.IsOwner)
            {
                if (collision.gameObject.GetComponent<LimbComponent>().limbType == LimbType.leftArm)
                {
                    hasLeftArm = true;
                    gameObject.GetComponent<LimbShooter>().leftArmSprite.SetActive(true);
                    gameObject.GetComponent<LimbShooter>().leftForearmSprite.SetActive(true);
                }

                if (collision.gameObject.GetComponent<LimbComponent>().limbType == LimbType.rightArm)
                {
                    hasRightArm = true;
                    gameObject.GetComponent<LimbShooter>().rightArmSprite.SetActive(true);
                    gameObject.GetComponent<LimbShooter>().rightForearmSprite.SetActive(true);
                }
            }

            BoltNetwork.Destroy(collision.gameObject);
        }
    }

}
