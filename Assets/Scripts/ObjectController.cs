using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectController : MonoBehaviour
{
    public int index;
    public float earnOxygenTime;
    public Sprite[] sprites;

    private DataController dataController;
    private QuestionData questionData;
    private int recoveredKeys;
    private float timeRemaining;

    private void Start()
    {
        dataController = FindObjectOfType<DataController>();

        questionData = dataController.GetQuestionData(index);

        if (questionData.wasAnswered && gameObject.tag == "Chest")
            ChangeSprite(1);
    }

    private void Update()
    {
        recoveredKeys = PlayerPrefs.GetInt("recoveredKeys");
        timeRemaining = PlayerPrefs.GetFloat("timeRemaining");
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
            PlayerPrefs.SetFloat("timeRemaining", timeRemaining + earnOxygenTime);
            Destroy(gameObject);
        }
        else if (gameObject.tag == "Key")
        {
            PlayerPrefs.SetInt("recoveredKeys", recoveredKeys + 1);
            Destroy(gameObject);
        }
    }

    private void ChangeSprite(int index)
    {
        this.GetComponent<SpriteRenderer>().sprite = sprites[index];
    }
}

