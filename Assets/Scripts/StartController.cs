using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    public GameObject menuDisplay, creditsDisplay, instrucDisplay, control, objective1, objective2, quitButton;
    public bool isStartMenu;

    private GameObject[] instructions;

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
            control.SetActive(false);
            objective1.SetActive(false);
            objective2.SetActive(false);

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

    public void ShowObjective1(bool show)
    {
        objective1.SetActive(show);
    }

    public void ShowObjective2(bool show)
    {
        objective2.SetActive(show);
    }

    public void ShowControllers(bool show)
    {
        control.SetActive(show);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadSceneAndDestroy(string name)
    {
        GameObject[] reload = GameObject.FindGameObjectsWithTag("Reload");
        foreach (GameObject go in reload)
            Destroy(go);

        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
