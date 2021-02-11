using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeComponent : MonoBehaviour
{
    public bool onFloor = false;
    public GameObject limbHitbox;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("ground"))
        {
            onFloor= true;
        }
        //if (limbHitbox != null)
        //{
        //    limbHitbox.SetActive(false);
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ground"))
        {
            onFloor = false;
        }
    }
}
