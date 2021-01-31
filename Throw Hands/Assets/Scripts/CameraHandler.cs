using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Bolt.Matchmaking;

public class CameraHandler : MonoBehaviour
{
    public GameObject hostPositionX;
    public GameObject clientPositionX;

    private void Update()
    {
        if (clientPositionX !=null)
        {
            //Debug.Log("HOST");
            //Debug.Log(hostPositionX.transform.position.x);
            //Debug.Log("CLIENT");
            //Debug.Log(clientPositionX.transform.position.x);

            

            
        }
        
    }
}
