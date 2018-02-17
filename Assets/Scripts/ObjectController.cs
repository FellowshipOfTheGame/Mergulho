using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectController : MonoBehaviour
{
    public int index;
    public float earnOxygenTime;

    private float timeRemaining;

    private void OnMouseDown()
    {
        if (gameObject.tag == "Chest")
        {
            //Checar se nao precisa de chave para ser aberto
            PlayerPrefs.SetInt("currentQuestion", index);
            SceneManager.LoadScene("Quiz");
        }
        else if (gameObject.tag == "Bubble")
        {
            timeRemaining = PlayerPrefs.GetFloat("timeRemaining") + earnOxygenTime;
            PlayerPrefs.SetFloat("timeRemaining", timeRemaining);
        }
        else if (gameObject.tag == "Key")
        {
            //Captura a chave para ser usada em um único baú indicado pelo index
        }
    }
}
