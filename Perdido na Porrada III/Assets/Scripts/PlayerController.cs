using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Photon.MonoBehaviour
{
    // Start is called before the first frame update
    public PhotonView photonView;
    public Rigidbody2D rb;
    public GameObject PlayerCamera;

    public float speed = 10.0f;

    private void Awake()
    {
        if (photonView.isMine)
        {
            Debug.Log("eu tenho photon view");
            PlayerCamera.SetActive(true);
        }
    }


    private void CheckInputs()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        //rb.apply move * speed * Time.deltaTim
        rb.AddForce(move * speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            CheckInputs();
        }
    }
}
