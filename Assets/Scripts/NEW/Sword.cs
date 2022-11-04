using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Sword : MonoBehaviour
{
    public GameObject _Combo_Text; // �޺� �ؽ�Ʈ ����
    public Camera _MainCam; // ����� ���� ī�޶� 

    public int _Hit_Count = 0; // ������ �����ؼ� �����ϴ� ���� ����ϴ� Count ����
    public int _Hit_Time = 6; // ������ �� �ð� ���� �������� ���� ��� Count�� �ʱ�ȭ��
    public float _Hit_Time_P = 0; // ������ ����ǰ� ������ �ð� - ����ؼ� ������Ʈ

    public Color _Hit_Color = new Color(1, 1, 1, 0); // �޺� �̹����� ���İ�

    void Start()
    {

        _MainCam = Camera.main;
        
    }

    void Update()
    {

        _Hit_Time_P += Time.deltaTime; // �޺� �ʱ�ȭ �ð��� �� ������ ���� 

        if (_Hit_Time_P >= _Hit_Time) // ���� �޺� �ʱ�ȭ �ð��� �����ٸ�
        {
            _Hit_Count = 0; // �޺� ī��Ʈ �ʱ�ȭ
        }

        
        if(_Hit_Count >= 1) // ��Ʈ ī��Ʈ�� 1���� Ŭ ���� ������
        {
            // ���İ��� 0���� ũ�ٸ�, �ð� �ʿ� ���� ���İ��� ���������� ���ҽ�Ŵ
            _Hit_Color.a = 1 - (_Hit_Time_P / _Hit_Time);
            _Combo_Text.GetComponent<TextMeshProUGUI>().color = _Hit_Color;
        }

    }

    // ���� ���߽��� ���� ���� //
    void Hit()
    {
        Apply();
        Draw_Text();
        Cam_Move();

    }

    // ���� ȿ�� ������Ʈ
    void Apply()
    {
        _Hit_Time_P = 0; // �޺� �ʱ�ȭ �ð��� �ʱ�ȭ
        _Hit_Count += 1; // �޺� ī��Ʈ ���� ����
        _Hit_Color = new Color(1, 1, 1, 1); // ���� �ʱ�ȭ
        _Combo_Text.GetComponent<TextMeshProUGUI>().color = _Hit_Color; // ���� ����

    }

    // �ؽ�Ʈ�� ������Ʈ
    void Draw_Text()
    {
        _Combo_Text.GetComponent<TextMeshProUGUI>().text = ($"COMBO X {_Hit_Count:D3}");
    }

    void Cam_Move()
    {
        _MainCam.orthographicSize = 5;
    }

}
