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
    private GameController game;


    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        game = GameObject.FindObjectOfType<GameController>();

        movement = false;
        anim.SetBool("HorizontalMovement", movement);

        LoadPlayerPosition();
    }

    void Update() {
        if (game.isGameActive == true)
        {
            yVelocity = Input.GetAxis("Vertical");
            xVelocity = Input.GetAxis("Horizontal");

            if (xVelocity == 0)
                movement = false;
            else
                movement = true;

            if (xVelocity > 0 && xVelocity <= 1)
                sr.flipX = false;
            if (xVelocity < 0 && xVelocity >= - 1)
                sr.flipX = true;

            anim.SetBool("HorizontalMovement", movement);

            rb.velocity = new Vector2(xVelocity, yVelocity) * speed;

            SavePlayerPosition();
        }
    }

    private void SavePlayerPosition() {
        PlayerPrefs.SetFloat("playerX", transform.position.x);
        PlayerPrefs.SetFloat("playerY", transform.position.y);
        PlayerPrefs.SetFloat("playerZ", transform.position.z);
    }

    private void LoadPlayerPosition() {
        transform.position = new Vector3(PlayerPrefs.GetFloat("playerX"), PlayerPrefs.GetFloat("playerY"), PlayerPrefs.GetFloat("playerZ"));
    }
}
