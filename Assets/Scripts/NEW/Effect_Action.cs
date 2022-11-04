using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Action : MonoBehaviour
{
    private GameObject _Player;
    private Animator _Animator;

    private SpriteRenderer _SpriteRenderer;
    private Color _Color;

    public Vector3 _FixedPosition; // 상대적 위치 지정

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
        // 애니메이션 활성화
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
