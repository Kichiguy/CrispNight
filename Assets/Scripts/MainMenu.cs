using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button quitButon;
    public Button creditsButton;
    public GameObject menu;
    public GameObject CreditsMenu;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        CreditsMenu.SetActive(true);
        menu.SetActive(false);
    }
}
