using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlbumController : MonoBehaviour
{
    public AudioSource bookFlip;
    public GameObject RightArrow;
    public GameObject LeftArrow;
    public GameObject page1;
    public GameObject page2;
    public GameObject frames2;

    private DataController dataController;
    private QuestionData questionData;
    private Image[] imgsPage1;
    private Image[] imgsPage2;
    private Sprite emptySprite;
    private Sprite newSprite;

    private int questionsLength;
    private int pages;
    private int currentPages;

    private void Start () {
        dataController = FindObjectOfType<DataController>();

        imgsPage1 = page1.GetComponentsInChildren<Image>();
        imgsPage2 = page2.GetComponentsInChildren<Image>();

        questionsLength = dataController.GetQuestionDataLengh();

        pages = Mathf.CeilToInt(questionsLength / 8f);

        currentPages = 1;

        emptySprite = Resources.Load<Sprite>("emptySprite");

        TurnPage(0);
	}

    private void Update()
    {
        if (currentPages == pages)
            RightArrow.SetActive(false);
        else
            RightArrow.SetActive(true);

        if (currentPages == 1)
            LeftArrow.SetActive(false);
        else
            LeftArrow.SetActive(true);
    }

    private void LoadImages(int index, int length, Image[] imgs)
    {
        for (int i = 0; index < length; i++)
        {
            if (index < questionsLength) {
                frames2.SetActive(true);

                questionData = dataController.GetQuestionData(index);

                if (questionData.wasAnswered)
                {
                    newSprite = Resources.Load<Sprite>("Collectible/" + index);

                    imgs[i].sprite = newSprite;
                    imgs[i].color = Color.white;
                }
                else
                {
                    imgs[i].sprite = emptySprite;
                    imgs[i].color = Color.clear;
                }
            }
            else
            {
                frames2.SetActive(false);
                imgs[i].sprite = emptySprite;
                imgs[i].color = Color.clear;
            }

            index++;
        }
    }

    public void TurnPage(int dir) {
        bookFlip.Play();

        if (dir == 1)
            currentPages++;
        else if(dir == -1)
            currentPages--;

        int index = 8 * (currentPages - 1);
        int length = 8 * currentPages;

        if (index < 0)
            index *= -1;

        LoadImages(index, length - 4, imgsPage1);
        LoadImages(index + 4, length, imgsPage2);
    }
}
