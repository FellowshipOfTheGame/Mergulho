using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

    public GameObject item;
    private bool isActive;

    public void Start() {
        isActive = false;
    }

	public void ShowItem() {
        isActive = !isActive;
        item.SetActive(isActive);
    }
}
