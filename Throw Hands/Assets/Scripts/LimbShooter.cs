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

    InputMaster controls;

    private void Awake()
    {
        controls = new InputMaster();

        controls.Gameplay.LeftArmShoot.performed += ctx => {
            if (!rightArmShootTrigger) {
                leftArmShootTrigger = true;
                ShootLeftArm();
                }
        };
        controls.Gameplay.RightArmShoot.performed += ctx => {
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

        controls.Gameplay.Parry.performed += ctx => Parry();
    }

    private void Start()
    {
        playerAnimator = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    public override void Attached()
    {
        state.OnShoot = Shoot;
        state.AddCallback("LeftArmEnable", ChangeLeftArm);
        state.AddCallback("RightArmEnable", ChangeRightArm);
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
        playerAnimator.SetTrigger(animation);
        yield return new WaitForSeconds(animationTime);
        if (animation == "IsRightPunching")
        {
            limbType = LimbType.rightArm;
        }
        else
        {
            limbType = LimbType.leftArm;
        }


        state.Shoot();
    }

    //PARRY

    public void Parry()
    {
        Debug.Log("Parry");
        StartCoroutine(ParryAnimation());
    }

    private IEnumerator ParryAnimation()
    {
        playerAnimator.SetTrigger("Parry");
        Block.SetActive(true);
        yield return new WaitForSeconds(parryAnimationTime);
        Block.SetActive(false);

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

            if (playerType == PlayerType.Carlous)
            {
                newLimb.GetComponent<Rigidbody2D>().velocity = -1 * transform.right * launchForce;
            }
            else
            {
                newLimb.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
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
