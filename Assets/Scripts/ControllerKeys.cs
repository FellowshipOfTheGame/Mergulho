using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerKeys : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public float speed;

    private float yVelocity;
    private float xVelocity;
    private bool movement;

    void Start() {
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        movement = false;
        anim.SetBool("HorizontalMovement", movement);

        LoadPlayerPosition();
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

        SavePlayerPosition();
    }

    private void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("playerX", transform.position.x);
        PlayerPrefs.SetFloat("playerY", transform.position.y);
        PlayerPrefs.SetFloat("playerZ", transform.position.z);
    }

    private void LoadPlayerPosition()
    {
        transform.position = new Vector3(PlayerPrefs.GetFloat("playerX"), PlayerPrefs.GetFloat("playerY"), PlayerPrefs.GetFloat("playerZ"));
    }
}
