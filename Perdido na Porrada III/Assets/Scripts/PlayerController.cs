using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Bolt.Matchmaking;
using UnityEngine.InputSystem;

public class PlayerController : Bolt.EntityBehaviour<ICustomPlayerState>
{

    public float speed = 10.0f;
    public Rigidbody2D rb;
    public float jumpForce;
    public float colliderRaySize;
    public Collider2D groundCollider;

    InputMaster controls;

    Vector2 move;

    private void Awake()
    {
        controls = new InputMaster();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();

    }

    //void start para o bolt
    public override void Attached()
    {
        state.SetTransforms(state.PlayerTransform, gameObject.transform);
        groundCollider = GameObject.FindGameObjectWithTag("ground").GetComponent<Collider2D>();
    }

    // update para o bolt 
    public override void SimulateOwner()
    {
        CheckInputs();
    }

    public void Update()
    {
        Debug.Log(BoltMatchmaking.CurrentSession.ConnectionsCurrent);
    }

    //    // Start is called before the first frame update
    //    public PhotonView photonView;
    //    public Rigidbody2D rb;
    //    public GameObject PlayerCamera;

    //    private void Awake()
    //    {
    //        if (photonView.isMine)
    //        {
    //            Debug.Log("eu tenho photon view");
    //            PlayerCamera.SetActive(true);
    //        }
    //    } 


    private void CheckInputs()
    {
        //Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        ////rb.apply move * speed * Time.deltaTim
        //transform.position += move * speed * BoltNetwork.FrameDeltaTime;
        Vector2 m = move * speed * BoltNetwork.FrameDeltaTime;
        transform.Translate(m, Space.World);
     if(Input.GetKey(KeyCode.Space) && IsGrounded()) 
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, colliderRaySize);
        if (hit.collider == groundCollider)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    //    // Update is called once per frame
    //    void Update()
    //    {
    //        if (photonView.isMine)
    //        {
    //            CheckInputs();
    //        }
    //    }
}
