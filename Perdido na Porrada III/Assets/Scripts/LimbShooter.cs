using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbShooter : MonoBehaviour
{
    public GameObject limb;
    public float launchForce; /* Força do lançamento do membro*/
    public Transform shotPoint; /* Ponto onde será instanciado o membro */
    public Sprite leftArmSprite;
    public Sprite rightArmSprite;
    public LimbCollector limbCollectior;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && limbCollectior.hasLeftArm)
        {
            Shoot(LimbType.leftArm);
            limbCollectior.hasLeftArm = false;
        }

        if (Input.GetKeyDown(KeyCode.X) && limbCollectior.hasRightArm)
        {
            Shoot(LimbType.rightArm);
            limbCollectior.hasRightArm = false;
        }
    }

    void Shoot(LimbType limbType) 
    {
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
        newLimb.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }
}

public enum LimbType 
{
    leftArm,
    rightArm
}
