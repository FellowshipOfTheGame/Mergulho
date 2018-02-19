using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float leftLimit;
    public float rightLimit;
    public float upLimit;
    public float downLimit;
    private Vector3 posicao;

    private Vector3 offset;  
    void Start() {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate() {
        if (player.transform.position.x > leftLimit) {
            if (player.transform.position.x < rightLimit) {
                posicao.x = player.transform.position.x + offset.x;
            }
        }
        if (player.transform.position.y > downLimit) {
            if (player.transform.position.y < upLimit){
                posicao.y = player.transform.position.y + offset.y;
            }
        }
        posicao.z = player.transform.position.z + offset.z;
        transform.position = posicao;
    }
}