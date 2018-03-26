using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    public GameObject menuDisplay, creditsDisplay, instrucDisplay, quitButton;
    public bool isStartMenu;

    private void Start()
    {
        if (isStartMenu)
        {
            //WebGL nao funciona o botao de sair, entao nao é exibido
            if (Application.dataPath.Contains("://") || Application.dataPath.Contains(":///"))
                quitButton.SetActive(false);

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
