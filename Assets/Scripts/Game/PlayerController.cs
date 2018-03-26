using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float damage;
    public GameObject panel;
    public AudioClip bubbles;

    private AudioSource[] audioSources;
    private float timeRemaining;

    void Start ()
    {
        audioSources = Camera.main.GetComponents<AudioSource>();
        panel.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Tubarao")
        {
            timeRemaining = PlayerPrefs.GetFloat("timeRemaining");
            timeRemaining -= damage;
            panel.SetActive(true);
            PlayerPrefs.SetFloat("timeRemaining", timeRemaining);

            if (!audioSources[1].isPlaying)
                audioSources[1].PlayOneShot(bubbles);
        }
    }

    void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Tubarao")
            panel.SetActive(false);
    }
}
