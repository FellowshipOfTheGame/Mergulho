using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    public GameObject menuDisplay;
    public GameObject creditsDisplay;
    public GameObject instrucDisplay;
    public bool isStartMenu;

    private void Start()
    {
        if (isStartMenu)
        {
            menuDisplay.SetActive(true);
            creditsDisplay.SetActive(false);
            instrucDisplay.SetActive(false);
        }
    }

    public void ShowMenuDisplay(bool show)
    {
        menuDisplay.SetActive(show);
    }

    public void ShowCreditsDisplay(bool show)
    {
        creditsDisplay.SetActive(show);
    }

    public void ShowInstructionsDisplay(bool show)
    {
        instrucDisplay.SetActive(show);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
