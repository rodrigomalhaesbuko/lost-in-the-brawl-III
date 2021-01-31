using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbShooter : Bolt.EntityBehaviour<ICustomPlayerState>
{
    public GameObject limb;
    public float launchForce; /* Força do lançamento do membro*/
    public Transform shotPoint; /* Ponto onde será instanciado o membro */
    public Sprite leftArmSprite;
    public Sprite rightArmSprite;
    public LimbCollector limbCollectior;

    private LimbType limbType;


    public override void Attached()
    {
        state.OnShoot = Shoot;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && limbCollectior.hasLeftArm && entity.IsOwner)
        {
            limbType = LimbType.leftArm;
            state.Shoot();
            limbCollectior.hasLeftArm = false;
        }

        if (Input.GetKeyDown(KeyCode.X) && limbCollectior.hasRightArm && entity.IsOwner)
        {
            limbType = LimbType.rightArm;
            state.Shoot();
            limbCollectior.hasRightArm = false;
        }
    }

    void Shoot()
    {
        if (entity.IsOwner) { 
            if (limbType == LimbType.leftArm) {
                //limb.GetComponent<SpriteRenderer>().sprite = leftArmSprite;
                limb.GetComponent<LimbComponent>().limbType = LimbType.leftArm;
            }

            if (limbType == LimbType.rightArm)
            {
                // limb.GetComponent<SpriteRenderer>().sprite = rightArmSprite;
                limb.GetComponent<LimbComponent>().limbType = LimbType.rightArm;
            }

            GameObject newLimb = Instantiate(limb, shotPoint.position, shotPoint.rotation);
            BoltNetwork.Attach(newLimb);
            newLimb.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
        }
    }
}

public enum LimbType 
{
    leftArm,
    rightArm
}
