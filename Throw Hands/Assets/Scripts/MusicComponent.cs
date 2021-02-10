using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicComponent : MonoBehaviour
{
    private AudioSource _audioSource;
    private static MusicComponent instance = null;

    public static MusicComponent Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioSource.volume = PlayerPrefs.GetFloat("volume");

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}
