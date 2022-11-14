using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_Spawn : MonoBehaviour
{
    public GameObject[] petPrefabs;
    public GameObject pet;

    private void Start()
    {
        pet = Instantiate(petPrefabs[(int)Pet_Data.instance.currentPet]);
        pet.transform.position = transform.position;
        pet.transform.localScale = new Vector2(1, 1);
    }
}
