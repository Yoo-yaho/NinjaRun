using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어의 상태를 정의합니다
public enum PlayerState
{
    RUN, JUMP, D_JUMP, ATTACK, DEATH
}
public class M_Player_Controller : MonoBehaviour
{
    // 플레이어의 상태 정의 변수
    public PlayerState PlayerState;

    public GameObject _Jump_Effect; // 점프 이펙트 오브젝트
    public GameObject _D_Jump_Effect; // 더블 점프 이펙트 오브젝트
    public GameObject _Attack_Effect; // 공격 이펙트 오브젝트

    // -------------------------
    // 컴포넌트 변수 정의
    private Rigidbody2D _PlayerRigidbody;
    private Animator _PlayerAnimator;
    private AudioSource _PlayerAudio;
    public AudioClip[] _PlayerClip;
    private SpriteRenderer _PlayerSprite;

    // -------------------------

    private int _Jump_Count = 0; // 누적 점프 횟수
    private int _Jump_Power = 15; // 점프의 힘

    public float _Distance = 0; // 플레이어가 달려간 거리
    public int _Distance_Speed = 5; // 플레이어가 1초 동안 달려가는 거리

    public GameObject _Sword; // 근접 공격 오브젝트를 공격을 통화 활성화/비활성화

    private bool _IsGrounded = false; // 바닥에 닿았는지 체크함
    private bool _IsDoubleJump = false; // 더블 점프를 했는지 체크함
    private bool _IsAttack = false; // 점프 대신 공격 명령어를 사용하는지 체크함
    private bool _IsDie = false; // 죽음 처리 체크

    void Awake()
    {
        // 컴포넌트 부착
        _PlayerRigidbody = GetComponent<Rigidbody2D>();
        _PlayerAnimator = GetComponent<Animator>();
        _PlayerAudio = GetComponent<AudioSource>();
        _PlayerSprite = GetComponent<SpriteRenderer>();

        _PlayerAnimator.SetBool("DIE", false);
    }

    void Update()
    {

        Landing_Check();

        Distance_Check();

    }

    void FixedUpdate()
    {

    }

    void LateUpdate()
    {

    }

    /*
    void State_Move_Mobie()
    {
        // 플레이어가 사망 상태라면 실행하지 않음
        if (PlayerState == PlayerState.DEATH)
        {
            Die();
            return;
        }

        // 화면 아무 곳이나 터치했다면
        if (Input.touchCount > 0)
        {
            // 첫 번째 터치가 입력됐다면
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // 공격이 가능한지 체크함
                Attack_Check();

                // 더블 점프 상태라면, 종료함 (반응 없음)
                if (PlayerState == PlayerState.D_JUMP)
                {
                    if (_IsAttack)
                    {
                        StartCoroutine("Attack");
                    }

                    return;
                }

                // 점프 상태라면, 더블 점프 상태로 전환
                if (PlayerState == PlayerState.JUMP)
                {
                    if (_IsAttack)
                    {
                        StartCoroutine("Attack");
                    }

                    D_Jump();
                }

                // 달리고 있는 상태라면, 점프 상태로 전환
                if (PlayerState == PlayerState.RUN)
                {

                    // 공격이 가능한 상태라면, 공격 상태로 전환 
                    if (_IsAttack)
                    {
                        StartCoroutine("Attack");
                    }
                    else
                    {
                        // 점프 명령어 실행
                        Jump();

                    }

                }
            }

        }
    }

    void State_Move()
    {
        // 플레이어가 사망 상태라면 실행하지 않음
        if (PlayerState == PlayerState.DEATH)
        {
            Die();
            return;
        }

        // 마우스 좌클릭을 눌렀다면
        if (Input.GetMouseButtonDown(0))
        {
            Attack_Check();

            // 더블 점프 상태라면, 종료함 (반응 없음)
            if (PlayerState == PlayerState.D_JUMP)
            {

                // 공격이 가능한 상태라면, 공격 상태로 전환 
                if (_IsAttack)
                {
                    StartCoroutine("Attack");
                }

                return;
            }

            // 점프 상태라면, 더블 점프 상태로 전환
            if (PlayerState == PlayerState.JUMP)
            {

                // 공격이 가능한 상태라면, 공격 상태로 전환 
                if (_IsAttack)
                {
                    StartCoroutine("Attack");
                    return;
                }

                D_Jump();
            }

            // 달리고 있는 상태라면, 점프 상태로 전환
            if (PlayerState == PlayerState.RUN)
            {

                // 공격이 가능한 상태라면, 공격 상태로 전환 
                if (_IsAttack)
                {
                    StartCoroutine("Attack");
                }
                else
                {
                    // 점프 명령어 실행
                    Jump();

                }

            }

        }
    }

    */
    void Distance_Check()
    {
        _Distance += _Distance_Speed * Time.deltaTime;
    }

    // 공격 코루틴
    IEnumerator Attack()
    {

        _Sword.GetComponent<BoxCollider2D>().enabled = true; // 콜라이더 활성화
        _PlayerAnimator.SetTrigger("ATTACK"); // 공격 애니메이션 재생

        // 공격 효과 활성화
        _Attack_Effect.SetActive(true);

        // 공격 사운드 재생
        _PlayerAudio.clip = _PlayerClip[0];
        _PlayerAudio.Play();

        yield return new WaitForSeconds(0.06f); // 지연

        _Sword.GetComponent<BoxCollider2D>().enabled = false; // 콜라이더 비활성화

    }

