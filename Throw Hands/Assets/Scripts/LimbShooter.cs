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
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && limbCollectior.hasLeftArm && entity.IsOwner)
        {
            limbCollectior.hasLeftArm = false;
            StartCoroutine(PunchAnimation("IsLeftPunching"));
            limbType = LimbType.leftArm;

        }

        if (Input.GetKeyDown(KeyCode.X) && limbCollectior.hasRightArm && entity.IsOwner)
        {
            limbCollectior.hasRightArm = false;
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

    void Shoot()
    {
        if (entity.IsOwner)
        {
            if (limbType == LimbType.leftArm)
            {
                leftArmSprite.SetActive(false);
                leftForearmSprite.SetActive(false);
                setupLimbSprite(LimbType.leftArm);
            }

            if (limbType == LimbType.rightArm)
            {
                rightArmSprite.SetActive(false);
                rightForearmSprite.SetActive(false);
                setupLimbSprite(LimbType.rightArm);
            }

            GameObject newLimb = BoltNetwork.Instantiate(limb, shotPoint.position, shotPoint.rotation);
            newLimb.GetComponent<LimbComponent>().limbHitbox.GetComponent<BoxCollider2D>().isTrigger = true;
            newLimb.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
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
