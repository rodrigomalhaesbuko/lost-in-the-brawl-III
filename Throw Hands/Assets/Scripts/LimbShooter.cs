using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbShooter : Bolt.EntityBehaviour<ICustomPlayerState>
{
    public GameObject limb;
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

    private LimbType limbType;
    public Animator playerAnimator;
    public float animationTime;

    private void Start()
    {
        playerAnimator = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    public override void Attached()
    {
        state.OnShoot = Shoot;
        state.AddCallback("LeftArmEnable", ChangeArm);
        state.AddCallback("RightArmEnable", ChangeArm);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && state.LeftArmEnable && entity.IsOwner)
        {
            
            StartCoroutine(PunchAnimation("IsLeftPunching"));
            limbType = LimbType.leftArm;

        }

        if (Input.GetKeyDown(KeyCode.X) && state.RightArmEnable && entity.IsOwner)
        {
            
            StartCoroutine(PunchAnimation("IsRightPunching"));
            limbType = LimbType.rightArm;
        }
    }

    private IEnumerator PunchAnimation(string animation)
    {
        playerAnimator.SetTrigger(animation);
        yield return new WaitForSeconds(animationTime);
        state.Shoot();
    }

    private void ChangeArm()
    {
            leftArmSprite.SetActive(state.LeftArmEnable);
            leftForearmSprite.SetActive(state.LeftArmEnable);
            rightArmSprite.SetActive(state.RightArmEnable);
            rightForearmSprite.SetActive(state.RightArmEnable);
    }

    void Shoot()
    {
        if (entity.IsOwner)
        {
            PlayerType playerType = gameObject.GetComponent<PlayerStatus>().playerType;
            if (limbType == LimbType.leftArm)
            {
                leftArmSprite.SetActive(state.LeftArmEnable);
                leftForearmSprite.SetActive(state.LeftArmEnable);
                state.LeftArmEnable = false;
                setupLimbSprite(LimbType.leftArm);
            }

            if (limbType == LimbType.rightArm)
            {
                rightArmSprite.SetActive(state.RightArmEnable);
                rightForearmSprite.SetActive(state.RightArmEnable);
                state.RightArmEnable = false;
                setupLimbSprite(LimbType.rightArm);
            }

            GameObject newLimb = BoltNetwork.Instantiate(limb, shotPoint.position, shotPoint.rotation);
            newLimb.GetComponent<LimbComponent>().limbHitbox.GetComponent<BoxCollider2D>().isTrigger = true;
            if(playerType == PlayerType.Carlous)
            {
                newLimb.GetComponent<Rigidbody2D>().velocity = -1 * transform.right * launchForce;
            }
            else
            {
                newLimb.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
            }
           
        }
    }

    void setupLimbSprite(LimbType type)
    {
        PlayerType playerType = gameObject.GetComponent<PlayerStatus>().playerType;

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

    }
}

public enum LimbType 
{
    leftArm,
    rightArm
}
