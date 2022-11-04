using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    M_Player_Controller _Player_Controller;
    TextMeshProUGUI distanceText;
    TextMeshProUGUI finalDistanceText;
    GameObject result;
    GameObject Player;

    private void Awake()
    {
        _Player_Controller = GameObject.Find("Player").GetComponent<M_Player_Controller>();
        distanceText = GameObject.Find("DistanceText").GetComponent<TextMeshProUGUI>();
        finalDistanceText = GameObject.Find("FinalDistanceText").GetComponent<TextMeshProUGUI>();

        result = GameObject.Find("Result");
        result.SetActive(false);
    }

    private void Update()
    {
        int _Distance = Mathf.FloorToInt(_Player_Controller._Distance);
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        if (Player.GetComponent<M_Player_Controller>().PlayerState == PlayerState.DEATH)
        {
            result.SetActive(true);
            finalDistanceText.text = _Distance + "m";
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Ingame");
    }
}
