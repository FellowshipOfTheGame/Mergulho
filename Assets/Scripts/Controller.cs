using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Vector3 playerPos;
    private bool hasStarted = false;
    
    private void Start() {
        playerPos = new Vector3(0f, 0f, 0f);
    }

    private void Update(){
        if (Input.GetMouseButtonDown(0)) {
            hasStarted = true;
            playerPos = new Vector3(0f, 0f, 0f);
        }
        if (hasStarted) {
            float XmousePosInBlocks = (Input.mousePosition.x - (Screen.width / 2)) / (Screen.width / 2) * 16;
            float YmousePosInBlocks = (Input.mousePosition.y - (Screen.height / 2)) / (Screen.height / 2) * 9;
            playerPos.x = XmousePosInBlocks;
            playerPos.y = YmousePosInBlocks;
            this.transform.position = playerPos;
        }
    }
}