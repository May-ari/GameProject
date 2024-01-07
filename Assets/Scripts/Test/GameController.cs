using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController player = null;
    [SerializeField] private GameObject GameOverScreen;

    void Awake()
    {
        GameOverScreen.SetActive(false);
    }

    void Update()
    {
        if (player==null)
        {
            GameOverScreen.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Game");
    }
}
