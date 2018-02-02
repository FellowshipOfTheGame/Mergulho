using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumManager : MonoBehaviour {

    public AudioSource bookFlip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadImages(Button button) {
        //if(tem mais paginas)

        bookFlip.Play();
        //else nao carrega mais paginas
    }

}
