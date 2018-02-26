using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour {

    private float timer;
    public GameObject fish;
    public GameObject shark;
    public int sharkProbability;
    public float timeToSpawn;
    private int result;

	void Start () {
        timer = 0;
	}
	
	void Update () {
        timer += Time.deltaTime;
        if (timer > timeToSpawn) {
            timer = 0f;
            result = SharkOrFish();
            if (result == 1) {
                Instantiate(shark, new Vector3(-60f, Random.Range(1, 7.5f), 0f), new Quaternion(0f, 180f, 0f, 0f));
            } else {
                Instantiate(fish, new Vector3(-55f, Random.Range(-5.5f, 10f), 0f), new Quaternion(0f, 180f, 0f, 0f));
            }
        }
	}

    private int SharkOrFish() {
        int chance;
        chance = (int) Random.Range(0, sharkProbability);
        if (chance == 1) {
            return 1;
        } else {
            return 0;
        }
    }
}
