using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip[] _BGMSound;

    private AudioSource _AudioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        _AudioSource = GetComponent<AudioSource>();
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < _BGMSound.Length; i++)
        {
            if (arg0.name == _BGMSound[i].name)
            {
                _AudioSource.loop = false;
                BGMPlay(_BGMSound[i]);
            }            

            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                _AudioSource.loop = true;
                BGMPlay(_BGMSound[2]);
            }            
        }     
    }

    public void BGMPlay(AudioClip clip)
    {
        _AudioSource.clip = clip;
        _AudioSource.Play();
    }
}
