using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int movementFlag;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private Collider2D collider;
    private SpriteRenderer spriterenderer;



    // 적 플랫폼 낙사 관련한 패치

    // 플랫폼을 감지하는 콜라이더 오브젝트
    private GameObject _Platform;

    // 스크롤 속도를 변경하는 ScrollingObject 스크립트
    private ScrollingObject _ScrollingObject;

    // 깃허브 버전 새로운 업데이트 !

    // 적의 공격 받는 횟수 ( Life ) 를 생성
    private int Life;

    // 적 스코어링을 위한 초기 값 저장
    private int _ScoreLife;

    // 적이 공격을 받았을 경우, 일정 시간동안 무적 판정
    private bool Is_Attacked = false;

    // 적 스크롤링 ( Transform , Rigidbody 이동 겹치는 문제 )
    public float _Speed = 2f;

    // 스코어 호출을 위한 캔버스스크립트
    private GameObject _Canvas;

    private void Awake()
    {
        //animator = gameObject.GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        spriterenderer = GetComponent<SpriteRenderer>();

        _Platform = transform.GetChild(0).gameObject;
        _ScrollingObject = GetComponent<ScrollingObject>();

        _Canvas = GameObject.Find("Canvas");

    }

    private void Start()
    {
        // 목숨을 1 ~ 3 사이로 설정함
        Life = Random.Range(1, 4);

        _ScoreLife = Life;

        // 목숨에 비례해 색 설정 ( 보호막 ? )
        // 1 : 기본 - 2 : 노랑 - 3 : 파랑

        Set_Image();

    }


    private void FixedUpdate()
    {
        if (_Platform.GetComponent<Enemy_PlatformCheck>()._IsCheck)
        {  
            // 속도가 작다면 ( 정지 속도 10f 라면 )
            if (_ScrollingObject.speed < 11)
            {
                // 기본 이동속도로 전환함
                _ScrollingObject.speed = 12f;
            }


        }
        else
        {

            // 속도가 크다면 ( 기본 속도 12f 라면 )
            if (_ScrollingObject.speed > 11)
            {
                // 플랫폼의 이동 속도와 동일시함
                _ScrollingObject.speed = 10f;
            }
        }
    }


    /*IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);

        if (movementFlag == 0)
            animator.SetBool("isMoving", false);
        else
            animator.SetBool("isMoving", true);

        yield return new WaitForSeconds(2f);

        StartCoroutine("ChangeMovement");
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword"))
        {
            collision.gameObject.SendMessage("Hit");
            Sword_Hit();

        }

        if (collision.CompareTag("DeadZone"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDisable()
    {
        
    }

    private void OnDestroy()
    {
        _Canvas.GetComponent<UIController>().Score_Up((_ScoreLife - 1) * 10);
    }

    // 공격 당했을 때, 무작위 방향으로 위로 떠오르고, 콜라이더를 제거해 추락시킨다.
    // 공격 당했음을 알리기 위해 머터리얼에 접근해 R 값을 증가시켜 깜빡이는 효과를 준다.
    void Sword_Hit()
    {
        // 예외 처리
        if (Is_Attacked)
        {
            return;
        }

        // 적을 날리는 힘
        float _ForcePower = 800f;

        // 적을 날리는 방향 ( 대각선 )
        Vector2 _Dir = (Vector2.right);


        // 체력을 깎음
        Life -= 1;

        // 힘을 가함 ( 가하기 전에 초기화 )
        rigidbody.AddForce((_Dir * _ForcePower), ForceMode2D.Force);

        if (Life <= 0)
        {
            collider.enabled = false; // 콜라이더 비활성화

            Destroy(this.gameObject, 3.0f); //3초 뒤에 제거
        }
        else
        {
            StartCoroutine("Attack_Check");
        }


        // 이미지를 전환함
        Set_Image();



        /*
          
        if (Life <= 0)
        {
            collider.enabled = false; // 콜라이더 비활성화

            Destroy(this.gameObject, 3.0f); //3초 뒤에 제거
        }

        */

    }

    IEnumerator Attack_Check()
    {
        Is_Attacked = true;
        yield return new WaitForSeconds(0.05f);
        Is_Attacked = false;

    }

    // 공격 당했음을 알리기 위해 머터리얼에 접근해 R 값을 증가시켜 깜빡이는 효과를 준다.
    void Set_Image()
    {
        // 생명 값에 따라 색상을 조절함
        switch (Life)
        {
            case 0:
                spriterenderer.color = Color.red;

                break;

            case 1:
                spriterenderer.color = Color.white;

                break;

            case 2:
                spriterenderer.color = Color.yellow;

                break;

            case 3:
                spriterenderer.color = Color.blue;

                break;
        }
    }
}
