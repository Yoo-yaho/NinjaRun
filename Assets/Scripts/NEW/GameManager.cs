using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{


    public TextMeshProUGUI _Distance_Text; // 거리값을 표시할 텍스트
    public Slider _Distance_Bar; // 거리값을 통해 거리를 표시할 슬라이드 바

    public int _Distance = 0; // 플레이어로부터 추출한 거리값

    public bool isClear;

    public GameObject _Player;

    public TextMeshProUGUI _Score_Text; // 스코어를 표시할 텍스트
    private int _Score = 0; // 스코어

    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        _Distance_Text = GameObject.Find("Canvas/DistanceText").GetComponent<TextMeshProUGUI>();
        _Distance_Bar = GameObject.Find("Canvas/New_Distance").GetComponent<Slider>();

        isClear = false;
    }
    
    void Update()
    {
        _Distance = (int)_Player.GetComponent<M_Player_Controller>()._Distance;

        _Distance_Text.text = string.Format("{0}M", _Distance);
        _Distance_Bar.value = _Distance * 2.5f;


        if (_Distance_Bar.value == 1000)
            isClear = true;
    }

    public void Add_Score(int Value)
    {
        _Score += Value;
    }
}
