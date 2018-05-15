using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    public float lostTime;
    public AudioClip cameraFlash, openChest, bubbles;
    public GameObject answersObj, infoDisplay, questionDisplay, newPhotoDisplay, oxygenTimeBar;
    public Text infoText, questionNumber, questionText;

    private AudioSource audioSource;
    private DataController dataController;
    private QuestionData curQuestion;
    private AnswerButton[] answers;
    private float oxygenTime, timeToAnswer = 0, playTimeAvaliable, timePerCent;
    private bool questionAvaliable = false;

    private void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();

        audioSource.PlayOneShot(openChest);

        ShowInfoDisplay(true);
        ShowQuestionDisplay(false);
        ShowPhotoDisplay(false);

        dataController = FindObjectOfType<DataController>();

        curQuestion = dataController.GetQuestion(PlayerPrefs.GetInt("currentQuestion"));

        oxygenTime = PlayerPrefs.GetFloat("timeRemaining");

        playTimeAvaliable = PlayerPrefs.GetFloat("playTimeAvaliable");

        answers = answersObj.GetComponentsInChildren<AnswerButton>();

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
        questionNumber.text = (System.Int32.Parse(curQuestion.index) + 1).ToString();
        infoText.text = curQuestion.infoText;
        questionText.text = curQuestion.questionText;

        ColorBlock colors;

        for (int i = 0; i < answers.Length; i++)
        {
            //Muda a cor para o botao pressionado
            colors = answers[i].GetComponent<Button>().colors;
            if (!curQuestion.answers[i].isCorrect)
            {
                colors.pressedColor = Color.red;
            }
            else
            {
                colors.pressedColor = Color.green;
            }
            answers[i].GetComponent<Button>().colors = colors;

            //Muda o texto da alternativa
            answers[i].SetUp(curQuestion.answers[i]);
        }

        questionAvaliable = true;
    }

    public void AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect)
        {
            questionAvaliable = false;

            curQuestion.wasAnswered = true;
            curQuestion.timeUsed = timeToAnswer;

            PlayerPrefs.SetFloat("timeRemaining", oxygenTime);
            PlayerPrefs.SetInt("questionsAnswered", PlayerPrefs.GetInt("questionsAnswered") + 1);

            audioSource.PlayOneShot(cameraFlash);

            ShowPhotoDisplay(true);
            ShowQuestionDisplay(false);
        }
        else
        {
            audioSource.PlayOneShot(bubbles);
            curQuestion.mistakes++;
            oxygenTime -= lostTime;

            PlayerPrefs.SetFloat("timeRemaining", oxygenTime);

            if (oxygenTime <= 0f)
                SceneManager.LoadScene("Lose");
        }
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
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
