using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioSource audioSource;
    public float startingPitch = 1f;
    public float finalPitch = 1.2f;
    public float whenChange = 0.2f;

    private float timeRemaining;
    private float playTime;
    private float change;
    private Scene scene;

    void Start() {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        timeRemaining = PlayerPrefs.GetFloat("timeRemaining");
        playTime = PlayerPrefs.GetFloat("playTimeAvaliable");
        change = whenChange * playTime;
        audioSource.pitch = startingPitch;
    }

    void Update() {
        timeRemaining = PlayerPrefs.GetFloat("timeRemaining");
        if (timeRemaining <= change) {
            audioSource.pitch = finalPitch;
        } else {
            audioSource.pitch = startingPitch;

        }
        scene = SceneManager.GetActiveScene();
        if ((scene.name == "Win") || (scene.name == "Lose")) {
            audioSource.Stop();
        }
    }
}
