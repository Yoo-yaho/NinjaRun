using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour
{
    public AudioClip deathClip; // ����� ����� ����� Ŭ��
    public Vector2 velocity;
    public Vector2 playerPosition;

    public int jumpCount = 0; // ���� ���� Ƚ��
    public float jumpForce = 1500f; // ���� ��
    private bool isGrounded = false; // �ٴڿ� ��Ҵ��� ��Ÿ��
    public float distance = 0.0f;
    public bool isDead = false; // ��� ����
    public bool isDoublJumped = false;
    public float runSpeed;
    public float runMaxSpeed = 1f;
    public float jumpSpeed;
    public float jumpMaxSpeed = 1f;
    public float acceleration = 0.5f;
    public float skillMana;
    public float skillRegen;
    public float cloaking = 900f;

    private Rigidbody2D playerRigidbody; // ����� ������ٵ� ������Ʈ
    private Animator animator; // ����� �ִϸ����� ������Ʈ
    private AudioSource playerAudio; // ����� ����� �ҽ� ������Ʈ
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
        // ���� ������Ʈ�κ��� ����� ������Ʈ���� ������ ������ �Ҵ�
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
            // ����� ó���� �� �̻� �������� �ʰ� ����
            return;
        }

        // ���콺 ���� ��ư�� �������� && �ִ� ���� Ƚ��(2)�� �������� �ʾҴٸ�
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            // ���� Ƚ�� ����
            jumpCount++;
            // ���� ������ �ӵ��� ���������� ����(0, 0)�� ����
            playerRigidbody.velocity = Vector2.zero;
            // ������ٵ� �������� ���� �ֱ�
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            // ����� �ҽ� ���
            playerAudio.Play();

            if (jumpCount == 2)
            {
                isDoublJumped = true;
                animator.SetTrigger("isDoubleJump");
            }
        }   

        if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            // ���콺 ���� ��ư���� ���� ���� ���� && �ӵ��� y ���� ������ (���� ��� ��)
            // ���� �ӵ��� �������� ����
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        // �ִϸ������� Grounded �Ķ���͸� isGrounded ������ ����
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
            // �ӵ��� ����(0, 0)�� ����
            playerRigidbody.velocity = Vector2.zero;
            //---------------------------------------------- ��������, �Ǵ� �������鼭 ��� �� �ұ�Ģ�� ���̷� �̵��ϴ� ���� ����

            playerRigidbody.AddForce(new Vector2(0, cloaking));

            skillBar.fillAmount -= skillMana;
        }

        // ���콺 ��Ŭ�� / ���� ��� ���
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("����");
            StartCoroutine("Attacking");
            animator.SetTrigger("isAttack");
        }

        skillBar.fillAmount += skillRegen * Time.deltaTime;
    }

    IEnumerator Attacking() // ���� ��ǵ��� ���� �ݶ��̴��� Ȱ��ȭ
    {
        if (Attack_Point.activeSelf == true) // ���� ���̶�� ����
        {
            yield return null;
        }

        Attack_Point.SetActive(true); // ���� �ݶ��̴� Ȱ��ȭ
        yield return new WaitForSeconds(0.4f); // 0.�� ��
        Attack_Point.SetActive(false); // ���� �ݶ��̴� ��Ȱ��ȭ
    }

    private void FixedUpdate()
    {
        distance += runSpeed;
    }

    public void Die()
    {
        // �ִϸ������� Die Ʈ���� �Ķ���͸� ��
        //animator.SetTrigger("Die");

        // ����� �ҽ��� �Ҵ�� ����� Ŭ���� deathClip���� ����
        //playerAudio.clip = deathClip;
        // ��� ȿ���� ���
        //playerAudio.Play();

        // �ӵ��� ����(0, 0)�� ����
        playerRigidbody.velocity = Vector2.zero;
        // ��� ���¸� true�� ����
        isDead = true;
        runSpeed = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DeadZone" && !isDead)
        {
            // �浹�� ������ �±װ� Dead�̸� ���� ������� �ʾҴٸ� Die() ����
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
        // � �ݶ��̴��� �������, �浹 ǥ���� ������ ���� ������
        if (collision.contacts[0].normal.y > 0.7f)
        {
            // isGrounded�� true�� �����ϰ�, ���� ���� Ƚ���� 0���� ����
            isGrounded = true;
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // � �ݶ��̴����� ������ ��� isGrounded�� false�� ����
        if(collision.collider.tag == "Platform")
        {
            isGrounded = false;
        }

    }
}
