using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{

    public GameObject fish;
    public GameObject shark;
    public GameObject turtle;
    public int sharkProbability;
    public int turtleProbability;
    public float timeToSpawn;
    public float xPosition;
    public float rotation;
    public float screenStart;
    public float screenEnd;

    private int result;
    private GameController game;
    private float timer;


    void Start()
    {
        game = GameObject.FindObjectOfType<GameController>();
        timer = 0;
        for (float i = screenStart; i < screenEnd; i += 16.0f)
        {
            Instantiate(fish, new Vector3(i, Random.Range(-5.5f, 10f), 0f), new Quaternion(0f, rotation, 0f, 0f));
        }
    }

    void Update() {
        if (game.isGameActive == true) {
            timer += Time.deltaTime;
            if (timer > timeToSpawn) {
                timer = 0f;
                result = SharkFishOrTurtle();
                if (result == 1) {
                    Instantiate(shark, new Vector3(xPosition, Random.Range(1, 7.5f), 0f), new Quaternion(0f, rotation, 0f, 0f));
                } else if (result == 2) {
                    Instantiate(turtle, new Vector3(xPosition, Random.Range(-2.5f, 9.0f), 0f), new Quaternion(0f, rotation, 0f, 0f));
                } else {
                    Instantiate(fish, new Vector3(xPosition, Random.Range(-5.5f, 10f), 0f), new Quaternion(0f, rotation, 0f, 0f));
                }
            }
        }
    }

    private int SharkFishOrTurtle() {
        int chance;
        chance = (int)Random.Range(0, sharkProbability);
        if (chance == 1) {
            return 1;
        } else {
            chance = (int)Random.Range(0, turtleProbability);
            if (chance == 1) {
                return 2;
            } else {
                return 0;
            }
        }
    }
}
