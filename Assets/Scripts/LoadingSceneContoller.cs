using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneContoller : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    Image loadingBar;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        // Scene�� �񵿱�� �ҷ����� �� Scene�� �ε��� ������ �ڵ����� �ҷ��� Scene���� �ҷ����� ������
        // false�� �����ϸ� Scene�� 90% �ε��� ���·� �������·� �Ѿ�� �ʰ� ����ϰ� �ȴ�
        // true�� �����ϰ� �Ǹ� �׶����� �ð��� �ε��ϰ� �Ѿ�� �ȴ�.
        op.allowSceneActivation = false;

        float timer = 0f;
        // �ε��� ������ �ʴ´ٸ� �ݺ��ϰ� �Ѵ�.
        while (!op.isDone)
        {
            // �ݺ����� �ѹ� �ݺ��� �� ���� ����Ƽ���� ������� �ش�.
            yield return null;

            if (op.progress < 0.9f)
            {
                loadingBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledTime;
                loadingBar.fillAmount = Mathf.Lerp(0.1f, 1f, timer / 10000);

                if (loadingBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }

            //timer += Time.unscaledTime;
            //loadingBar.fillAmount = timer / 1000f;

            //if (timer > 1000)
            //{
            //    op.allowSceneActivation = true;
            //}

            //yield return null;
        }
    }
}
