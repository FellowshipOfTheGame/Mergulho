using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public SpriteRenderer sr;

    private float size;

    void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Random.ColorHSV(0f, 1f, 0.4f, 0.7f, 0.5f, 1f);
        size = Random.Range(0.8f, 1.3f);
        this.transform.localScale = new Vector3 (size, size, 0f);
    }
}
