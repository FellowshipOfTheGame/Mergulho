using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestController : MonoBehaviour {

    public int index;

    private void OnMouseDown()
    {
        PlayerPrefs.SetInt("currentQuestion", index);
        SceneManager.LoadScene("Quiz");
    }
}
