using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour {

    public GameObject info;
    public GameObject question;

    // Use this for initialization
    private void Start() {
        LoadScreenInfo();
    }

    public void LoadScreenInfo() {
        info.SetActive(true);
        question.SetActive(false);
    }

    public void LoadScreenQuestion() {
        info.SetActive(false);
        question.SetActive(true);
    }

    public void QuizScreenOff() {
        info.SetActive(false);
        question.SetActive(false);
    }
}
