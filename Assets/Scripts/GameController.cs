using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject pause;

    // Use this for initialization
    void Start()
    {
        pause.SetActive(false);
    }

    public void ShowPauseMenu(bool show)
    {
        pause.SetActive(show);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
