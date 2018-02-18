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
    public GameObject imagesContainer;

    private DataController dataController;
    private QuestionData questionData;
    private Image[] images;
    private int questionsLength;
    private int pages;
    private int currentPage;

    private void Start () {
        dataController = FindObjectOfType<DataController>();

        images = imagesContainer.GetComponentsInChildren<Image>();

        questionsLength = dataController.GetQuestionDataLengh();

        pages = (int)(questionsLength / images.Length);
        
        pages = 3;//-----remover

        currentPage = 1;

        TurnPage(0);
	}

    private void Update()
    {
        if (currentPage == pages)
            RightArrow.SetActive(false);
        else
            RightArrow.SetActive(true);

        if (currentPage == 1)
            LeftArrow.SetActive(false);
        else
            LeftArrow.SetActive(true);
    }

    public void TurnPage(int dir) {
        bookFlip.Play();

        if (dir == 1)
            currentPage++;
        else if(dir == -1)
            currentPage--;

        int imagesPerPage = images.Length * currentPage;
        int index = images.Length - imagesPerPage;

        if (index < 0)
            index *= -1;

        while (index < questionsLength)
        {
            questionData = dataController.GetQuestionData(index);

            if (questionData.wasAnswered)
            {
                Sprite obj = Resources.Load<Sprite>("Collectible/" + index);

                images[index].sprite = obj;
                images[index].color = Color.white;
            }

            index++;
        }
    }
}
