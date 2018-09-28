using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;

public class DataController : MonoBehaviour
{
    // Tempo por questao
    public float time;
    
    private QuestionData[] questions;
    private Sprite[] sprites;
    private float playTimeAvaliable;
    private bool loaded;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        loaded = false;
    }

    private void Update()
    {
        if (!loaded)
        {
            loaded = true;
            LoadJsonData();
            LoadImages();
            StartPlayerPrefs();
            SceneManager.LoadScene("Start");
            Camera.main.enabled = false;
        }
    }

    private void StartPlayerPrefs()
    {
        PlayerPrefs.SetFloat("playTimeAvaliable", playTimeAvaliable);
        PlayerPrefs.SetFloat("timeRemaining", playTimeAvaliable);
        PlayerPrefs.SetInt("questionsLength", questions.Length);
        PlayerPrefs.SetInt("currentQuestion", 0);
        PlayerPrefs.SetInt("recoveredKeys", 0);
        PlayerPrefs.SetInt("questionsAnswered", 0);
        PlayerPrefs.SetInt("keyWarningTimes", 3);
        PlayerPrefs.SetInt("clickChestWKey", 0);
    }

    private void LoadJsonData()
    {
        TextAsset jsonTextFile = Resources.Load<TextAsset>("Quiz/questions_" + PlayerPrefs.GetString("lang"));
        GameData loadedData = JsonUtility.FromJson<GameData>(jsonTextFile.text);
        questions = loadedData.questions;
        playTimeAvaliable = questions.Length * time;
    }

    private void LoadImages()
    {
        Texture2D texture;
        Rect r;
        Vector2 v;
        sprites = new Sprite[questions.Length];
        for (int i = 0; i < questions.Length; i++)
        {
            texture = Resources.Load<Texture2D>("Quiz/" + i);
            r = new Rect(0, 0, texture.width, texture.height);
            v = new Vector2(0.5f, 0.5f);
            sprites[i] = Sprite.Create(texture, r, v);
        }
    }

    /*  Public Methods  */

    public Sprite[] GetSprites()
    {
        return sprites;
    }

    public QuestionData GetQuestion(int index)
    {
        return questions[index];
    }
}