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

    //parry
    public GameObject Block;
    public GameObject HitBox;
    InputMaster controls;

    private bool enableLeftShooting = true;
    private bool enableRightShooting = true;


    private void Awake()
    {
        controls = gameObject.GetComponent<PlayerController>().controls;

        controls.Gameplay.LeftArmShoot.started += ctx => {
            if (!rightArmShootTrigger) {
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

        controls.Gameplay.LeftArmShoot.started += ctx => {

                leftArmShootTrigger = false;
                

        };
        controls.Gameplay.RightArmShoot.started += ctx => {

                rightArmShootTrigger = false;

        };
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
        if (state.LeftArmEnable && entity.IsOwner && !LeftArmShooted)
        {
            LeftArmShooted = true;
            StartCoroutine(PunchAnimation("IsLeftPunching"));
        }
    }

    public void ShootRightArm()
    {
        if (state.RightArmEnable && entity.IsOwner && !RightArmShooted)
        {
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
        if (entity.IsOwner)
        {
            PlayerType playerType = gameObject.GetComponent<PlayerStatus>().playerType;
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

            GameObject newLimb = BoltNetwork.Instantiate(limb, shotPoint.position, shotPoint.rotation);
            newLimb.GetComponent<LimbComponent>().limbHitbox.GetComponent<BoxCollider2D>().isTrigger = true;

            if (!gameObject.GetComponent<PlayerStatus>().isFlipped)
            {
                if (playerType == PlayerType.Carlous)
                {
                    newLimb.GetComponent<Rigidbody2D>().velocity = -1 * transform.right * launchForce;
                }
                else
                {
                    newLimb.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
                }
            }
            else
            {
                if (playerType == PlayerType.Carlous)
                {
                    newLimb.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
                 }
                else
                {
                    newLimb.GetComponent<Rigidbody2D>().velocity = -1 * transform.right * launchForce;
                }

                Vector3 newLimbLocalScale = newLimb.transform.localScale;
                newLimbLocalScale.x *= -1;
                newLimb.transform.localScale = newLimbLocalScale;
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
