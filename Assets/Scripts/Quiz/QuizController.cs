using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    public float lostTime;
    public int maxScore;
    public int scoreLostPerError;

    public AudioClip cameraFlash;
    public AudioClip openChest;
    public GameObject infoDisplay;
    public GameObject questionDisplay;
    public GameObject newPhotoDisplay;
    public GameObject oxygenTimeBar;

    public Text infoText;
    public Text questionNumber;
    public Text questionText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;

    private DataController dataController;
    private QuestionData curQuestionData;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    private float oxygenTime;
    private float timeToAnswer = 0;
    private float playTimeAvaliable;
    private float timePerCent;
    private bool questionAvaliable = false;

    private void Start()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(openChest);

        ShowInfoDisplay(true);
        ShowQuestionDisplay(false);
        ShowPhotoDisplay(false);

        dataController = FindObjectOfType<DataController>();

        curQuestionData = dataController.GetCurrentQuestionData();

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

        questionNumber.text = (System.Int32.Parse(curQuestionData.index) + 1).ToString();
        infoText.text = curQuestionData.infoText;
        questionText.text = curQuestionData.questionText;

        for (int i = 0; i < curQuestionData.answers.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObject.transform.localScale = Vector3.one;

            ColorBlock colors;
            if (!curQuestionData.answers[i].isCorrect)
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

            PlayerPrefs.SetFloat("timeRemaining", oxygenTime);
            PlayerPrefs.SetInt("questionsAnswered", PlayerPrefs.GetInt("questionsAnswered") + 1);

            Camera.main.GetComponent<AudioSource>().PlayOneShot(cameraFlash);
            ShowPhotoDisplay(true);
            ShowQuestionDisplay(false);
        }
        else
        {
            curQuestionData.mistakes++;
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
