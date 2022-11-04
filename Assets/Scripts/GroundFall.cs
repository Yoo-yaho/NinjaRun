using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFall : MonoBehaviour
{
    bool shouldFall = false;
    public float fallSpeed = 1.0f;

    public PlayerController player;
    public List<Obstacle> obstacles = new List<Obstacle>();

    private void FixedUpdate()
    {
        if (shouldFall)
        {
            Vector2 pos = transform.position;
            float fallAmount = fallSpeed * Time.fixedDeltaTime;
            pos.y -= fallAmount;

            if (player != null)
            {
                player.groundHeight -= fallAmount;

                Vector2 playerPos = player.transform.position;
                playerPos.y -= fallAmount;
                player.transform.position = playerPos;
            }

            foreach (Obstacle o in obstacles)
            {
                if (o != null)
                {
                    Vector2 obstaclePos = o.transform.position;
                    obstaclePos.y -= fallAmount;
                    o.transform.position = obstaclePos;
                }             
            }
            transform.position = pos;
        }
        else
        {
            if (player != null)
            {
                shouldFall = true;
            }
        }
    }
}
