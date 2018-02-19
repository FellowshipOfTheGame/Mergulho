using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerKeys : MonoBehaviour {

    public Animator anim;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private float yVelocity;
    private float xVelocity;
    public float speed;
    private bool movement;



    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        movement = false;
        anim.SetBool("HorizontalMovement", movement);

    }

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
            movement = true;
            sr.flipX = false;
            xVelocity = 1;
        } else if (Input.GetKeyUp(KeyCode.RightArrow)) {
            movement = false;
            xVelocity = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            movement = true;
            sr.flipX = true;
            xVelocity = -1;
        } else if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            movement = false;
            xVelocity = 0;
        }
        anim.SetBool("HorizontalMovement", movement);
        rb.velocity = new Vector2(xVelocity, yVelocity) * speed;
    }
}
