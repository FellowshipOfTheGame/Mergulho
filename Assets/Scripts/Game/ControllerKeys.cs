using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerKeys : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidbody2;
    public SpriteRenderer spriteRenderer;
    public AudioClip swimSound, reentrySound;

    private Animator animator;
    public float yVelocity, xVelocity;
    private bool movement, play;
    private GameController game;
    private AudioSource[] audioSources;

    void Awake()
    {
        audioSources = Camera.main.GetComponents<AudioSource>();
        animator = GetComponent<Animator>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        game = FindObjectOfType<GameController>();

        movement = false;
        animator.SetBool("HorizontalMovement", movement);

        LoadPlayerPosition();
    }

    void Update()
    {
        if (game.isGameActive)
        {
            animator.speed = 1;

            yVelocity = Input.GetAxis("Vertical");
            xVelocity = Input.GetAxis("Horizontal");

            if (xVelocity == 0)
            {
                movement = false;
                audioSources[0].Stop();
            }
            else
            {
                movement = true;
                if (!audioSources[0].isPlaying)
                    audioSources[0].PlayOneShot(swimSound);
            }

            if (xVelocity > 0 && xVelocity <= 1)
                spriteRenderer.flipX = false;
            if (xVelocity < 0 && xVelocity >= -1)
                spriteRenderer.flipX = true;

            rigidbody2.velocity = new Vector2(xVelocity, yVelocity) * speed;
        }
        else
        {
            audioSources[0].Pause();
            yVelocity = 0;
            xVelocity = 0;
            rigidbody2.velocity = new Vector2(0, 0);
            movement = false;
            animator.speed = 0;
        }

        animator.SetBool("HorizontalMovement", movement);

        SavePlayerPosition();
    }

    private void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("playerX", transform.position.x);
        PlayerPrefs.SetFloat("playerY", transform.position.y);
        PlayerPrefs.SetFloat("playerZ", transform.position.z);
    }

    public Vector3 LoadPlayerPosition()
    {
        transform.position = new Vector3(PlayerPrefs.GetFloat("playerX"), PlayerPrefs.GetFloat("playerY"), PlayerPrefs.GetFloat("playerZ"));
        return transform.position;
    }
}
