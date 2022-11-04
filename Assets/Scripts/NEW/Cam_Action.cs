using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Action : MonoBehaviour
{
    public GameObject _Player;
    public GameObject _Player_Sword;

    void Start()
    {

        _Player = GameObject.FindGameObjectWithTag("Player");
        _Player_Sword = GameObject.FindGameObjectWithTag("Sword");
        
    }
}
