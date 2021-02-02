using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbCollector : Bolt.EntityBehaviour<ICustomPlayerState>
{
    public bool hasLeftArm = true;
    public bool hasRightArm = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (entity.IsOwner)
            {
                if (collision.CompareTag("limb"))
                {

                    if (collision.gameObject.GetComponent<LimbComponent>().limbType == LimbType.leftArm)
                    {
                        state.LeftArmEnable = true;
                        gameObject.GetComponent<LimbShooter>().leftArmSprite.SetActive(state.LeftArmEnable);
                        gameObject.GetComponent<LimbShooter>().leftForearmSprite.SetActive(state.LeftArmEnable);
                        BoltNetwork.Destroy(collision.gameObject);
                    }

                    if (collision.gameObject.GetComponent<LimbComponent>().limbType == LimbType.rightArm)
                    {
                        state.RightArmEnable = true;
                        gameObject.GetComponent<LimbShooter>().rightArmSprite.SetActive(state.RightArmEnable);
                        gameObject.GetComponent<LimbShooter>().rightForearmSprite.SetActive(state.RightArmEnable);
                        BoltNetwork.Destroy(collision.gameObject);
                    }

                }
            }

            
        }
    }

}
