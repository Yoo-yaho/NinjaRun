using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour
{
    public AudioClip deathClip; // 사망시 재생할 오디오 클립
    public Vector2 velocity;
    public Vector2 playerPosition;

    public int jumpCount = 0; // 누적 점프 횟수
    public float jumpForce = 1500f; // 점프 힘
    private bool isGrounded = false; // 바닥에 닿았는지 나타냄
    public float distance = 0.0f;
    public bool isDead = false; // 사망 상태
    public bool isDoublJumped = false;
    public float runSpeed;
    public float runMaxSpeed = 1f;
    public float jumpSpeed;
    public float jumpMaxSpeed = 1f;
    public float acceleration = 0.5f;
    public float skillMana;
    public float skillRegen;
    public float cloaking = 900f;

    private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
    private Animator animator; // 사용할 애니메이터 컴포넌트
    private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트
    private SpriteRenderer spriteRenderer;
    public Transform playerTransform;
    public Image skillBar;
    public GameObject log;
    public GameObject Attack_Point;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void Start()
    {
        // 게임 오브젝트로부터 사용할 컴포넌트들을 가져와 변수에 할당
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //playerPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);
    }

    private void Update()
    {      
        if (isDead)
        {
            // 사망시 처리를 더 이상 진행하지 않고 종료
            return;
        }

        // 마우스 왼쪽 버튼을 눌렀으며 && 최대 점프 횟수(2)에 도달하지 않았다면
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            // 점프 횟수 증가
            jumpCount++;
            // 점프 직전에 속도를 순간적으로 제로(0, 0)로 변경
            playerRigidbody.velocity = Vector2.zero;
            // 리지드바디에 위쪽으로 힘을 주기
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            // 오디오 소스 재생
            playerAudio.Play();

            if (jumpCount == 2)
            {
                isDoublJumped = true;
                animator.SetTrigger("isDoubleJump");
            }
        }   

        if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            // 마우스 왼쪽 버튼에서 손을 떼는 순간 && 속도의 y 값이 양수라면 (위로 상승 중)
            // 현재 속도를 절반으로 변경
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        // 애니메이터의 Grounded 파라미터를 isGrounded 값으로 갱신
        animator.SetBool("isGrounded", isGrounded);

        if (Time.timeScale <= 1)
        {
            Time.timeScale += runSpeed / 1000;
        }

        if (jumpForce <= 700)
        {
            jumpForce += runSpeed / 500;
        }

        if (runSpeed < runMaxSpeed)
        {
            runSpeed += acceleration * Time.deltaTime;
            animator.SetFloat("RunSpeed", runSpeed);
        }

        if (jumpSpeed < jumpMaxSpeed)
        {
            jumpSpeed += acceleration * Time.deltaTime;
            animator.SetFloat("JumpSpeed", jumpSpeed);
        }

        if (isGrounded == true) jumpCount = 0;

        animator.SetFloat("yVelocity", playerRigidbody.velocity.y);

        if (Input.GetKey(KeyCode.Z))
        {
            this.gameObject.layer = 8;
            spriteRenderer.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
            skillBar.fillAmount -= skillMana * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            this.gameObject.layer = 3;
            spriteRenderer.color = new Color(1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(log, new Vector2(playerTransform.position.x, playerTransform.position.y - 1), 
                log.transform.rotation);
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 3.0f);

            //----------------------------------------------
            // 속도를 제로(0, 0)로 변경
            playerRigidbody.velocity = Vector2.zero;
            //---------------------------------------------- 연속으로, 또는 떨어지면서 사용 시 불규칙한 높이로 이동하는 오류 수정

            playerRigidbody.AddForce(new Vector2(0, cloaking));

            skillBar.fillAmount -= skillMana;
        }

        // 마우스 우클릭 / 공격 모션 재생
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("공격");
            StartCoroutine("Attacking");
            animator.SetTrigger("isAttack");
        }

        skillBar.fillAmount += skillRegen * Time.deltaTime;
    }

    IEnumerator Attacking() // 공격 모션동안 공격 콜라이더를 활성화
    {
        if (Attack_Point.activeSelf == true) // 공격 중이라면 종료
        {
            yield return null;
        }

        Attack_Point.SetActive(true); // 공격 콜라이더 활성화
        yield return new WaitForSeconds(0.4f); // 0.초 후
        Attack_Point.SetActive(false); // 공격 콜라이더 비활성화
    }

    private void FixedUpdate()
    {
        distance += runSpeed;
    }

    public void Die()
    {
        // 애니메이터의 Die 트리거 파라미터를 셋
        //animator.SetTrigger("Die");

        // 오디오 소스에 할당된 오디오 클립을 deathClip으로 변경
        //playerAudio.clip = deathClip;
        // 사망 효과음 재생
        //playerAudio.Play();

        // 속도를 제로(0, 0)로 변경
        playerRigidbody.velocity = Vector2.zero;
        // 사망 상태를 true로 변경
        isDead = true;
        runSpeed = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DeadZone" && !isDead)
        {
            // 충돌한 상대방의 태그가 Dead이며 아직 사망하지 않았다면 Die() 실행
            Die();
        }

        if (other.tag == "Obstacle" && !isDead)
        {
            runSpeed *= 0.5f;
            jumpSpeed *= 0.5f;
            jumpForce -= 100;
            Time.timeScale -= 0.2f;

            StartCoroutine(OnBeatTime(6));
        }
    }

    IEnumerator OnBeatTime(int maxTime)
    {
        int countTime = 0;

        while (countTime < maxTime)
        {
            if (countTime % 2 == 0)
                spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            else
                spriteRenderer.color = new Color(1, 1, 1, 0.8f);

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }
        spriteRenderer.color = new Color(1, 1, 1, 1);

        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 어떤 콜라이더와 닿았으며, 충돌 표면이 위쪽을 보고 있으면
        if (collision.contacts[0].normal.y > 0.7f)
        {
            // isGrounded를 true로 변경하고, 누적 점프 횟수를 0으로 리셋
            isGrounded = true;
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 어떤 콜라이더에서 떼어진 경우 isGrounded를 false로 변경
        if(collision.collider.tag == "Platform")
        {
            isGrounded = false;
        }

    }
}
