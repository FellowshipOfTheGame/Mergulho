using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectController : MonoBehaviour
{
    public int index;
    public float earnOxygenTime;
    public Sprite[] sprites;
    public AudioClip soundEffect;

    private DataController dataController;
    private QuestionData questionData;
    private int recoveredKeys;
    private float timeRemaining;

    private void Start()
    {
        dataController = FindObjectOfType<DataController>();
        questionData = dataController.GetQuestionData(index);

        if (gameObject.tag == "Chest" && questionData.wasAnswered)
        {
            ChangeSprite(2);
            Destroy(gameObject.GetComponent<Collider2D>());
        }
        if (gameObject.tag == "Bubble" && PlayerPrefs.GetString("bubble_" + index).Equals("caught"))
            Destroy(gameObject);
        if (gameObject.tag == "Key" && PlayerPrefs.GetString("key_" + index).Equals("caught"))
            Destroy(gameObject);
    }

    private void Update()
    {
        recoveredKeys = PlayerPrefs.GetInt("recoveredKeys");
        timeRemaining = PlayerPrefs.GetFloat("timeRemaining");
    }

    private void OnMouseOver()
    {
        if (gameObject.tag == "Chest" && !questionData.wasAnswered)
            ChangeSprite(0);
        else if (gameObject.tag == "Key" || gameObject.tag == "Bubble")
            ChangeSprite(1);
    }

    private void OnMouseExit()
    {
        if (gameObject.tag == "Chest" && !questionData.wasAnswered)
            ChangeSprite(1);
        else if(gameObject.tag == "Key" || gameObject.tag == "Bubble")
            ChangeSprite(0);
    }

    private void OnMouseDown() {
        if (gameObject.tag == "Chest")
        {
            if (!questionData.wasAnswered && recoveredKeys > 0)
            {
                PlayerPrefs.SetInt("currentQuestion", index);
                PlayerPrefs.SetInt("recoveredKeys", recoveredKeys - 1);
                SceneManager.LoadScene("Quiz");
            }
        }
        else if (gameObject.tag == "Bubble")
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(soundEffect);
            PlayerPrefs.SetFloat("timeRemaining", timeRemaining + earnOxygenTime);
            PlayerPrefs.SetString("bubble_" + index, "caught");
            Destroy(gameObject);
        }
        else if (gameObject.tag == "Key")
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(soundEffect);
            PlayerPrefs.SetInt("recoveredKeys", recoveredKeys + 1);
            PlayerPrefs.SetString("key_" + index, "caught");
            Destroy(gameObject);
        }
    }

    private void ChangeSprite(int index)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index];
    }
}

