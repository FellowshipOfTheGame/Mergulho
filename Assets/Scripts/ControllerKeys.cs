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
        if (game.isGameActive == true) {

            if (Input.GetKeyDown(KeyCode.W)) {
                yVelocity = 1;
            } else if (Input.GetKeyUp(KeyCode.W)) {
                yVelocity = 0;
            }
            if (Input.GetKeyDown(KeyCode.S))  {
                yVelocity = -1;
            }  else if (Input.GetKeyUp(KeyCode.S)){
                yVelocity = 0;
            }
            if (Input.GetKeyDown(KeyCode.D)) {
                movement = true;
                sr.flipX = false;
                xVelocity = 1;
            } else if (Input.GetKeyUp(KeyCode.D)) {
                movement = false;
                xVelocity = 0;
            }
            if (Input.GetKeyDown(KeyCode.A)){
                movement = true;
                sr.flipX = true;
                xVelocity = -1;
            }  else if (Input.GetKeyUp(KeyCode.A)) {
                movement = false;
                xVelocity = 0;
            }

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
