using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class configController : MonoBehaviour
{

    InputMaster controls;

    float posx = -895;
    int dir = 1;

    int pos = 1;

    const int dist = 30;
    float vel = dist * 1.5f;

    // Start is called before the first frame update
    void Awake()
    {
        controls = new InputMaster();
        //controls.StaticScene.Move.performed += ctx => Move(ctx.);

        controls.StaticScene.Select.performed += _ => go();
    }

    void go()
    {
        if(pos == 1)
        {
            SceneManager.LoadScene("initialScreen");
        }

        Debug.Log("bla");
    }

    void Move(bool m)
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(posx, 429, 0);

        if (posx < -930)
        {
            dir = 1;
        }

        if (posx > -900)
        {
            dir = -1;
        }

        posx += dir * Time.deltaTime * vel;
    }


    private void OnEnable()
    {
        controls.StaticScene.Enable();
    }

    private void OnDisable()
    {
        controls.StaticScene.Disable();
    }
}
