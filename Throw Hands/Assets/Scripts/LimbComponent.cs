using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class LimbComponent : Bolt.EntityBehaviour<ILimbState>
{

    [SerializeField] private Collider2D groundCollider;
    public float raySize = 0.7f;
    public LimbType limbType;
    public GameObject shoes;
    public GameObject limbHitbox;
    public PlayerType playerType;


    // Start is called before the first frame update
    void Start()
    {
        groundCollider = GameObject.FindGameObjectWithTag("ground").GetComponent<Collider2D>();
    }

    public override void Attached()
    {
        state.SetTransforms(state.LimbTransform, gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            gameObject.GetComponent<Rigidbody2D>().drag = 10f;
        }
    }

    public bool IsGrounded()
    {

        if (shoes.GetComponent<ShoeComponent>().onFloor)
        {
            Debug.Log("NO CHÃO");
            return true;
        }
        else
        {
            return false;
        }
    }
}
