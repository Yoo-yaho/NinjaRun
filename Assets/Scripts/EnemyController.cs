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
    private void Awake()
    {
        //animator = gameObject.GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(movementFlag, rigidbody.velocity.y);
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
    }

    // ���� ������ ��, ������ �������� ���� ��������, �ݶ��̴��� ������ �߶���Ų��.
    // ���� �������� �˸��� ���� ���͸��� ������ R ���� �������� �����̴� ȿ���� �ش�.
    void Sword_Hit()
    {
        float _ForcePower = 300f;
        Vector2 _Dir = (Vector2.up + new Vector2(Random.Range(-1.00f, 1.00f), 0));

        rigidbody.AddForce(_Dir * _ForcePower, ForceMode2D.Force);
        Debug.Log(_Dir);

        Sword_Hit_Image();
        collider.enabled = false; // �ݶ��̴� ��Ȱ��ȭ

        Destroy(this.gameObject, 3.0f); //3�� �ڿ� ����
    }

    // ���� �������� �˸��� ���� ���͸��� ������ R ���� �������� �����̴� ȿ���� �ش�.
    void Sword_Hit_Image()
    {
        Debug.Log("�� ����");
        spriterenderer.color = Color.red;
    }
}
