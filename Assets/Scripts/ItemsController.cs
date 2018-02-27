using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    private GameObject[] keys;
    private GameObject[] bubbles;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
