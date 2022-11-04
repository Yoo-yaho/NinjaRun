using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_Select : MonoBehaviour
{
    public Pet pet;
    public Pet_Select[] pets;
    Animator anim;
    SpriteRenderer spriteRenderer;
    Transform transform;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();

        if (Pet_Data.instance.currentPet == pet) OnSelect();
        else OnDeSelect();
    }

    private void OnMouseUpAsButton()
    {
        Pet_Data.instance.currentPet = pet;
        OnSelect();

        for (int i = 0; i < pets.Length; i++)
        {
            if (pets[i] != this) pets[i].OnDeSelect();
        }
    }

    private void OnDeSelect()
    {
        anim.SetBool("Select", false);
        spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
    }

    void OnSelect()
    {
        anim.SetBool("Select", true);
        spriteRenderer.color = new Color(1f, 1f, 1f);
        //transform.localScale = new Vector2(4f, 4f);
    }
}
