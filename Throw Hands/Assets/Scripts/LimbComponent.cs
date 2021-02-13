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

    public GameObject owner;

    public bool wallCollison = false;
    public bool wallFlipped = false;

    private bool fliped = false;

    void Start()
    {
        groundCollider = GameObject.FindGameObjectWithTag("ground").GetComponent<Collider2D>();
    }

    public override void Attached()
    {
        state.SetTransforms(state.LimbTransform, gameObject.transform);
    }

    void Update()
    {
        if (IsGrounded())
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            gameObject.GetComponent<Rigidbody2D>().drag = 10f;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            limbHitbox.GetComponent<LimbHitComponent>().Damaging = false;
        }
        else
        {
            if (GameController.armFlip && !fliped)
            {
                fliped = true;

                Vector3 newLimbLocalScale = gameObject.transform.localScale;
                newLimbLocalScale.x *= -1;
                gameObject.transform.localScale = newLimbLocalScale;
            }
        }
    }


    public bool IsGrounded()
    {

        if (shoes.GetComponent<ShoeComponent>().onFloor)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
