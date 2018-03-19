using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataController : MonoBehaviour
{
    public float timePerQuestion;
    public GameObject sprite;

    private QuestionData[] allQuestionData;
    private string gameDataFileName = "data.json";
    private float playTimeAvaliable;
    private Sprite[] sprites;

    private void Start()
    {
        PlayerPrefs.DeleteAll();

        DontDestroyOnLoad(gameObject);

        LoadGameData();

        LoadCollectibleImages();

        SceneManager.LoadScene("Start");
    }

    private void LoadGameData()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, 
        // and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

            // Retrieve the allQuestionData property of loadedData
            allQuestionData = loadedData.allQuestionData;

            playTimeAvaliable = allQuestionData.Length * timePerQuestion;

            PlayerPrefs.SetFloat("playTimeAvaliable", playTimeAvaliable);
            PlayerPrefs.SetFloat("timeRemaining", playTimeAvaliable);
            PlayerPrefs.SetInt("score", 0);
            PlayerPrefs.SetInt("currentQuestion", 0);
            PlayerPrefs.SetInt("recoveredKeys", 0);
            PlayerPrefs.SetInt("questionsAnswered", 0);
            PlayerPrefs.SetInt("questionsLength", GetQuestionDataLengh());
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

    private void LoadCollectibleImages()
    {
        Texture2D texture = null;
        Rect r;
        Vector2 v;

        byte[] imageBytes;
        string oneImagePath;
        string imagesPath = Path.Combine(Application.streamingAssetsPath, "Collectible/");
        sprites = new Sprite[allQuestionData.Length];

        for (int i = 0; i < allQuestionData.Length; i++)
        {
            texture = null;
            oneImagePath = imagesPath + i + ".png";

            if (File.Exists(oneImagePath))
            {
                imageBytes = File.ReadAllBytes(oneImagePath);
                texture = new Texture2D(170, 160);
                texture.LoadImage(imageBytes);
                r = new Rect(0, 0, texture.width, texture.height);
                v = new Vector2(0.5f, 0.5f);
                sprites[i] = Sprite.Create(texture, r, v);
            }
        }
    }

    public Sprite[] GetCollectibleSprites()
    {
        return sprites;
    }

    public QuestionData GetCurrentQuestionData()
    {
        return allQuestionData[PlayerPrefs.GetInt("currentQuestion")];
    }

    public QuestionData GetQuestionData(int index)
    {
        return allQuestionData[index];
    }

    public int GetQuestionDataLengh()
    {
        return allQuestionData.Length;
    }
}