using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    public float lostTime;

    public GameObject infoDisplay;
    public GameObject questionDisplay;

    public Text infoText;
    public Text questionNumber;
    public Text questionText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;

    private DataController dataController;
    private QuestionData currentQuestionData;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    private float oxygenTime = 0;
    private int scoreEarned = 0;
    private float timeToAnswer = 0;
    private bool questionAvaliable = false;

    private void Start()
    {
        infoDisplay.SetActive(true);
        questionDisplay.SetActive(false);

        dataController = FindObjectOfType<DataController>();

        currentQuestionData = dataController.GetCurrentQuestionData();

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

        questionNumber.text = (currentQuestionData.index + 1).ToString();
        infoText.text = currentQuestionData.infoText;
        questionText.text = currentQuestionData.questionText;

        for (int i = 0; i < currentQuestionData.answers.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObject.transform.localScale = Vector3.one;

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.SetUp(currentQuestionData.answers[i]);
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
            //Calcular pontuação a partir de um range de tempo (quanto mais tempo menos pontos)
            //Salvar quanto tempo foi levado para responder a pergunta e quantos erros cometeu
            questionAvaliable = false;

            scoreEarned = (int)timeToAnswer / 10;

            scoreEarned += PlayerPrefs.GetInt("score");
            PlayerPrefs.SetInt("score", scoreEarned);

            oxygenTime += PlayerPrefs.GetFloat("timeRemaining");
            PlayerPrefs.SetFloat("timeRemaining", oxygenTime);

            SceneManager.LoadScene("Game");
        }
        else
        {
            currentQuestionData.mistakes++;
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
