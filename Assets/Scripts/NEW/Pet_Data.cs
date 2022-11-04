using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pet
{
    Bird, Rat, Dragon
}

public class Pet_Data : MonoBehaviour
{
    public static Pet_Data instance;

    private void Awake()
    {
        if (instance == null) instance = this;

        else if (instance != null) return;

        DontDestroyOnLoad(gameObject);
    }

    public Pet currentPet;
}
