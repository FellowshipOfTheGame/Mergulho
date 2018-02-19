﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizController : MonoBehaviour {

    public GameObject infoDisplay;
    public GameObject questionDisplay;

    public Text infoText;
    public Text numberText;
    public Text questionText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;

    private DataController dataController;
    private QuestionData currentQuestionData;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    private void Start()
    {
        infoDisplay.SetActive(true);
        questionDisplay.SetActive(false);

        dataController = FindObjectOfType<DataController>();
        // Store a reference to the DataController so we can request the data we need for 
        //this round

        currentQuestionData = dataController.GetCurrentQuestionData();
        // Ask the DataController for the data for the current round. At the moment, 
        // we only have one round - but we could extend this

        ShowQuestion();
    }

    private void ShowQuestion()
    {
        RemoveAnswerButtons();
                          
        // Get the QuestionData for the current question
        questionText.text = currentQuestionData.questionText;
        // Update questionText with the correct text
        infoText.text = currentQuestionData.info;

        for (int i = 0; i < currentQuestionData.answers.Length; i++)                              
        // For every AnswerData in the current QuestionData...
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();         
            // Spawn an AnswerButton from the object pool
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObject.transform.localScale = Vector3.one;

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.SetUp(currentQuestionData.answers[i]);                                    
            // Pass the AnswerData to the AnswerButton so the AnswerButton knows what text to display and whether it is the correct answer
        }
    }

    private void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)                                            // Return all spawned AnswerButtons to the object pool
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect)
        {
            //Calcular pontuação a partir de um range de tempo (quanto mais tempo menos pontos)
            //Salvar quanto tempo foi levado para responder a pergunta e quantos erros cometeu
            dataController.score += 100;
            dataController.UpdatePlayerPrefs();

            SceneManager.LoadScene("Game");
        }
        else
        {
            //Retira oxigenio (tempo) do que o player tem
            dataController.UpdatePlayerPrefs();
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
