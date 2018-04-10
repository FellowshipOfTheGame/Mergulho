using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlbumController : MonoBehaviour
{
    public AudioSource bookFlip;
    public GameObject RightArrow, LeftArrow, framesObject, imagesObject, lupaObject, bigFrame;
    public Image bigImage;

    private DataController dataController;
    private QuestionData questionData;
    private Image[] frames, images, lupasSprites;
    private Button[] lupas;
    private Sprite newSprite;
    private Sprite[] sprites;
    private int questionsLength, pages, currentPages, index, length;

    private void Start () {
        dataController = FindObjectOfType<DataController>();
        sprites = dataController.GetSprites();
        questionsLength = PlayerPrefs.GetInt("questionsLength");

        frames = framesObject.GetComponentsInChildren<Image>();
        images = imagesObject.GetComponentsInChildren<Image>();
        lupas = lupaObject.GetComponentsInChildren<Button>();
        lupasSprites = lupaObject.GetComponentsInChildren<Image>();

        pages = Mathf.CeilToInt(questionsLength / 8f);
        currentPages = 1;

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

    private void LoadImages()
    {
        for (int i = 0; index < length; i++)
        {
            if (index < questionsLength)
            {
                frames[i].color = Color.white;

                questionData = dataController.GetQuestion(index);

                if (questionData.wasAnswered)
                {
                    images[i].sprite = sprites[index];
                    images[i].color = Color.white;
                    lupas[i].interactable = true;
                    lupasSprites[i].color = Color.white;
                }
                else {
                    images[i].color = Color.clear;
                    lupas[i].interactable = false;
                    lupasSprites[i].color = Color.clear;
                }
            }
            else
            {
                frames[i].color = Color.clear;
                images[i].color = Color.clear;
                lupasSprites[i].color = Color.clear;
                lupas[i].interactable = false;
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

        index = 8 * (currentPages - 1);
        length = 8 * currentPages;

        if (index < 0)
            index *= -1;

        LoadImages();
    }

    public void LoadScene(string name)
    {
        if (PlayerPrefs.GetFloat("timeRemaining") <= 0f)
            SceneManager.LoadScene("Lose");
        else if(PlayerPrefs.GetInt("questionsAnswered") == PlayerPrefs.GetInt("questionsLength"))
            SceneManager.LoadScene("Win");
        else
            SceneManager.LoadScene(name);
    }

    public void BigFrameActive(int index) {
        if (index < 0) {
            bigFrame.SetActive(false);
            bigImage.color = Color.clear;

        } else {
            if (currentPages > 1) {
                index += (8*(currentPages -1));
            }
            bigImage.sprite = sprites[index];
            bigImage.color = Color.white;
            bigFrame.SetActive(true);
        }
    }
}
