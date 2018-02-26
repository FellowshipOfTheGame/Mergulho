using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    public GameObject player;
    public float leftLimit;
    public float rightLimit;
    private Vector3 posicao;

    void Start() {

    }

    void LateUpdate(){
        if (player.transform.position.x > leftLimit) {
            if (player.transform.position.x < rightLimit) {
                posicao.x = player.transform.position.x;
            } else {
                posicao.x = rightLimit;
            }
        } else {
            posicao.x = leftLimit;
        }
        transform.position = posicao;
    }
}
