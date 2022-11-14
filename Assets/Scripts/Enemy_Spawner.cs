using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public GameObject _Enemy;

    // ���� ���������� ����
    public bool _IsSpawn = false;

    // �÷��� ���� �ִ� ���� ����
    public bool _IsPlatform = false;

    // ���� �ð� �ּڰ�
    [SerializeField]
    private float Spawn_Min = 1.5f;

    // ���� �ð� �ִ�
    [SerializeField]
    private float Spawn_Max = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        _Enemy = Resources.Load<GameObject>("Prefabs/Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        // ���� �Ұ����ϴٸ� ����
        if (_IsSpawn)
        {
            yield return null;
        }
        else
        {
            // �÷��� ���� �ִٸ� ó����
            if (_IsPlatform)
            {
                // �� ����
                _IsSpawn = true;

                // ����ġ�� ������ Ʈ������ ���� ���� ( �÷��� �� �տ��� ������ )
                Vector3 _trans = new Vector3(16,2,0);


                // ���� �����ϴٸ� ���� ����
                Instantiate(_Enemy, _trans, this.transform.rotation);

                // ����
                yield return new WaitForSeconds(Random.Range(Spawn_Min, Spawn_Max));

                // �ٽ� ���� ���� �����ϵ��� ������
                _IsSpawn = false;
            }
            else
            {
                yield return null;
            }

        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �̹� Ȱ��ȭ��� ����
        if (_IsPlatform)
        {
            return;
        }
        // �÷����� �����ϸ� �÷��� ���� ������ �˸�
        if(collision.tag == "Platform")
        {
            _IsPlatform = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // �̹� ��Ȱ��ȭ��� ����
        if (!_IsPlatform)
        {
            return;
        }
        // �÷����� ����� �÷��� ���� ������ �˸�
        if(collision.tag == "Platform")
        {
            _IsPlatform = false;
        }
    }
}
