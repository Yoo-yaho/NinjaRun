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



    // �� �÷��� ���� ������ ��ġ

    // �÷����� �����ϴ� �ݶ��̴� ������Ʈ
    private GameObject _Platform;

    // ��ũ�� �ӵ��� �����ϴ� ScrollingObject ��ũ��Ʈ
    private ScrollingObject _ScrollingObject;

    // ����� ���� ���ο� ������Ʈ !

    // ���� ���� �޴� Ƚ�� ( Life ) �� ����
    private int Life;

    // �� ���ھ�� ���� �ʱ� �� ����
    private int _ScoreLife;

    // ���� ������ �޾��� ���, ���� �ð����� ���� ����
    private bool Is_Attacked = false;

    // �� ��ũ�Ѹ� ( Transform , Rigidbody �̵� ��ġ�� ���� )
    public float _Speed = 2f;

    // ���ھ� ȣ���� ���� ĵ������ũ��Ʈ
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
        // ����� 1 ~ 3 ���̷� ������
        Life = Random.Range(1, 4);

        _ScoreLife = Life;

        // ����� ����� �� ���� ( ��ȣ�� ? )
        // 1 : �⺻ - 2 : ��� - 3 : �Ķ�

        Set_Image();

    }


    private void FixedUpdate()
    {
        if (_Platform.GetComponent<Enemy_PlatformCheck>()._IsCheck)
        {  
            // �ӵ��� �۴ٸ� ( ���� �ӵ� 10f ��� )
            if (_ScrollingObject.speed < 11)
            {
                // �⺻ �̵��ӵ��� ��ȯ��
                _ScrollingObject.speed = 12f;
            }


        }
        else
        {

            // �ӵ��� ũ�ٸ� ( �⺻ �ӵ� 12f ��� )
            if (_ScrollingObject.speed > 11)
            {
                // �÷����� �̵� �ӵ��� ���Ͻ���
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

    // ���� ������ ��, ������ �������� ���� ��������, �ݶ��̴��� ������ �߶���Ų��.
    // ���� �������� �˸��� ���� ���͸��� ������ R ���� �������� �����̴� ȿ���� �ش�.
    void Sword_Hit()
    {
        // ���� ó��
        if (Is_Attacked)
        {
            return;
        }

        // ���� ������ ��
        float _ForcePower = 800f;

        // ���� ������ ���� ( �밢�� )
        Vector2 _Dir = (Vector2.right);


        // ü���� ����
        Life -= 1;

        // ���� ���� ( ���ϱ� ���� �ʱ�ȭ )
        rigidbody.AddForce((_Dir * _ForcePower), ForceMode2D.Force);

        if (Life <= 0)
        {
            collider.enabled = false; // �ݶ��̴� ��Ȱ��ȭ

            Destroy(this.gameObject, 3.0f); //3�� �ڿ� ����
        }
        else
        {
            StartCoroutine("Attack_Check");
        }


        // �̹����� ��ȯ��
        Set_Image();



        /*
          
        if (Life <= 0)
        {
            collider.enabled = false; // �ݶ��̴� ��Ȱ��ȭ

            Destroy(this.gameObject, 3.0f); //3�� �ڿ� ����
        }

        */

    }

    IEnumerator Attack_Check()
    {
        Is_Attacked = true;
        yield return new WaitForSeconds(0.05f);
        Is_Attacked = false;

    }

    // ���� �������� �˸��� ���� ���͸��� ������ R ���� �������� �����̴� ȿ���� �ش�.
    void Set_Image()
    {
        // ���� ���� ���� ������ ������
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
