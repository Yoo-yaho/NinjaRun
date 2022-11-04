using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    public AudioClip _UI_Click;

    private AudioSource _AudioSource;

    public void Awake()
    {
        _AudioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(this);
    }

    public void Play()
    {
        _AudioSource.clip = _UI_Click;
        _AudioSource.Play();
    }
}
