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
    public GameObject result;
    public GameObject clear;
    GameObject Player;
    GameManager gameManager;

    private void Awake()
    {
        _Player_Controller = GameObject.Find("Player").GetComponent<M_Player_Controller>();
        distanceText = GameObject.Find("DistanceText").GetComponent<TextMeshProUGUI>();
        finalDistanceText = GameObject.Find("FinalDistanceText").GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.Find("New_GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        result.SetActive(false);
        clear.SetActive(false);
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

        if (gameManager.isClear == true)
            clear.SetActive(true);
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
