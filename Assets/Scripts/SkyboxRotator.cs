using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float RotationPerSecond = 1f;
    public float acceleration = 0.5f;

    private GameObject _Player;
    
    private void Awake()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // 없을 경우, 탐색
        if(_Player == null)
        {
            _Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    protected void FixedUpdate()
    {
        // 죽었을 경우 스크롤을 시행하지 않음
        if(_Player.GetComponent<M_Player_Controller>().PlayerState == PlayerState.DEATH)
        {
            return;
        }
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotationPerSecond);
        RotationPerSecond += acceleration * Time.deltaTime;
    }

}