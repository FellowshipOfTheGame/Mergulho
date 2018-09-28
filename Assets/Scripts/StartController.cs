using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    public GameObject quitButton;
    public bool isStartMenu;

    private void Start()
    {
        if (isStartMenu)
        {
            //WebGL nao funciona o botao de sair, entao nao é exibido
            if (Application.dataPath.Contains("://") || Application.dataPath.Contains(":///"))
                quitButton.SetActive(false);
        }
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
