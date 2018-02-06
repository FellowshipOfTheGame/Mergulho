using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizController : MonoBehaviour {

    public GameObject infoScreen;
    public GameObject questionScreen;

    public Text infoText;
    public Text numberText;
    public Text questionText;

    private DataController dataController;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;

    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;

    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    private int questionIndex;

    // Use this for initialization
    private void Start()
    {
        infoScreen.SetActive(true);
        questionScreen.SetActive(false);

        dataController = FindObjectOfType<DataController>();                              
        // Store a reference to the DataController so we can request the data we need for this round

        currentRoundData = dataController.GetCurrentRoundData();
        // Ask the DataController for the data for the current round. At the moment, 
        // we only have one round - but we could extend this

        questionPool = currentRoundData.questions;
        // Take a copy of the questions so we could shuffle the pool or drop questions from it 
        // without affecting the original RoundData object

        ShowQuestion();
    }

    void ShowQuestion()
    {
        RemoveAnswerButtons();

        QuestionData questionData = questionPool[questionIndex];                            
        // Get the QuestionData for the current question
        questionText.text = questionData.questionText;
        // Update questionText with the correct text

        for (int i = 0; i < questionData.answers.Length; i++)                              
        // For every AnswerData in the current QuestionData...
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();         
            // Spawn an AnswerButton from the object pool
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObject.transform.localScale = Vector3.one;

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.SetUp(questionData.answers[i]);                                    
            // Pass the AnswerData to the AnswerButton so the AnswerButton knows what text to display and whether it is the correct answer
        }
    }

    void RemoveAnswerButtons()
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
            SceneManager.LoadScene("Game");
        }
    }

    public void ShowInfoScreen(bool show)
    {
        infoScreen.SetActive(show);
    }

    public void ShowQuestionScreen(bool show)
    {
        questionScreen.SetActive(show);
    }
}
