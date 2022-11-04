using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Action : MonoBehaviour
{
    private GameObject _Player;
    private Animator _Animator;

    private SpriteRenderer _SpriteRenderer;
    private Color _Color;

    public Vector3 _FixedPosition; // ����� ��ġ ����

    void Awake()
    {
        _Animator = GetComponent<Animator>();
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _Color = new Color(1, 1, 1, 1);
    }

    void Start()
    {

        _Player = GameObject.FindGameObjectWithTag("Player");
        
    }

    void OnEnable()
    {
        // �ִϸ��̼� Ȱ��ȭ
        _Animator.SetTrigger("Action");

      
    }

     void Update()
    {
        Move();

        _Color -= new Color(0, 0, 0, Time.deltaTime * 5.0f);
      _SpriteRenderer.color = _Color;

        if(_SpriteRenderer.color.a <= 0.01f)
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        _Color = new Color(1, 1, 1, 1);
    }

    void Move()
    {
        transform.position = _Player.transform.position + _FixedPosition;

    }
}
