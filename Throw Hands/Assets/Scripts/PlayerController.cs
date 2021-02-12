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
    public Animator playerAnimator;
    public GameObject shoes;
    private bool parrying;
    private bool enableParry = true;
    private bool enableParryAnimation = true;
    private bool alreadyJumped = false;

    public InputMaster controls;

    Vector2 move;

    private void Awake()
    {
        controls = new InputMaster();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();

        controls.Gameplay.Parry.started += ctx => {
            if (enableParry) {
                enableParry = false;
                parrying = true;
                //StartCoroutine(PrepareParry());   
            }
        };

        controls.Gameplay.Parry.canceled += ctx => { enableParry = true; };
    }

    //void start para o bolt
    public override void Attached()
    {
        state.SetTransforms(state.PlayerTransform, gameObject.transform);
        playerAnimator = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        state.SetAnimator(playerAnimator);
        state.LeftArmEnable = true;
        state.RightArmEnable = true;

        if (BoltNetwork.IsClient)
        {
            state.MyX = move.x;
        }
        else
        {
            state.OtherX = move.x;
        }
    }

    //private IEnumerator PrepareParry()
    //{
    //    yield return new WaitForEndOfFrame();
    //    parrying = false;
    //}

    // update para o bolt
    public override void SimulateOwner()
    {
        CheckInputs();

        if (BoltNetwork.IsClient)
        {
            state.MyX = move.x;
        }
        else
        {
            state.OtherX = move.x;
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

        if (IsGrounded())
        {
            if (state.MoveY >= 0.5f)
            {
                if (!alreadyJumped)
                {
                    alreadyJumped = true;
                    state.Animator.SetBool("Jump", true);
                    Jump();
                }
            }
            else
            {
                state.Animator.SetBool("Jump", false);
            }
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
            if (enableParryAnimation)
            {
                if(state.LeftArmEnable || state.RightArmEnable)
                {
                    enableParryAnimation = false;
                    state.Animator.SetTrigger("ParryT");
                    StartCoroutine(ParryAnimation());
                }
            }
        }

        

        //if(state.MyX > state.OtherX)
        //{
        //        Vector3 newScale = gameObject.transform.localScale;
        //        newScale.x *= -1;
        //        gameObject.transform.localScale = newScale;

        //}else if (state.MyX < state.OtherX)
        //{
        //        Vector3 newScale = gameObject.transform.localScale;
        //        newScale.x *= -1;
        //        gameObject.transform.localScale = newScale;
        //}
    }

    private IEnumerator ParryAnimation()
    {
        StartCoroutine(Block(gameObject.GetComponent<LimbShooter>().parryAnimationTime));
        yield return new WaitForSeconds(gameObject.GetComponent<LimbShooter>().parryAnimationTime);
        enableParry = true;
        parrying = false;
        enableParryAnimation = true;
    }

    private IEnumerator Block(float animationTime )
    {
        gameObject.GetComponent<LimbShooter>().HitBox.SetActive(false);
        gameObject.GetComponent<LimbShooter>().Block.SetActive(true);
        yield return new WaitForSeconds(animationTime / 2);
        gameObject.GetComponent<LimbShooter>().HitBox.SetActive(true);
        gameObject.GetComponent<LimbShooter>().Block.SetActive(false);
    }


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
            alreadyJumped = false;
            return false;  
        }
    }

    public void disableControls()
    {
        move = Vector2.zero;
        controls.Gameplay.Disable();
    }

    public void enableControls()
    {
        controls.Gameplay.Enable();
    }

    //private void OnEnable()
    //{
    //    controls.Gameplay.Enable();
    //}

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
