using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float dano;
    private float timeRemaining;
    public GameObject panel;

    void Start ()
    {
        panel.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Tubarao")
        {
            timeRemaining = PlayerPrefs.GetFloat("timeRemaining");
            timeRemaining -= dano;
            panel.SetActive(true);
            PlayerPrefs.SetFloat("timeRemaining", timeRemaining);
        }
    }

    void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Tubarao")  {
            panel.SetActive(false);
        }
    }
}
