using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject settingsPanel;
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }
    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}