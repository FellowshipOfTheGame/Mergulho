using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    public GameObject menuDisplay, creditsDisplay, instrucDisplay, quitButton;
    public bool isStartMenu;
    public GameObject[] instructions;

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

            for(int i = 1; i < instructions.Length; i++)
            {
                instructions[i].SetActive(false);
            }

            instructions[0].SetActive(true);
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

    public void EnablePageInstruction(string page)
    {
        instructions[Int32.Parse(page)].SetActive(true);
    }

    public void DisablePageInstruction(string page)
    {
        instructions[Int32.Parse(page)].SetActive(false);
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
