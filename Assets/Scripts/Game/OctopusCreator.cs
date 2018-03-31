using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusCreator : MonoBehaviour {

    private float timer;
    public GameObject octopusRight;
    public GameObject octopusLeft;
    public float timeToSpawn;
    public float yPosition;
    public float screenStart;
    public float screenEnd;
    private int direction;

    void Start() {
        timer = 0;
    }

    void Update() {
        timer += Time.deltaTime;
        if (timer > timeToSpawn) {
            timer = 0f;
            direction = (int)Random.Range(0, 100);
            direction = direction % 2;
            if (direction == 1) {
                Instantiate(octopusRight, new Vector3(Random.Range(screenStart, screenEnd), yPosition, 0f), new Quaternion(0f, 0f, 0f, 0f));
            } else {
                Instantiate(octopusLeft, new Vector3(Random.Range(screenStart, screenEnd), yPosition, 0f), new Quaternion(0f, 180f, 0f, 0f));
            }
        }
    }
}
