using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    public GameObject menuDisplay, creditsDisplay, instrucDisplay, quitButton;
    public GameObject instruction0, instruction1, instruction2, instruction3;
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
            instruction0.SetActive(false);
            instruction1.SetActive(false);
            instruction2.SetActive(false);
            instruction3.SetActive(false);
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

    public void ShowInstructions0(bool show)
    {
        instruction0.SetActive(show);
    }

    public void ShowInstructions1(bool show)
    {
        instruction1.SetActive(show);
    }

    public void ShowInstructions2(bool show)
    {
        instruction2.SetActive(show);
    }

    public void ShowInstructions3(bool show)
    {
        instruction3.SetActive(show);
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

        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
