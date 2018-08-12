using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CustomizationController : MonoBehaviour {

    public GameObject[] itens;
    public GameObject warning, player;

    public Color[] colors;
    public SpriteRenderer[] skin;
    private bool colorSet = false;
    private Color32 aux;

    public void GoToGame () {
        for (int i = 0; i < itens.Length; i++) {
            if (itens[i].activeSelf == false) {
                warning.SetActive(true);
                return;
            }
        }
        if (!colorSet) {
            warning.SetActive(true);
            return;
        }
        aux = skin[0].color;
        PlayerPrefs.SetInt("colorR", aux.r);
        PlayerPrefs.SetInt("colorG", aux.g);
        PlayerPrefs.SetInt("colorB", aux.b);

        SceneManager.LoadScene("Game Loader");
    }

    public void SetSkinColor (int colorIndex){
        for (int i = 0; i < skin.Length; i++) {
            skin[i].color = colors[colorIndex];
        }
        colorSet = true;
    }

}
