using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioControl : MonoBehaviour
{

    public static AudioSource audioSource;
    public static AudioClip damegeSource;
    public static AudioClip attackSource;
    public static AudioClip introSource;
    public static AudioClip loseSource;
    public static AudioClip winSource;

    bool play;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        play = true;
        audioSource.volume = PlayerPrefs.GetFloat("volume");
        audioSource.Play();

        damegeSource = Resources.Load<AudioClip>("damage");
        attackSource = Resources.Load<AudioClip>("punch");
        introSource = Resources.Load<AudioClip>("intro");
        loseSource = Resources.Load<AudioClip>("lose");
        winSource = Resources.Load<AudioClip>("win");
    }

    public static void PlaySound(SFXType clip)
    {
        switch (clip) {
            case SFXType.Damage:
                audioSource.PlayOneShot(damegeSource);
                break;
            case SFXType.Punch:
                audioSource.PlayOneShot(attackSource);
                break;
            case SFXType.Intro:
                audioSource.PlayOneShot(introSource);
                break;
            case SFXType.Lose:
                audioSource.PlayOneShot(loseSource);
                break;
            case SFXType.Win:
                audioSource.PlayOneShot(winSource);
                break;
        }
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

public enum SFXType
{ 
    Damage,
    Punch,
    Intro,
    Lose,
    Win
}