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
    public GameObject shoes;
    private bool parrying;
    private bool enableParry = true;

    InputMaster controls;

    Vector2 move;

    private void Awake()
    {
        controls = new InputMaster();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();

        controls.Gameplay.Parry.performed += ctx => {
            if (enableParry) {
                parrying = true;
                enableParry = false;
            }
        };

        controls.Gameplay.Parry.canceled += ctx => { enableParry = true; };

    }

    //void start para o bolt
    public override void Attached()
    {
        state.SetTransforms(state.PlayerTransform, gameObject.transform);
        state.SetTransforms(state.CameraTransform, Camera.transform);
        playerAnimator = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        Camera.GetComponent<CameraHandler>().clientPositionX = gameObject;
        state.SetAnimator(playerAnimator);
        state.LeftArmEnable = true;
        state.RightArmEnable = true;
        
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
        if (state.MoveX == 0.0f)
        {
            state.Animator.SetBool("IsWalking", false);
            //Debug.Log("Ta andando");
        }
        else
        {
            state.Animator.SetBool("IsWalking", true);
            //Debug.Log("Nao Ta andando");
        }

        if (state.MoveY >= 0.5f)
        {
            if (IsGrounded()) {
                state.Animator.SetBool("Jump", true);
                Jump();
            }
        }
        else
        {
            state.Animator.SetBool("Jump", false);
        }

        if (state.MoveY < -0.5f)
        {
            state.Animator.SetBool("Duck", true);
        }
        else
        {
            state.Animator.SetBool("Duck", false);
        }

        if (state.Parrying)
        {
            state.Animator.SetTrigger("Parry");
            StartCoroutine(ParryAnimation());
        }
    }


    private IEnumerator ParryAnimation()
    {
        
        gameObject.GetComponent<LimbShooter>().HitBox.SetActive(false);
        yield return new WaitForSeconds(gameObject.GetComponent<LimbShooter>().parryAnimationTime);
        gameObject.GetComponent<LimbShooter>().HitBox.SetActive(true);
        parrying = false; 
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
        state.Parrying = parrying;

        //Debug.Log(BoltMatchmaking.CurrentSession.ConnectionsCurrent);


        transform.Translate(new Vector2(m.x, 0f), Space.World);

        //transform.position += new Vector3(m.x, 0f, transform.position.z);

    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    public float bola = 10f;

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
