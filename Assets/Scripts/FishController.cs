using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour {

    private float xVelocity;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private float size;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Random.ColorHSV(0f, 1f, 0.4f, 0.7f, 0.5f, 1f);
        size = Random.Range(0.8f, 1.3f);
        this.transform.localScale = new Vector3 (size, size, 0f);
    }

    // Update is called once per frame
    void Update () {
        rb = GetComponent<Rigidbody2D>();
        xVelocity = rb.velocity.x;
    }
    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.gameObject.tag == "Tubarao"){
            Destroy(gameObject);
        } else if (trigger.gameObject.tag == "Player"){
            rb.velocity = new Vector2(0, 0);
        }
    }
    void OnTriggerExit2D(Collider2D trigger) {
        if (trigger.gameObject.tag == "Player") {
            rb.velocity = new Vector2(xVelocity, 0);
        }
    }
}
