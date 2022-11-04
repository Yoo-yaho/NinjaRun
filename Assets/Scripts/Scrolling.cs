using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float depth = 1.0f;
    public float xThershhold = 0.0f;
    public float xRespawn = 0.0f;

    PlayerController player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        float realVelocity = player.velocity.x / depth;
        Vector2 pos = this.transform.position;

        pos.x -= realVelocity * Time.fixedDeltaTime;

        if (pos.x <= xThershhold)
        {
            pos.x = xRespawn;
        }

        this.transform.position = pos;
    }
}
