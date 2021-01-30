using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class PlayerController : Bolt.EntityBehaviour<ICustomPlayerState>
{

    public float speed = 10.0f;
    public Rigidbody2D rb;

    //void start para o bolt
    public override void Attached()
    {
        state.SetTransforms(state.PlayerTransform, gameObject.transform);
    }

    // update para o bolt 
    public override void SimulateOwner()
    {
        CheckInputs();
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
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        //rb.apply move * speed * Time.deltaTim
        transform.position += move * speed * BoltNetwork.FrameDeltaTime;
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
