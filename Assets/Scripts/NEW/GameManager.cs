using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{


    public TextMeshProUGUI _Distance_Text; // �Ÿ����� ǥ���� �ؽ�Ʈ
    public Slider _Distance_Bar; // �Ÿ����� ���� �Ÿ��� ǥ���� �����̵� ��

    public int _Distance = 0; // �÷��̾�κ��� ������ �Ÿ���

    public bool isClear;

    public GameObject _Player;

    public TextMeshProUGUI _Score_Text; // ���ھ ǥ���� �ؽ�Ʈ
    private int _Score = 0; // ���ھ�

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
