using UnityEngine;

// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour
{
    public GameObject[] obstacles; // 장애물 오브젝트들
    public GameObject enemy;
    private bool stepped = false; // 플레이어 캐릭터가 밟았었는가

    // 컴포넌트가 활성화될때 마다 매번 실행되는 메서드
    private void OnEnable()
    {
        // 발판을 리셋하는 처리
        stepped = false;

        // ------------------------------------------------
        // 장애물을 전부 비활성화
        /*
        for (int i = 0; i < obstacles.Length; i++)
        {
            // 현재 순번의 장애물을 1/3 확률로 활성화
            if (Random.Range(0, 3) == 0)
            {
                obstacles[i].SetActive(true);
                
            }
            else
            {
                obstacles[i].SetActive(false);
            }
        }
        */
        // 적을 활성화

        if (Random.Range(0, 0) == 0)
        {
            Instantiate(enemy, transform.position + (Vector3.up * 5f) + (Vector3.right * 50.0f), transform.rotation);
        }

    }
}