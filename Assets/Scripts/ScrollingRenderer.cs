using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingRenderer : MonoBehaviour
{
    public float depth = 1.0f;
    float targetOffset;

    PlayerController player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        //float realVelocity = player.velocity.x / depth;

        targetOffset += depth * Time.fixedDeltaTime;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(targetOffset, 0);
    }
}
