using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControllerTitle : MonoBehaviour
{
    
    public void Play()
    {
        SceneManager.LoadScene("ChrSelect");
    }

    public void Select()
    {
        LoadingSceneContoller.LoadScene("Ingame");
    }
}
