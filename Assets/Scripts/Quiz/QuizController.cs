using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    public float lostTime;
    public int maxScore, scoreLostPerError;
    public AudioClip cameraFlash, openChest;
    public GameObject infoDisplay, questionDisplay, newPhotoDisplay, oxygenTimeBar;
    public Text infoText, questionNumber, questionText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;

    private DataController dataController;
    private QuestionData curQuestion;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();
    private float oxygenTime, timeToAnswer = 0, playTimeAvaliable, timePerCent;
    private bool questionAvaliable = false;

    private void Start()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(openChest);

        ShowInfoDisplay(true);
        ShowQuestionDisplay(false);
        ShowPhotoDisplay(false);

        dataController = FindObjectOfType<DataController>();

        curQuestion = dataController.GetQuestion(PlayerPrefs.GetInt("currentQuestion"));

        oxygenTime = PlayerPrefs.GetFloat("timeRemaining");

        playTimeAvaliable = PlayerPrefs.GetFloat("playTimeAvaliable");

        ShowQuestion();
    }

    private void Update()
    {
        if (questionAvaliable)
            timeToAnswer += Time.deltaTime;

        timePerCent = oxygenTime / playTimeAvaliable;

        oxygenTimeBar.transform.localScale = new Vector3(timePerCent, oxygenTimeBar.transform.localScale.y);
    }

    private void ShowQuestion()
    {
        RemoveAnswerButtons();

        questionNumber.text = (System.Int32.Parse(curQuestion.index) + 1).ToString();
        infoText.text = curQuestion.infoText;
        questionText.text = curQuestion.questionText;

        for (int i = 0; i < curQuestion.answers.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObject.transform.localScale = Vector3.one;

            ColorBlock colors;
            if (!curQuestion.answers[i].isCorrect)
            {
                colors = answerButtonGameObject.GetComponent<Button>().colors;
                colors.pressedColor = Color.red;
                answerButtonGameObject.GetComponent<Button>().colors = colors;
            }
            else
            {
                colors = answerButtonGameObject.GetComponent<Button>().colors;
                colors.pressedColor = Color.green;
                answerButtonGameObject.GetComponent<Button>().colors = colors;
            }

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.SetUp(curQuestion.answers[i]);
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

            curQuestion.questionScore = maxScore - scoreLostPerError * curQuestion.mistakes;

            PlayerPrefs.SetInt("score", curQuestion.questionScore + PlayerPrefs.GetInt("score"));

            curQuestion.wasAnswered = true;
            curQuestion.timeUsed = timeToAnswer;

            PlayerPrefs.SetFloat("timeRemaining", oxygenTime);
            PlayerPrefs.SetInt("questionsAnswered", PlayerPrefs.GetInt("questionsAnswered") + 1);

            Camera.main.GetComponent<AudioSource>().PlayOneShot(cameraFlash);
            ShowPhotoDisplay(true);
            ShowQuestionDisplay(false);
        }
        else
        {
            curQuestion.mistakes++;
            oxygenTime -= lostTime;
        }
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowInfoDisplay(bool show)
    {
        infoDisplay.SetActive(show);
    }

    public void ShowQuestionDisplay(bool show)
    {
        questionDisplay.SetActive(show);
    }

    public void ShowPhotoDisplay(bool show)
    {
        newPhotoDisplay.SetActive(show);
    }
}
