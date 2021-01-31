using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbComponent : MonoBehaviour
{

    [SerializeField] private Collider2D groundCollider;
    public float raySize = 0.7f;
    public LimbType limbType;

    // Start is called before the first frame update
    void Start()
    {
        groundCollider = GameObject.FindGameObjectWithTag("ground").GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raySize);
        if (hit.collider == groundCollider)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
