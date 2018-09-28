using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantController : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Vector3 posicao;
    private ControllerKeys playerController;
    private GameController game;
    public Animator animator;
    private bool movement;


    void Start () {
        game = FindObjectOfType<GameController>();
        playerController = FindObjectOfType<ControllerKeys>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        posicao = playerController.LoadPlayerPosition();
        posicao.x -= 0.3f;
        posicao.y += 1.5f;
        transform.position = posicao;
	}

    void Update() {
        if (game.isGameActive) {

            if (playerController.xVelocity == 0)  {
                movement = false;
            } else {
                movement = true;
            }

            if (playerController.xVelocity > 0 && playerController.xVelocity <= 1)
                spriteRenderer.flipX = false;
            if (playerController.xVelocity < 0 && playerController.xVelocity >= -1)
                spriteRenderer.flipX = true;
        }  else {
            movement = false;
        }
        animator.SetBool("HorizontalMovement", movement);
    }
}
