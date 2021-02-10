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
}
