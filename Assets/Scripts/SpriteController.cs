using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour {

    public Sprite[] bau;

    //Troca  as sprites
    void ChangeSprite() {
        this.GetComponent<SpriteRenderer>().sprite = bau[1];
    }
}
