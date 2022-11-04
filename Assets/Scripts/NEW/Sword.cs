using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Sword : MonoBehaviour
{
    public GameObject _Combo_Text; // 콤보 텍스트 연결
    public Camera _MainCam; // 연출용 메인 카메라 

    public int _Hit_Count = 0; // 공격이 연속해서 명중하는 것을 계산하는 Count 변수
    public int _Hit_Time = 6; // 공격이 이 시간 내에 명중하지 않을 경우 Count를 초기화함
    public float _Hit_Time_P = 0; // 공격이 시행되고 나서의 시간 - 계속해서 업데이트

    public Color _Hit_Color = new Color(1, 1, 1, 0); // 콤보 이미지의 알파값

    void Start()
    {

        _MainCam = Camera.main;
        
    }

    void Update()
    {

        _Hit_Time_P += Time.deltaTime; // 콤보 초기화 시간을 초 단위로 증가 

        if (_Hit_Time_P >= _Hit_Time) // 만약 콤보 초기화 시간이 지났다면
        {
            _Hit_Count = 0; // 콤보 카운트 초기화
        }

        
        if(_Hit_Count >= 1) // 히트 카운트가 1보다 클 때만 연산함
        {
            // 알파값이 0보다 크다면, 시간 초에 따라 알파값을 지속적으로 감소시킴
            _Hit_Color.a = 1 - (_Hit_Time_P / _Hit_Time);
            _Combo_Text.GetComponent<TextMeshProUGUI>().color = _Hit_Color;
        }

    }

    // 공격 적중시의 판정 적용 //
    void Hit()
    {
        Apply();
        Draw_Text();
        Cam_Move();

    }

    // 적중 효과 업데이트
    void Apply()
    {
        _Hit_Time_P = 0; // 콤보 초기화 시간을 초기화
        _Hit_Count += 1; // 콤보 카운트 수를 증가
        _Hit_Color = new Color(1, 1, 1, 1); // 색상 초기화
        _Combo_Text.GetComponent<TextMeshProUGUI>().color = _Hit_Color; // 색상 적용

    }

    // 텍스트를 업데이트
    void Draw_Text()
    {
        _Combo_Text.GetComponent<TextMeshProUGUI>().text = ($"COMBO X {_Hit_Count:D3}");
    }

    void Cam_Move()
    {
        _MainCam.orthographicSize = 5;
    }

}
