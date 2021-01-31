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
    public GameObject Camera;
    public Animator playerAnimator;

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
        state.SetTransforms(state.CameraTransform, Camera.transform);
        playerAnimator = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        groundCollider = GameObject.FindGameObjectWithTag("ground").GetComponent<Collider2D>();
        Camera.GetComponent<CameraHandler>().clientPositionX = gameObject;
        state.SetAnimator(playerAnimator);
    }

    // update para o bolt 
    public override void SimulateOwner()
    {
        CheckInputs();

        if (Camera.transform.position.x > 21.0f || Camera.transform.position.x < -5.5f)
        {
            Vector3 position = new Vector3((Camera.transform.position.x), Camera.transform.position.y, Camera.transform.position.z);
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, position, Time.deltaTime * 1f);
        }
        else if (Camera.transform.position.x - Mathf.Abs(transform.position.x) > 10f || Camera.transform.position.x - Mathf.Abs(transform.position.x) < -10f)
        {
            Vector3 position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, Camera.transform.position.z);
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, position, Time.deltaTime * 1f);
        }
        else
        {
            Vector3 position = new Vector3((Camera.transform.position.x + transform.position.x) / 2.0f, Camera.transform.position.y, Camera.transform.position.z);
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, position, Time.deltaTime * 1f);
        }
    }

    public void Update()
    {
        Debug.Log(BoltMatchmaking.CurrentSession.ConnectionsCurrent);
        if (state.MoveX == 0.0f)
        {
            state.Animator.SetBool("IsWalking", false);
            Debug.Log("Ta andando");
        }
        else
        {
            state.Animator.SetBool("IsWalking", true);
            Debug.Log("Nao Ta andando");
        }


        if (state.MoveY > 0.5f)
        {
            state.Animator.SetBool("IsJump", true);
            Jump();
        }
        else
        {
            state.Animator.SetBool("IsJump", false);
        }

        if (state.MoveY < -0.5f)
        {
            state.Animator.SetBool("IsDuck", true);
        }
        else
        {
            playerAnimator.SetBool("IsDuck", false);
        }
        

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
        state.MoveX = move.x;
        state.MoveY = move.y;
        Debug.Log(move);

        //Debug.Log(BoltMatchmaking.CurrentSession.ConnectionsCurrent);

        transform.Translate(m, Space.World);
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        playerAnimator.SetTrigger("Jump");
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
