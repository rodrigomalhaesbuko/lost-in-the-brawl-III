using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioControl : MonoBehaviour
{

    public AudioSource audioSource;

    bool play;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        play = true;
        audioSource.volume = 0.1f;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(play == true)
        {
            audioSource.Play();
        }

        if(play == false)
        {
            audioSource.Stop();
        }
        */
        
    }
}
