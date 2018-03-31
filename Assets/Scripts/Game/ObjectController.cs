using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectController : MonoBehaviour
{
    public int index;
    public float earnOxygenTime;
    public AudioClip soundEffect;
    public Sprite[] sprites;

    private GameController gameController;
    private DataController dataController;
    private QuestionData question;
    private int recoveredKeys;
    private float timeRemaining;
    private AudioSource[] audioSources;
    private int keyWarningTimes;

    private void Start()
    {
        audioSources = Camera.main.GetComponents<AudioSource>();

        dataController = FindObjectOfType<DataController>();
        gameController = FindObjectOfType<GameController>();

        if (index < PlayerPrefs.GetInt("questionsLength"))
        {
            question = dataController.GetQuestion(index);

            if (gameObject.tag == "Chest" && question.wasAnswered)
            {
                ChangeSprite(2);
                Destroy(gameObject.GetComponent<Collider2D>());
            }
            if (gameObject.tag == "Bubble" && PlayerPrefs.GetString("bubble_" + index).Equals("caught"))
                Destroy(gameObject);
            if (gameObject.tag == "Key" && PlayerPrefs.GetString("key_" + index).Equals("caught"))
                Destroy(gameObject);
        }
        else
            Destroy(gameObject);

    }

    private void Update()
    {
        keyWarningTimes = PlayerPrefs.GetInt("keyWarningTimes");
        recoveredKeys = PlayerPrefs.GetInt("recoveredKeys");
        timeRemaining = PlayerPrefs.GetFloat("timeRemaining");
    }

    private void OnMouseOver()
    {
        if (gameObject.tag == "Chest" && !question.wasAnswered)
            ChangeSprite(0);
        else if (gameObject.tag == "Key" || gameObject.tag == "Bubble")
            ChangeSprite(1);
    }

    private void OnMouseExit()
    {
        if (gameObject.tag == "Chest" && !question.wasAnswered)
            ChangeSprite(1);
        else if(gameObject.tag == "Key" || gameObject.tag == "Bubble")
            ChangeSprite(0);
    }

    private void OnMouseDown() {
        if (gameObject.tag == "Chest")
        {
            if (!question.wasAnswered && recoveredKeys > 0)
            {
                PlayerPrefs.SetInt("currentQuestion", index);
                PlayerPrefs.SetInt("recoveredKeys", recoveredKeys - 1);
                SceneManager.LoadScene("Quiz");
            } else if (!question.wasAnswered && recoveredKeys == 0) {
                PlayerPrefs.SetInt("keyWarningTimes", keyWarningTimes - 1);
                if (keyWarningTimes > 0) {
                    gameController.ShowKeyWarning(true);
                }
            }
        }
        else if (gameObject.tag == "Bubble")
        {
            audioSources[1].PlayOneShot(soundEffect);
            PlayerPrefs.SetFloat("timeRemaining", timeRemaining + earnOxygenTime);
            PlayerPrefs.SetString("bubble_" + index, "caught");
            Destroy(gameObject);
        }
        else if (gameObject.tag == "Key")
        {
            audioSources[1].PlayOneShot(soundEffect);
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

