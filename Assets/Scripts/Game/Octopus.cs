using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour
{
    public Rigidbody2D rb;
    public int xVelocity, yVelocity;

    private GameController game;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        game = GameObject.FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (game.isGameActive == true)
        {
            anim.SetBool("swim", true);
            rb.velocity = new Vector2(xVelocity, yVelocity);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            anim.SetBool("swim", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Destroyers")
            Destroy(gameObject);
    }
}
