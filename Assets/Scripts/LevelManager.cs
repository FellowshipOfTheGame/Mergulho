using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public GameObject pause;

	// Use this for initialization
	void Start () {
        pause.SetActive(false);
    }

    public void LoadScreenPause(bool on) {
        pause.SetActive(on);
    }

    public void LoadScene(string name) {
        SceneManager.LoadScene(name);
    }
}
