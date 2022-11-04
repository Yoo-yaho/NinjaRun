using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    [SerializeField] [Range(1f, 40f)] float speed = 3f;

    [SerializeField] float posValue;

    Vector2 startPos;
    float newPos;
    public float acceleration = 0.5f;

    public GameObject Player;

    private void Awake()
    {
        //player = GameObject.Find("Player").GetComponent<PlayerController>();
        startPos = transform.position;
    }

    private void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        if (Player.GetComponent<M_Player_Controller>().PlayerState == PlayerState.DEATH)
        {
            return;
        }

        speed += acceleration * (Time.timeSinceLevelLoad / 10000);
        newPos = Mathf.Repeat(Time.time * speed, posValue);
        transform.position = startPos + Vector2.left * newPos;

        //if (player.ishit == true)
        //{
        //  speed *= 0.7f;
        //}
    }
}
