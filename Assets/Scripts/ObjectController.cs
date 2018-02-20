using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectController : MonoBehaviour
{
    public int index;
    public float earnOxygenTime;
    public bool needKey = false;

    private void OnMouseDown()
    {
        if (gameObject.tag == "Chest")
        {
            if (needKey)
            {
                //Checar se possui chave correspondente a esse baú
            }
            else
            {
                PlayerPrefs.SetInt("currentQuestion", index);
                SceneManager.LoadScene("Quiz");
            }
        }
        else if (gameObject.tag == "Bubble")
        {
            PlayerPrefs.SetFloat("timeRemaining", PlayerPrefs.GetFloat("timeRemaining") + earnOxygenTime);
            gameObject.SetActive(false);
            //Nao resolve, pois se mudar a cena e depois voltar, a bolha estara de volta tambem
        }
        else if (gameObject.tag == "Key")
        {
            //Captura a chave para ser usada em um único baú indicado pelo index
        }
    }
}
