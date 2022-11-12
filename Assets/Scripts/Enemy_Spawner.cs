using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public GameObject _Enemy;

    // 스폰 가능한지의 여부
    public bool _IsSpawn = false;

    // 플랫폼 위에 있는 지의 여부
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
        // 스폰 불가능하다면 종료
        if (_IsSpawn)
        {
            yield return null;
        }
        else
        {
            // 플랫폼 위에 있다면 처리함
            if (_IsPlatform)
            {
                // 적 스폰
                _IsSpawn = true;

                // 추가 랜덤 보정치를 더하는 변수 생성
                float _Bouns = Random.Range(3.0f, 6.0f);


                // 보정치를 적용한 트랜스폼 변수 생성 ( 플랫폼 맨 앞에서 생성됨 )
                Vector3 _trans = new Vector3(
                    this.transform.position.x + 2f + _Bouns, this.transform.position.y + 2f, this.transform.position.z);


                // 스폰 가능하다면 적을 생성
                Instantiate(_Enemy, _trans, this.transform.rotation);

                // 지연
                yield return new WaitForSeconds(1.5f);

                // 다시 적을 스폰 가능하도록 설정함
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
        // 플랫폼을 감지하면 플랫폼 위에 있음을 알림
        if(collision.tag == "Platform")
        {
            _IsPlatform = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 플랫폼을 벗어나면 플랫폼 위에 없을음 알림
        if(collision.tag == "Platform")
        {
            _IsPlatform = false;
        }
    }
}
