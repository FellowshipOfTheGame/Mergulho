using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour {

    public Rigidbody2D rb;
    public int xVelocity;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        rb.velocity = new Vector2(xVelocity, 0);
    }


    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.gameObject.tag == "Destroyers") {
            Destroy(gameObject);
        }
    }
}
