using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll_Emeny : MonoBehaviour
{
    public Rigidbody2D Rig;

    public float Speed = 150f;
    // Start is called before the first frame update
    void Start()
    {
        Rig = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 _Speed = new Vector2(Rig.velocity.x, 0);
        Rig.velocity = new Vector2(_Speed.x + Rig.velocity.x,Rig.velocity.y);
    }
}
