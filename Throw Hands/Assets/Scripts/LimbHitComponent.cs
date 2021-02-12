using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbHitComponent : MonoBehaviour
{
    public LimbComponent LimbComponent;
    public bool Damaging = true;
    public Rigidbody2D rdbody;
    public Collider2D myColider;
    public GameObject limb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Debug.Log("BOLAAA");
            Vector3 newLimbLocalScale = limb.transform.localScale;
            newLimbLocalScale.x *= -1;
            limb.transform.localScale = newLimbLocalScale;
        }
    }
}
