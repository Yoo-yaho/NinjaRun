using UnityEngine;

// 게임 오브젝트를 계속 왼쪽으로 움직이는 스크립트
public class ScrollingObject : MonoBehaviour {

    public float speed = 12f; // 이동 속도
    public float acceleration = 0.5f;

    private void Update() {
        // 게임 오브젝트를 왼쪽으로 일정 속도로 평행 이동하는 처리
        this.transform.Translate(Vector3.left * speed * Time.deltaTime);
        // ------------------------------------------------------
        // 속도 증가 비활성화
        
        // speed += acceleration * (Time.timeSinceLevelLoad / 3000);
        // ------------------------------------------------------
    }
}