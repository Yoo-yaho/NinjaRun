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
        // Scene을 비동기로 불러들일 때 Scene의 로딩이 끝나면 자동으로 불러올 Scene으로 불러올지 설정함
        // false로 설정하면 Scene을 90% 로드한 상태로 다음상태로 넘어가지 않고 대기하게 된다
        // true로 변경하게 되면 그때남은 시간을 로딩하고 넘어가게 된다.
        op.allowSceneActivation = false;

        float timer = 0f;
        // 로딩이 끝나지 않는다면 반복하게 한다.
        while (!op.isDone)
        {
            // 반복문이 한번 반복될 때 마다 유니티에게 제어권을 준다.
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
