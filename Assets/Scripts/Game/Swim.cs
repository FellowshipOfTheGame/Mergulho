﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour
{
    public Rigidbody2D rb;
    public int xVelocity;

    private GameController game;
    private Animator anim;

    private void Start ()
    {
        anim = GetComponent<Animator>();
        game = GameObject.FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update ()
    {
        if (game.isGameActive == true) {
            anim.SetBool("swim", true);
            rb.velocity = new Vector2(xVelocity, 0);
        } else {
            rb.velocity = new Vector2(0, 0);
            anim.SetBool("swim", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Destroyers") {
            Destroy(gameObject);
        }
    }
}
