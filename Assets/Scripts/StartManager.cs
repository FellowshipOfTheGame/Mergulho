using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {

    public GameObject menu;
    public GameObject credits;

    // Use this for initialization
    private void Start() {
        menu.SetActive(true);
        credits.SetActive(false);
    }

    public void LoadScreenMenu(bool on) {
        menu.SetActive(on);
    }
    
    public void LoadScreenCredits(bool on) {
        credits.SetActive(on);
    }

    public void LoadScene(string name) {
        SceneManager.LoadScene(name);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
