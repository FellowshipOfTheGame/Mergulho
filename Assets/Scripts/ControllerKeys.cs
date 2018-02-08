using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerKeys : MonoBehaviour {

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private float yVelocity;
    private float xVelocity;
    public float speed;



    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            yVelocity = 1;
        } else if (Input.GetKeyUp(KeyCode.UpArrow)) {
            yVelocity = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            yVelocity = -1;
        } else if (Input.GetKeyUp(KeyCode.DownArrow)) {
            yVelocity = 0;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            sr.flipX = false;
            xVelocity = 1;
        } else if (Input.GetKeyUp(KeyCode.RightArrow)) {
            xVelocity = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            sr.flipX = true;
            xVelocity = -1;
        } else if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            xVelocity = 0;
        }

        rb.velocity = new Vector2(xVelocity, yVelocity) * speed;
    }
}
