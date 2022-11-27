using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾��� ���¸� �����մϴ�
public enum PlayerState
{
    RUN, JUMP, D_JUMP, ATTACK, DEATH
}
public class M_Player_Controller : MonoBehaviour
{
    // �÷��̾��� ���� ���� ����
    public PlayerState PlayerState;

    public GameObject _Jump_Effect; // ���� ����Ʈ ������Ʈ
    public GameObject _D_Jump_Effect; // ���� ���� ����Ʈ ������Ʈ
    public GameObject _Attack_Effect; // ���� ����Ʈ ������Ʈ

    // -------------------------
    // ������Ʈ ���� ����
    private Rigidbody2D _PlayerRigidbody;
    private Animator _PlayerAnimator;
    private AudioSource _PlayerAudio;
    public AudioClip[] _PlayerClip;
    private SpriteRenderer _PlayerSprite;

    // -------------------------

    private int _Jump_Count = 0; // ���� ���� Ƚ��
    private int _Jump_Power = 15; // ������ ��

    public float _Distance = 0; // �÷��̾ �޷��� �Ÿ�
    public int _Distance_Speed = 5; // �÷��̾ 1�� ���� �޷����� �Ÿ�

    public GameObject _Sword; // ���� ���� ������Ʈ�� ������ ��ȭ Ȱ��ȭ/��Ȱ��ȭ

    private bool _IsGrounded = false; // �ٴڿ� ��Ҵ��� üũ��
    private bool _IsDoubleJump = false; // ���� ������ �ߴ��� üũ��
    private bool _IsAttack = false; // ���� ��� ���� ��ɾ ����ϴ��� üũ��
    private bool _IsDie = false; // ���� ó�� üũ

