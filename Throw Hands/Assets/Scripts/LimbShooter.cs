using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LimbShooter : Bolt.EntityBehaviour<ICustomPlayerState>
{
    public GameObject Limb;
    public float launchForce; /* Força do lançamento do membro*/
    public Transform shotPoint; /* Ponto onde será instanciado o membro */
    public GameObject leftArmSprite;
    public GameObject leftForearmSprite;
    public GameObject rightArmSprite;
    public GameObject rightForearmSprite;
    public LimbCollector limbCollectior;

    public GameObject carlousRightArm;
    public GameObject carlousLeftArm;

    public GameObject douglasRightArm;
    public GameObject douglasLeftArm;
    public bool LeftArmShooted = false;
    public bool RightArmShooted = false;

    private LimbType limbType;
    public Animator playerAnimator;
    public float animationTime;
    public float parryAnimationTime;
    private bool rightArmShootTrigger = false ;
    private bool leftArmShootTrigger = false ;

    private bool _isLocal = false;

    //parry
    public GameObject Block;
    public GameObject HitBox;
    InputMaster controls;

    private bool enableLeftShooting = true;
    private bool enableRightShooting = true;

    public void LocalLeftShootP1(InputAction.CallbackContext ctx)
    {
        Debug.Log("LEFT BOLA P1");
        if (ctx.performed)
        {
            if (gameObject.GetComponent<PlayerStatus>().playerType == PlayerType.Douglas)
                if (!rightArmShootTrigger)
                {
                    leftArmShootTrigger = true;
                    ShootLeftArm();
                }
        }
    }

    public void LocalRightShootP1(InputAction.CallbackContext ctx)
    {
        Debug.Log("RIGHT BOLA P1");
        if (ctx.performed)
        {
            if (gameObject.GetComponent<PlayerStatus>().playerType == PlayerType.Douglas && _isLocal)
                if (!leftArmShootTrigger)
                {
                    rightArmShootTrigger = true;
                    ShootRightArm();
                }
        }

    }


    public void LocalLeftShootP2(InputAction.CallbackContext ctx)
    {
        Debug.Log("LEFTARM BOLA P2");
        if (ctx.performed)
        {
            if (gameObject.GetComponent<PlayerStatus>().playerType != PlayerType.Douglas && _isLocal)
                if (!rightArmShootTrigger)
                {
                    leftArmShootTrigger = true;
                    ShootLeftArm();
                }
        }

    }

    public void LocalRightShootP2(InputAction.CallbackContext ctx)
    {
        Debug.Log("RIGHT BOLA P2");
        if (ctx.performed)
        {
            if (gameObject.GetComponent<PlayerStatus>().playerType != PlayerType.Douglas && _isLocal)
                if (!leftArmShootTrigger)
                {
                    rightArmShootTrigger = true;
                    ShootRightArm();
                }
        }

    }

    private void Awake()
    {
        controls = gameObject.GetComponent<PlayerController>().controls;
        _isLocal = gameObject.GetComponent<PlayerStatus>().GameController.GetComponent<GameController>().isLocal;
        if (gameObject.GetComponent<PlayerStatus>().GameController.GetComponent<GameController>().isLocal)
        {
            if (gameObject.GetComponent<PlayerStatus>().playerType == PlayerType.Douglas)
            {
                //controls.Gameplay.LocalLeftShootP1.started += ctx => {
                //    if (!rightArmShootTrigger)
                //    {
                //        leftArmShootTrigger = true;
                //        ShootLeftArm();
                //    }
                //};
                //controls.Gameplay.LocalRightShootP1.started += ctx => {
                //    if (!leftArmShootTrigger)
                //    {
                //        rightArmShootTrigger = true;
                //        ShootRightArm();
                //    }
                //};

                controls.Gameplay.LocalLeftShootP1.canceled += ctx => {

                    leftArmShootTrigger = false;
                    Debug.Log("SOLTOUUU P1");

                };
                controls.Gameplay.LocalRightShootP1.canceled += ctx => {

                    rightArmShootTrigger = false;
                    Debug.Log("SOLTOUUU P1");

                };
            }
            else
            {
                //controls.Gameplay.LocalLeftShootP2.started += ctx => {
                //    if (!rightArmShootTrigger)
                //    {
                //        leftArmShootTrigger = true;
                //        ShootLeftArm();
                //    }
                //};
                //controls.Gameplay.LocalRightShootP2.started += ctx => {
                //    if (!leftArmShootTrigger)
                //    {
                //        rightArmShootTrigger = true;
                //        ShootRightArm();
                //    }
                //};

                controls.Gameplay.LocalLeftShootP2.canceled += ctx => {

                    leftArmShootTrigger = false;
                    Debug.Log("SOLTOUUU P2");

                };

                controls.Gameplay.LocalRightShootP2.canceled += ctx => {

                    rightArmShootTrigger = false;
                    Debug.Log("SOLTOUUU P2");

                };
            }
        }
        else
        {
            controls.Gameplay.LeftArmShoot.started += ctx => {
                if (!rightArmShootTrigger)
                {
                    leftArmShootTrigger = true;
                    ShootLeftArm();
                }
            };
            controls.Gameplay.RightArmShoot.started += ctx => {
                if (!leftArmShootTrigger)
                {
                    rightArmShootTrigger = true;
                    ShootRightArm();
                }
            };

            controls.Gameplay.LeftArmShoot.canceled += ctx => {

                leftArmShootTrigger = false;

            };
            controls.Gameplay.RightArmShoot.canceled += ctx => {

                rightArmShootTrigger = false;

            };
        }


    }

    // bolt void start 
    public override void Attached()
    {
        state.OnShoot = Shoot;
        state.AddCallback("LeftArmEnable", ChangeLeftArm);
        state.AddCallback("RightArmEnable", ChangeRightArm);
        state.IsLeftArmShooting = false;
        state.IsRightArmShooting = false;
    }


    //PUNCHES 
    public void ShootLeftArm()
    {
        if (state.LeftArmEnable && entity.IsOwner && !LeftArmShooted && !gameObject.GetComponent<PlayerController>().isDucking)
        {
            audioControl.PlaySound(SFXType.Punch);
            LeftArmShooted = true;
            StartCoroutine(PunchAnimation("IsLeftPunching"));
        }
    }

    public void ShootRightArm()
    {
        if (state.RightArmEnable && entity.IsOwner && !RightArmShooted && !gameObject.GetComponent<PlayerController>().isDucking)
        {
            audioControl.PlaySound(SFXType.Punch);
            RightArmShooted = true;
            StartCoroutine(PunchAnimation("IsRightPunching"));
        }
    }

    private IEnumerator PunchAnimation(string animation)
    {
        if (animation == "IsRightPunching")
        {
            state.IsRightArmShooting = true;
        }
        else
        { 
            state.IsLeftArmShooting = true;
        }

        yield return new WaitForSeconds(animationTime);

        if (animation == "IsRightPunching")
        {
            limbType = LimbType.rightArm;
            state.IsRightArmShooting = false;
            enableRightShooting = true;
        }
        else
        {
            limbType = LimbType.leftArm;
            state.IsLeftArmShooting = false;
            enableLeftShooting = true;
        }


        state.Shoot();
    }


    private void Update()
    {
        if (state.IsRightArmShooting)
        {
            if (enableRightShooting)
            {
                enableRightShooting = false;
                state.Animator.SetTrigger("IsRightPunching");
            }
        }

        if (state.IsLeftArmShooting)
        {
            if (enableLeftShooting)
            {
                enableLeftShooting = false;
                state.Animator.SetTrigger("IsLeftPunching");
            }     
        }
    }

    private void ChangeRightArm()
    {
         leftArmSprite.SetActive(state.LeftArmEnable);
         leftForearmSprite.SetActive(state.LeftArmEnable);
         rightArmSprite.SetActive(state.RightArmEnable);
         rightForearmSprite.SetActive(state.RightArmEnable);
    }

    private void ChangeLeftArm()
    {
        leftArmSprite.SetActive(state.LeftArmEnable);
        leftForearmSprite.SetActive(state.LeftArmEnable);
        rightArmSprite.SetActive(state.RightArmEnable);
        rightForearmSprite.SetActive(state.RightArmEnable);
    }

    void Shoot()
    {
        StartCoroutine(ShootCourotine());
    }

    public IEnumerator ShootCourotine()
    {
        GameObject newLimb = null;
        PlayerType playerType = gameObject.GetComponent<PlayerStatus>().playerType;

        if (entity.IsOwner)
        {
            
            if (limbType == LimbType.leftArm)
            {
                leftArmSprite.SetActive(state.LeftArmEnable);
                leftForearmSprite.SetActive(state.LeftArmEnable);
                state.LeftArmEnable = false;
                LeftArmShooted = false;
            }

            if (limbType == LimbType.rightArm)
            {
                rightArmSprite.SetActive(state.RightArmEnable);
                rightForearmSprite.SetActive(state.RightArmEnable);
                state.RightArmEnable = false;
                RightArmShooted = false;
                
            }
            GameObject limb = Limb;
            switch (playerType)
            {
                case PlayerType.Carlous:
                    if (limbType == LimbType.leftArm)
                    {
                        limb = carlousLeftArm;
                    }
                    else
                    {
                        limb = carlousRightArm;
                    }
                    break;
                case PlayerType.Douglas:
                    if (limbType == LimbType.leftArm)
                    {
                        limb = douglasLeftArm;
                    }
                    else
                    {
                        limb = douglasRightArm;
                    }
                    break;
            }

            newLimb = BoltNetwork.Instantiate(limb, shotPoint.position, shotPoint.rotation);
            newLimb.GetComponent<LimbComponent>().limbHitbox.GetComponent<BoxCollider2D>().isTrigger = true;
            newLimb.GetComponent<LimbComponent>().owner = gameObject;
                        

            if (!gameObject.GetComponent<PlayerStatus>().isFlipped)
            {
                if (playerType == PlayerType.Carlous)
                {
                    newLimb.GetComponent<Rigidbody2D>().velocity = -1 * transform.right * launchForce * 2;
                }
                else
                {
                    newLimb.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce * 2;
                }
            }
            else
            {
                if (playerType == PlayerType.Carlous)
                {
                    newLimb.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce * 2;
                }
                else
                {
                    newLimb.GetComponent<Rigidbody2D>().velocity = -1 * transform.right * launchForce * 2;
                }

                //Vector3 newLimbLocalScale = newLimb.transform.localScale;
                //newLimbLocalScale.x *= -1;
                //newLimb.transform.localScale = newLimbLocalScale;
            }

            yield return new WaitForEndOfFrame();

        }


    }

    //void setupLimbSprite(LimbType type)
    //{
    //    PlayerType playerType = gameObject.GetComponent<PlayerStatus>().playerType;

    //    switch (playerType)
    //    {
    //        case PlayerType.Carlous:
    //            if (limbType == LimbType.leftArm)
    //            {
    //                limb = carlousLeftArm;
    //            }
    //            else
    //            {
    //                limb = carlousRightArm;
    //            }
    //            break;
    //        case PlayerType.Douglas:
    //            if (limbType == LimbType.leftArm)
    //            {
    //                limb = douglasLeftArm;
    //            }
    //            else
    //            {
    //                limb = douglasRightArm;
    //            }
    //            break;
    //    }

    //}

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

}

public enum LimbType 
{
    leftArm,
    rightArm
}
