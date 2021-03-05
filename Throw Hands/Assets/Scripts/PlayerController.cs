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
    private bool _isLocal = false;
    private bool _controlsWorking = false;
    private bool parrying;
    private bool enableParry = true;
    private bool enableParryAnimation = true;
    private bool alreadyJumped = false;

    public bool isDucking = false; 

    public InputMaster controls;

    Vector2 move;


    private void Start()
    {
        InputSystem.onDeviceChange += InputSystemOnDeviceChange;
        PlayerInput player = gameObject.GetComponent<PlayerInput>();
        player.enabled = false;
        if (_isLocal)
        {
            player.enabled = true;
            if (Gamepad.all.Count == 1)
            {
                if (gameObject.GetComponent<PlayerStatus>().playerType == PlayerType.Douglas)
                {
                    player.SwitchCurrentControlScheme(player.defaultControlScheme, Gamepad.all[0], Keyboard.current);
                }
                else
                {
                    player.SwitchCurrentControlScheme(player.defaultControlScheme, Keyboard.current);
                }
            }
            else if (Gamepad.all.Count == 2)
            {
                if (gameObject.GetComponent<PlayerStatus>().playerType == PlayerType.Carlous)
                {
                    player.SwitchCurrentControlScheme(player.defaultControlScheme, Gamepad.all[1], Keyboard.current);
                }
                else
                {
                    player.SwitchCurrentControlScheme(player.defaultControlScheme, Keyboard.current);
                }
            }
            else
            {
                player.SwitchCurrentControlScheme(player.defaultControlScheme, Keyboard.current);
            }
        }

        
    }

    public void OnLocalMovP1(InputAction.CallbackContext ctx)
    {
        if (gameObject.GetComponent<PlayerStatus>().playerType == PlayerType.Douglas && _isLocal)
            move = ctx.ReadValue<Vector2>();
    }

    public void OnLocalMovP2(InputAction.CallbackContext ctx)
    {
        if (gameObject.GetComponent<PlayerStatus>().playerType != PlayerType.Douglas && _isLocal)
            move = ctx.ReadValue<Vector2>();
    }


    public void LocalParryP1(InputAction.CallbackContext ctx)
    {
        if (gameObject.GetComponent<PlayerStatus>().playerType == PlayerType.Douglas && _isLocal)
            if (enableParry)
            {
                enableParry = false;
                parrying = true;
                //StartCoroutine(PrepareParry());   
            }
    }

    public void LocalParryP2(InputAction.CallbackContext ctx)
    {
        if (gameObject.GetComponent<PlayerStatus>().playerType != PlayerType.Douglas && _isLocal)
            if (enableParry)
            {
                enableParry = false;
                parrying = true;
                //StartCoroutine(PrepareParry());   
            }
    }

    private void InputSystemOnDeviceChange(InputDevice device, InputDeviceChange deviceChange)
    {
        switch (deviceChange)
        {
            case InputDeviceChange.Added:
                Debug.Log("A new device has been added! Device is: " + device.displayName);

                //TODO: Change pairing from Keyboard to Xbox and dont allow/disable any other schemes


                break;
            case InputDeviceChange.Removed:
                Debug.Log(device.displayName + "has been removed!");

                break;
            case InputDeviceChange.Disconnected:
                Debug.Log(device.displayName + "has been disconnected!");

                break;
            case InputDeviceChange.Reconnected:
                break;
            case InputDeviceChange.Enabled:
                break;
            case InputDeviceChange.Disabled:
                break;
            case InputDeviceChange.UsageChanged:
                break;
            case InputDeviceChange.ConfigurationChanged:
                break;
            case InputDeviceChange.Destroyed:
                break;
        }
    }

    private void Awake()
    {
        controls = new InputMaster();

        _isLocal = gameObject.GetComponent<PlayerStatus>().GameController.GetComponent<GameController>().isLocal;
        if (gameObject.GetComponent<PlayerStatus>().GameController.GetComponent<GameController>().isLocal)
        {
            if (gameObject.GetComponent<PlayerStatus>().playerType == PlayerType.Douglas)
            {
                //controls.Gameplay.LocalMoveP1.ApplyBindingOverridesOnMatchingControls(Gamepad.all[0]);
                //controls.Gameplay.LocalMoveP1.performed += ctx =>
                //{
                //    move = ctx.ReadValue<Vector2>();
                //};

                controls.Gameplay.LocalMoveP1.canceled += ctx =>
                {
                    move = Vector2.zero;
                };

                //controls.Gameplay.LocalParryP1.started += ctx => {
                //    if (enableParry)
                //    {
                //        enableParry = false;
                //        parrying = true;
                //        //StartCoroutine(PrepareParry());   
                //    }
                //};

                controls.Gameplay.LocalParryP1.canceled += ctx => { enableParry = true; };
            }
            else
            {
                //controls.Gameplay.LocalMoveP2.performed += ctx =>
                //{
                //    move = ctx.ReadValue<Vector2>();
                //};

                controls.Gameplay.LocalMoveP2.canceled += ctx =>
                {
                    move = Vector2.zero;
                };

                //controls.Gameplay.LocalParryP2.started += ctx =>
                //{
                //    if (enableParry)
                //    {
                //        enableParry = false;
                //        parrying = true;
                //        //StartCoroutine(PrepareParry());   
                //    }
                //};

                controls.Gameplay.LocalParryP2.canceled += ctx => { enableParry = true; };
            }
        }
        else
        {
            controls.Gameplay.Move.performed += ctx =>
            {
                move = ctx.ReadValue<Vector2>();
            };

            controls.Gameplay.Parry.started += ctx => {
                if (enableParry)
                {
                    enableParry = false;
                    parrying = true;
                    //StartCoroutine(PrepareParry());   
                }
            };

            controls.Gameplay.Parry.canceled += ctx => { enableParry = true; };
        }


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
                alreadyJumped = false;
            }
        }
       
        if (state.MoveY < -0.5f)
        {
            isDucking = true;
            state.Animator.SetBool("Duck", true);
        }
        else
        {
            isDucking = false;
            state.Animator.SetBool("Duck", false);
        }

        if (state.Parrying)
        {
            if (enableParryAnimation)
            {
                if((state.LeftArmEnable || state.RightArmEnable) && !isDucking)
                {
                    enableParryAnimation = false;
                    state.Animator.SetTrigger("ParryT");
                    StartCoroutine(ParryAnimation());
                }
                else
                {
                    parrying = false;
                }
            }
            else
            {
                parrying = false;
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

        if (!isDucking)
        {
            transform.Translate(new Vector2(m.x, 0f), Space.World);
        }
      

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

    public void disableControls()
    {
        move = Vector2.zero;
        controls.Gameplay.Disable();
        gameObject.GetComponent<PlayerInput>().enabled = false;
    }

    public void enableControls()
    {
        controls.Gameplay.Enable();
        gameObject.GetComponent<PlayerInput>().enabled = true;
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
