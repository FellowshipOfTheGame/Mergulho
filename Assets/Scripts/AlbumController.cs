using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlbumController : MonoBehaviour
{
    public AudioSource bookFlip;

	private void Start () {
		
	}

	private void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Game");
    }

    public void LoadImages() {
        bookFlip.Play();
    }
}