    void Run()
    {
        PlayerState = PlayerState.RUN; // 상태를 달리기 상태로 전환

        // 달리기 애니메이션으로 전환
        _PlayerAnimator.SetBool("RUN", true);
        _PlayerAnimator.SetBool("JUMP", false);
        _PlayerAnimator.SetBool("D_JUMP", false);

    }

    void Jump()
    {
        PlayerState = PlayerState.JUMP; // 상태를 점프 상태로 전환

        _PlayerRigidbody.velocity = Vector2.zero; // 순간 속도를 0으로 설정
        _PlayerRigidbody.AddForce(new Vector2(0, _Jump_Power), ForceMode2D.Impulse); // 점프 출력

        // 점프 애니메이션으로 전환
        _PlayerAnimator.SetBool("RUN", false);
        _PlayerAnimator.SetBool("JUMP", true);
        _PlayerAnimator.SetBool("D_JUMP", false);

        // 점프 효과 활성화
        _Jump_Effect.SetActive(true);

        // 점프 사운드 재생
        _PlayerAudio.clip = _PlayerClip[1];
        _PlayerAudio.Play();

    }

    void D_Jump()
    {
        PlayerState = PlayerState.D_JUMP; // 상태를 더블 점프로 전환

        _PlayerRigidbody.velocity = Vector2.zero; // 순간 속도를 0으로 설정
        _PlayerRigidbody.AddForce(new Vector2(0, _Jump_Power), ForceMode2D.Impulse); // 점프 출력

        // 더블 점프 이펙트 활성화
        _D_Jump_Effect.SetActive(true);

        // 더블 점프 애니메이션으로 전환
        _PlayerAnimator.SetBool("RUN", false);
        _PlayerAnimator.SetBool("JUMP", false);
        _PlayerAnimator.SetBool("D_JUMP", true);

        // 점프 사운드 재생
        _PlayerAudio.clip = _PlayerClip[2];
        _PlayerAudio.Play();
    }

    // 착지 체크 ( 레이캐스트 활용 )
    void Landing_Check()
    {

        float _MaxDistance = 1.5f; // 레이의 최대 거리 변수

        // 레이 테스트용 그리기
        Debug.DrawRay(transform.position, Vector2.down * _MaxDistance, Color.blue, 0.1f);

        // 플레이어가 사망 상태라면, 실행하지 않음
        if (PlayerState == PlayerState.DEATH)
        {
            return;
        }

        // 플레이어가 점프/더블 점프 상태에서 떨어지고 있을 때만 레이를 발산합니다.

        if ((_PlayerRigidbody.velocity.y <= 0 &&
            (PlayerState == PlayerState.JUMP || PlayerState == PlayerState.D_JUMP)))
        {
            RaycastHit2D _Hit = Physics2D.Raycast(
            transform.position, Vector2.down, _MaxDistance, LayerMask.GetMask("Platform"));

            // 충돌체가 있다면, 충돌체를 체크합니다.
            if (_Hit.collider != null)
            {
                Debug.Log(_Hit.collider.tag);

                // 충돌체가 플랫폼이라면 달리기 상태로 전환합니다.
                if (_Hit.collider.tag == "Platform")
                {
                    Run();
                }

            }
            else // 충돌체가 없다면, 종료합니다.
            {

                return;
            }

        }


    }

    void Attack_Check()
    {
        float _MaxDistance = 4f; // 레이의 최대 거리 변수

        // 레이 테스트용 그리기
        Debug.DrawRay(transform.position, Vector2.right * _MaxDistance, Color.red, 0.1f);

        // 플레이어가 사망 상태라면, 실행하지 않음
        if (PlayerState == PlayerState.DEATH)
        {
            return;
        }

        // 플레이어가 공격 상태가 아닐때만 레이를 발산합니다. 취소.
        if (true)
        {
            RaycastHit2D _Hit = Physics2D.Raycast(
           transform.position, Vector2.right, _MaxDistance, LayerMask.GetMask("Enemy"));

            // 충돌체가 있다면, 공격을 할 수 있게 활성화 합니다.
            if (_Hit.collider != null)
            {
                _IsAttack = true;
            }
            else // 아니라면, 활성화하지 않고 종료합니다.
            {
                _IsAttack = false;
            }
        }



    }

    // 트리거
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 만약 데드존에 닿았을 시
        if (other.tag == "DeadZone" && PlayerState != PlayerState.DEATH)
        {
            PlayerState = PlayerState.DEATH;
            _Distance_Speed = 0;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy" && PlayerState != PlayerState.DEATH)
        {
            PlayerState = PlayerState.DEATH;
            Die();
            _Distance_Speed = 0;
        }
    }

    void Die()
    {
        if (!_IsDie)
        {
            _PlayerAnimator.SetTrigger("DIE");
            BroadcastMessage("DIE");
            _IsDie = true;
        }

    }

    public void Jump_Button()
    { 
        
        // 플레이어가 사망 상태라면 실행하지 않음
        if (PlayerState == PlayerState.DEATH)
        {
            return;
        }

        // 더블 점프 상태라면 종료함
        if (PlayerState == PlayerState.D_JUMP)
        {
            return;
        }

        // 점프 상태라면, 더블 점프 상태로 전환
        if (PlayerState == PlayerState.JUMP)
        {
            D_Jump();
        }

        // 달리고 있는 상태라면, 점프 상태로 전환
        if (PlayerState == PlayerState.RUN)
        {

            Jump();

        }

    }

    public void Attack_Button()
    {
        if(PlayerState == PlayerState.DEATH)
        {
            return;
        }

        StartCoroutine("Attack");
    }



}