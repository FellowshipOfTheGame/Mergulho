using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public GameObject menu;
    public GameObject credits;

    public void LoadScreenMenu() {
        menu.SetActive(true);
        credits.SetActive(false);
    }
    
    public void LoadScreenCredits() {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void LoadScene(string name) {
        SceneManager.LoadScene(name);
    }
}
