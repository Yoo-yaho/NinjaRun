using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerController player;
    public GameObject hitBox;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        Vector2 pos = this.transform.position;

        pos.x -= player.velocity.x * Time.fixedDeltaTime;

        if (pos.x < 0)
            Destroy(this.gameObject);

        transform.position = pos;
    }

    public void HitPlayer()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Instantiate(hitBox, this.transform.position, Quaternion.identity);
        }
    }
}
