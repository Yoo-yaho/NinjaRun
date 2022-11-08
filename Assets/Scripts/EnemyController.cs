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

    // 깃허브 버전 새로운 업데이트 !

    // 적의 공격 받는 횟수 ( Life ) 를 생성
    private int Life;

    // 적이 공격을 받았을 경우, 일정 시간동안 무적 판정
    private bool Is_Attacked = false;

    // 적 스크롤링 ( Transform , Rigidbody 이동 겹치는 문제 )
    public float _Speed = 2f;

    private void Awake()
    {
        //animator = gameObject.GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // 목숨을 1 ~ 3 사이로 설정함
        Life = Random.Range(1, 4);

        // 목숨에 비례해 색 설정 ( 보호막 ? )
        // 1 : 기본 - 2 : 노랑 - 3 : 파랑

        Set_Image();

    }

    private void FixedUpdate()
    {
        
    }

    void Think()
    {
        movementFlag = Random.Range(-1, 2);

        Invoke("Think", 1);
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
    // 공격 당했을 때, 무작위 방향으로 위로 떠오르고, 콜라이더를 제거해 추락시킨다.
    // 공격 당했음을 알리기 위해 머터리얼에 접근해 R 값을 증가시켜 깜빡이는 효과를 준다.
    void Sword_Hit()
    {

        if (!Is_Attacked)

        {   
            // 적을 날리는 힘
            float _ForcePower = 1000f;

            // 적을 날리는 방향 ( 대각선 )
            Vector2 _Dir = (Vector2.right);


            // 체력을 깎음
            Life -= 1;

            // 힘을 가함 ( 가하기 전에 초기화 )
            rigidbody.AddForce((_Dir * _ForcePower), ForceMode2D.Force);

            if(Life <= 0)
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


        }

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
        yield return new WaitForSeconds(0.1f);
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
