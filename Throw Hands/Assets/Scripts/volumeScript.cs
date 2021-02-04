using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class volumeScript : MonoBehaviour
{
    InputMaster controls;

    Vector2 m;

    int vel = 700;

    float volume;

    float posx; 

    // Start is called before the first frame update
    void Awake()
    {
        volume = PlayerPrefs.GetFloat("volume");

        posx = volume * 850 - 120;
        controls = new InputMaster();

        controls.StaticScene.Move.performed += ctx => m = ctx.ReadValue<Vector2>();
        controls.StaticScene.Move.canceled += _ => m = Vector2.zero;

        controls.StaticScene.Select.performed += _ => PlayerPrefs.SetFloat("volume", volume);
    }

    // Update is called once per frame
    void Update()
    {
        posx += m.x * vel * Time.deltaTime;

        if (posx < -120)
            posx = -120;
        else if (posx > 730)
            posx = 730;

        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(posx, 220, 0);

        volume = (posx + 120) / 850;
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
