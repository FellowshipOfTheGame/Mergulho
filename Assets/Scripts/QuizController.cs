﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    public float lostTime;
    public int maxScore;
    public int scoreLostPerError;

    public GameObject infoDisplay;
    public GameObject questionDisplay;

    public Text infoText;
    public Text questionNumber;
    public Text questionText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;

    private DataController dataController;
    private QuestionData curQuestionData;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    private float oxygenTime = 0;
    private float timeToAnswer = 0;
    private bool questionAvaliable = false;

    private void Start()
    {
        ShowInfoDisplay(true);
        ShowQuestionDisplay(false);

        dataController = FindObjectOfType<DataController>();

        curQuestionData = dataController.GetCurrentQuestionData();

        ShowQuestion();
    }

    private void Update()
    {
        if (questionAvaliable)
            timeToAnswer += Time.deltaTime;
    }

    private void ShowQuestion()
    {
        RemoveAnswerButtons();

        questionNumber.text = (curQuestionData.index + 1).ToString();
        infoText.text = curQuestionData.infoText;
        questionText.text = curQuestionData.questionText;

        for (int i = 0; i < curQuestionData.answers.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObject.transform.localScale = Vector3.one;

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.SetUp(curQuestionData.answers[i]);
        }

        questionAvaliable = true;
    }

    private void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect)
        {
            questionAvaliable = false;

            curQuestionData.questionScore = maxScore - scoreLostPerError * curQuestionData.mistakes;

            PlayerPrefs.SetInt("score", curQuestionData.questionScore + PlayerPrefs.GetInt("score"));

            curQuestionData.wasAnswered = true;
            curQuestionData.timeUsed = timeToAnswer;

            oxygenTime += PlayerPrefs.GetFloat("timeRemaining");
            PlayerPrefs.SetFloat("timeRemaining", oxygenTime);

            SceneManager.LoadScene("Game");
        }
        else
        {
            curQuestionData.mistakes++;
            oxygenTime -= lostTime;
        }
    }

    public void ShowInfoDisplay(bool show)
    {
        infoDisplay.SetActive(show);
    }

    public void ShowQuestionDisplay(bool show)
    {
        questionDisplay.SetActive(show);
    }
}