    void Awake()
    {
        // ������Ʈ ����
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
        // �÷��̾ ��� ���¶�� �������� ����
        if (PlayerState == PlayerState.DEATH)
        {
            Die();
            return;
        }

        // ȭ�� �ƹ� ���̳� ��ġ�ߴٸ�
        if (Input.touchCount > 0)
        {
            // ù ��° ��ġ�� �Էµƴٸ�
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // ������ �������� üũ��
                Attack_Check();

                // ���� ���� ���¶��, ������ (���� ����)
                if (PlayerState == PlayerState.D_JUMP)
                {
                    if (_IsAttack)
                    {
                        StartCoroutine("Attack");
                    }

                    return;
                }

                // ���� ���¶��, ���� ���� ���·� ��ȯ
                if (PlayerState == PlayerState.JUMP)
                {
                    if (_IsAttack)
                    {
                        StartCoroutine("Attack");
                    }

                    D_Jump();
                }

                // �޸��� �ִ� ���¶��, ���� ���·� ��ȯ
                if (PlayerState == PlayerState.RUN)
                {

                    // ������ ������ ���¶��, ���� ���·� ��ȯ 
                    if (_IsAttack)
                    {
                        StartCoroutine("Attack");
                    }
                    else
                    {
                        // ���� ��ɾ� ����
                        Jump();

                    }

                }
            }

        }
    }

    void State_Move()
    {
        // �÷��̾ ��� ���¶�� �������� ����
        if (PlayerState == PlayerState.DEATH)
        {
            Die();
            return;
        }

        // ���콺 ��Ŭ���� �����ٸ�
        if (Input.GetMouseButtonDown(0))
        {
            Attack_Check();

            // ���� ���� ���¶��, ������ (���� ����)
            if (PlayerState == PlayerState.D_JUMP)
            {

                // ������ ������ ���¶��, ���� ���·� ��ȯ 
                if (_IsAttack)
                {
                    StartCoroutine("Attack");
                }

                return;
            }

            // ���� ���¶��, ���� ���� ���·� ��ȯ
            if (PlayerState == PlayerState.JUMP)
            {

                // ������ ������ ���¶��, ���� ���·� ��ȯ 
                if (_IsAttack)
                {
                    StartCoroutine("Attack");
                    return;
                }

                D_Jump();
            }

            // �޸��� �ִ� ���¶��, ���� ���·� ��ȯ
            if (PlayerState == PlayerState.RUN)
            {

                // ������ ������ ���¶��, ���� ���·� ��ȯ 
                if (_IsAttack)
                {
                    StartCoroutine("Attack");
                }
                else
                {
                    // ���� ��ɾ� ����
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

    // ���� �ڷ�ƾ
    IEnumerator Attack()
    {

        _Sword.GetComponent<BoxCollider2D>().enabled = true; // �ݶ��̴� Ȱ��ȭ
        _PlayerAnimator.SetTrigger("ATTACK"); // ���� �ִϸ��̼� ���

        // ���� ȿ�� Ȱ��ȭ
        _Attack_Effect.SetActive(true);

        // ���� ���� ���
        _PlayerAudio.clip = _PlayerClip[0];
        _PlayerAudio.Play();

        yield return new WaitForSeconds(0.06f); // ����

        _Sword.GetComponent<BoxCollider2D>().enabled = false; // �ݶ��̴� ��Ȱ��ȭ

    }

    void Run()
    {
        PlayerState = PlayerState.RUN; // ���¸� �޸��� ���·� ��ȯ

        // �޸��� �ִϸ��̼����� ��ȯ
        _PlayerAnimator.SetBool("RUN", true);
        _PlayerAnimator.SetBool("JUMP", false);
        _PlayerAnimator.SetBool("D_JUMP", false);

    }

    void Jump()
    {
        PlayerState = PlayerState.JUMP; // ���¸� ���� ���·� ��ȯ

        _PlayerRigidbody.velocity = Vector2.zero; // ���� �ӵ��� 0���� ����
        _PlayerRigidbody.AddForce(new Vector2(0, _Jump_Power), ForceMode2D.Impulse); // ���� ���

        // ���� �ִϸ��̼����� ��ȯ
        _PlayerAnimator.SetBool("RUN", false);
        _PlayerAnimator.SetBool("JUMP", true);
        _PlayerAnimator.SetBool("D_JUMP", false);

        // ���� ȿ�� Ȱ��ȭ
        _Jump_Effect.SetActive(true);

        // ���� ���� ���
        _PlayerAudio.clip = _PlayerClip[1];
        _PlayerAudio.Play();

    }

    void D_Jump()
    {
        PlayerState = PlayerState.D_JUMP; // ���¸� ���� ������ ��ȯ

        _PlayerRigidbody.velocity = Vector2.zero; // ���� �ӵ��� 0���� ����
        _PlayerRigidbody.AddForce(new Vector2(0, _Jump_Power), ForceMode2D.Impulse); // ���� ���

        // ���� ���� ����Ʈ Ȱ��ȭ
        _D_Jump_Effect.SetActive(true);

        // ���� ���� �ִϸ��̼����� ��ȯ
        _PlayerAnimator.SetBool("RUN", false);
        _PlayerAnimator.SetBool("JUMP", false);
        _PlayerAnimator.SetBool("D_JUMP", true);

        // ���� ���� ���
        _PlayerAudio.clip = _PlayerClip[2];
        _PlayerAudio.Play();
    }

    // ���� üũ ( ����ĳ��Ʈ Ȱ�� )
    void Landing_Check()
    {

        float _MaxDistance = 1.5f; // ������ �ִ� �Ÿ� ����

        // ���� �׽�Ʈ�� �׸���
        Debug.DrawRay(transform.position, Vector2.down * _MaxDistance, Color.blue, 0.1f);

        // �÷��̾ ��� ���¶��, �������� ����
        if (PlayerState == PlayerState.DEATH)
        {
            return;
        }

        // �÷��̾ ����/���� ���� ���¿��� �������� ���� ���� ���̸� �߻��մϴ�.

        if ((_PlayerRigidbody.velocity.y <= 0 &&
            (PlayerState == PlayerState.JUMP || PlayerState == PlayerState.D_JUMP)))
        {
            RaycastHit2D _Hit = Physics2D.Raycast(
            transform.position, Vector2.down, _MaxDistance, LayerMask.GetMask("Platform"));

            // �浹ü�� �ִٸ�, �浹ü�� üũ�մϴ�.
            if (_Hit.collider != null)
            {
                Debug.Log(_Hit.collider.tag);

                // �浹ü�� �÷����̶�� �޸��� ���·� ��ȯ�մϴ�.
                if (_Hit.collider.tag == "Platform")
                {
                    Run();
                }

            }
            else // �浹ü�� ���ٸ�, �����մϴ�.
            {

                return;
            }

        }


    }

    void Attack_Check()
    {
        float _MaxDistance = 4f; // ������ �ִ� �Ÿ� ����

        // ���� �׽�Ʈ�� �׸���
        Debug.DrawRay(transform.position, Vector2.right * _MaxDistance, Color.red, 0.1f);

        // �÷��̾ ��� ���¶��, �������� ����
        if (PlayerState == PlayerState.DEATH)
        {
            return;
        }

        // �÷��̾ ���� ���°� �ƴҶ��� ���̸� �߻��մϴ�. ���.
        if (true)
        {
            RaycastHit2D _Hit = Physics2D.Raycast(
           transform.position, Vector2.right, _MaxDistance, LayerMask.GetMask("Enemy"));

            // �浹ü�� �ִٸ�, ������ �� �� �ְ� Ȱ��ȭ �մϴ�.
            if (_Hit.collider != null)
            {
                _IsAttack = true;
            }
            else // �ƴ϶��, Ȱ��ȭ���� �ʰ� �����մϴ�.
            {
                _IsAttack = false;
            }
        }



    }

    // Ʈ����
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���� �������� ����� ��
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
        
        // �÷��̾ ��� ���¶�� �������� ����
        if (PlayerState == PlayerState.DEATH)
        {
            return;
        }

        // ���� ���� ���¶�� ������
        if (PlayerState == PlayerState.D_JUMP)
        {
            return;
        }

        // ���� ���¶��, ���� ���� ���·� ��ȯ
        if (PlayerState == PlayerState.JUMP)
        {
            D_Jump();
        }

        // �޸��� �ִ� ���¶��, ���� ���·� ��ȯ
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