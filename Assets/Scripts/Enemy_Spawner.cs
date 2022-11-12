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

                // �߰� ���� ����ġ�� ���ϴ� ���� ����
                float _Bouns = Random.Range(3.0f, 6.0f);


                // ����ġ�� ������ Ʈ������ ���� ���� ( �÷��� �� �տ��� ������ )
                Vector3 _trans = new Vector3(
                    this.transform.position.x + 2f + _Bouns, this.transform.position.y + 2f, this.transform.position.z);


                // ���� �����ϴٸ� ���� ����
                Instantiate(_Enemy, _trans, this.transform.rotation);

                // ����
                yield return new WaitForSeconds(1.5f);

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
        // �÷����� �����ϸ� �÷��� ���� ������ �˸�
        if(collision.tag == "Platform")
        {
            _IsPlatform = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // �÷����� ����� �÷��� ���� ������ �˸�
        if(collision.tag == "Platform")
        {
            _IsPlatform = false;
        }
    }
}
