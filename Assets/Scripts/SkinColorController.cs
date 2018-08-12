using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinColorController : MonoBehaviour {

    public SpriteRenderer skin; 
    private Color32 color;
    //private int[] aux = new int[3];

	void Start () {
        color.r = (byte) PlayerPrefs.GetInt("colorR");
        color.g = (byte) PlayerPrefs.GetInt("colorG");
        color.b = (byte) PlayerPrefs.GetInt("colorB");
        color.a = 255;
        skin.color = color;
    }
}
