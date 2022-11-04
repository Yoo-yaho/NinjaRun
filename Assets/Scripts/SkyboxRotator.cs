using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float RotationPerSecond = 1f;
    public float acceleration = 0.5f;

    PlayerController player;

    private void Awake()
    {
        //player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    protected void FixedUpdate()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotationPerSecond);
        RotationPerSecond += acceleration * Time.deltaTime;
    }

}