using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_Controller : MonoBehaviour
{
    GameObject player;
    Animator anim;

    public float distance;

    Vector2 playerPos;
    Vector2 petPos;
    public float lerpTime = 0.5f;
    float currentTime = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");

        anim.SetBool("Select", true);

        petPos = new Vector2(transform.position.x, transform.position.y);
        playerPos = new Vector2(transform.position.x, player.transform.position.y + 0.5f);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= lerpTime)
        {
            currentTime = lerpTime;
        }

        transform.position = Vector2.Lerp(petPos, (Vector2)player.transform.position + new Vector2(-1.6f, 0.8f), currentTime / lerpTime);
        //transform.position = new Vector2(transform.position.x, player.position.y + 0.2f);
    }
}
