using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class LimbCollector : Bolt.EntityBehaviour<ICustomPlayerState>
{
    public bool hasLeftArm = true;
    public bool hasRightArm = true;

    InputMaster controls;

    private bool picked = false;

    public PlayerType myBody;

    private void Awake()
    {
        controls = new InputMaster();
        controls.Gameplay.Move.performed += ctx =>
        {
            //Debug.Log(ctx.ReadValue<Vector2>().y);
            if (ctx.ReadValue<Vector2>().y <= -0.5)
            {
                picked = true;
            }else
            {
                picked = false;
            }
        };
        
    }
        

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (picked)
        {
            if (entity.IsOwner)
            {
                if (collision.CompareTag("limb"))
                {
                    if(collision.gameObject.GetComponent<LimbComponent>().playerType == myBody)
                    {
                        if (collision.gameObject.GetComponent<LimbComponent>().limbType == LimbType.leftArm)
                        {
                            state.LeftArmEnable = true;
                            gameObject.GetComponent<LimbShooter>().LeftArmShooted = false;
                            gameObject.GetComponent<LimbShooter>().leftArmSprite.SetActive(state.LeftArmEnable);
                            gameObject.GetComponent<LimbShooter>().leftForearmSprite.SetActive(state.LeftArmEnable);
                            //BoltNetwork.Destroy(collision.gameObject);
                            Destroy(collision.gameObject);
                        }

                        if (collision.gameObject.GetComponent<LimbComponent>().limbType == LimbType.rightArm)
                        {
                            state.RightArmEnable = true;
                            gameObject.GetComponent<LimbShooter>().RightArmShooted = false;
                            gameObject.GetComponent<LimbShooter>().rightArmSprite.SetActive(state.RightArmEnable);
                            gameObject.GetComponent<LimbShooter>().rightForearmSprite.SetActive(state.RightArmEnable);
                            //BoltNetwork.Destroy(collision.gameObject);
                            Destroy(collision.gameObject);
                        }
                    }
                    

                }
            }

            
        }
    }


    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }


}
