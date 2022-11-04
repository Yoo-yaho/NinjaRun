using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObstacle : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 0.8f);
    }
}
