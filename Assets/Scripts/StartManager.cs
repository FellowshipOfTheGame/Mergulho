using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {

    public GameObject menu;
    public GameObject credits;
    public GameObject instructions;

    // Use this for initialization
    private void Start()
    {
        menu.SetActive(true);
        credits.SetActive(false);
        instructions.SetActive(false);
    }

    public void ShowMenuScreen(bool show)
    {
        menu.SetActive(show);
    }

    public void ShowCreditsScreen(bool show)
    {
        credits.SetActive(show);
    }

    public void ShowInstructionsScreen(bool show)
    {
        instructions.SetActive(show);
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
