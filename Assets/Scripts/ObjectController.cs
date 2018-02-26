using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectController : MonoBehaviour {
    public int index;
    public float earnOxygenTime;
    public bool needKey = false;
    public bool open = false;
    public bool bubbleActive = true;
    public Sprite[] sprites;

    private void Update() {
        if (bubbleActive == false){
            Destroy(gameObject);
        }

        if (gameObject.tag == "Chest") {
            if (open == true) {
                ChangeSprite(1);
            } else {
                ChangeSprite(0);
            }
        }
    }

    private void OnMouseDown() {
        if (gameObject.tag == "Chest") {
            if (!open) {
                if (needKey){
                    //Checar se possui chave correspondente a esse baú
                } else {
                    open = true;
                    PlayerPrefs.SetInt("currentQuestion", index);
                    SceneManager.LoadScene("Quiz");
                }
            }
        }
        else if (gameObject.tag == "Bubble"){
            PlayerPrefs.SetFloat("timeRemaining", PlayerPrefs.GetFloat("timeRemaining") + earnOxygenTime);
            bubbleActive = false;
            Destroy(gameObject);
            //Nao resolve, pois se mudar a cena e depois voltar, a bolha estara de volta tambem
        }
        else if (gameObject.tag == "Key") {
            //Captura a chave para ser usada em um único baú indicado pelo index
        }
    }

    //Troca  as sprites
    void ChangeSprite(int index) {
        this.GetComponent<SpriteRenderer>().sprite = sprites[index];
    }
}

