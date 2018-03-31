using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;

public class DataController : MonoBehaviour
{
    public float timePerQuestion;
    public GameObject sprite;

    private SpriteRenderer sprRend;
    private QuestionData[] questions;
    private Texture2D[] textures;
    private Sprite[] sprites;
    private bool isFinished, isLoadedJson, isLoadedImages, isDataReady, isLoading;
    private string jsonFilePath, imagesPath, jsonData;
    private float playTimeAvaliable;

    private void Start()
    {
        PlayerPrefs.DeleteAll();

        DontDestroyOnLoad(gameObject);

        sprRend = sprite.GetComponent<SpriteRenderer>();

        isLoading = false;
        isLoadedJson = false;
        isLoadedImages = false;
        isDataReady = false;
        isFinished = false;

        PlayerPrefs.SetInt("isFinished", 1);

        jsonFilePath = Path.Combine(Application.streamingAssetsPath, "questoes.json");
        imagesPath = Path.Combine(Application.streamingAssetsPath, "Collectible/");
    }

    private void Update()
    {
        if (!isFinished)
        {
            sprRend.color = new Color(sprRend.color.r, sprRend.color.g, sprRend.color.b, Mathf.PingPong(Time.time, 1f));

            if (!isLoadedJson)
            {
                isLoadedJson = true;
                StartCoroutine(RequestJsonFile());
            }

            if (isLoading) {
                isLoading = false;
                LoadJsonData();
            }

            if (!isLoadedImages && isDataReady)
            {
                isDataReady = false;
                StartCoroutine(RequestImages());
            }

            if (isLoadedImages)
            {
                isFinished = true;
                PlayerPrefs.SetInt("isFinished", 0);

                LoadImages();

                StartPlayerPrefs();

                SceneManager.LoadScene("Start");
                Camera.main.enabled = false;
            }
        }
    }

    private IEnumerator RequestJsonFile()
    {
        //WebGL Build
        if (jsonFilePath.Contains("://") || jsonFilePath.Contains(":///"))
        {
            UnityWebRequest www = UnityWebRequest.Get(jsonFilePath);
            yield return www.Send();
            jsonData = www.downloadHandler.text;
        }
        else //Desktop Build
        {
            if (File.Exists(jsonFilePath))
                jsonData = File.ReadAllText(jsonFilePath);
            else
                Debug.LogError("Cannot load game data!");
        }

        isLoading = true;
    }

    private void LoadJsonData()
    {
        GameData loadedData = JsonUtility.FromJson<GameData>(jsonData);
        questions = loadedData.questions;
        playTimeAvaliable = questions.Length * timePerQuestion;

        isDataReady = true;
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
    }

    private IEnumerator RequestImages()
    {
        string oneImagePath;
        textures = new Texture2D[questions.Length];

        //WebGL Build
        if (imagesPath.Contains("://") || imagesPath.Contains(":///"))
        {
            UnityWebRequest www;
            for (int i = 0; i < questions.Length; i++)
            {
                oneImagePath = Path.Combine(imagesPath, i + ".png");

                www = UnityWebRequest.GetTexture(oneImagePath);
                yield return www.Send();

                if (www.isError)
                    Debug.LogError(www.error);
                else
                    textures[i] = ((DownloadHandlerTexture)www.downloadHandler).texture;
            }
        }
        else //Desktop Build
        {
            Texture2D texture = null;
            byte[] imageBytes;

            for (int i = 0; i < questions.Length; i++)
            {
                texture = null;
                oneImagePath = Path.Combine(imagesPath, i + ".png");

                if (File.Exists(oneImagePath))
                {
                    imageBytes = File.ReadAllBytes(oneImagePath);
                    texture = new Texture2D(170, 160);
                    texture.LoadImage(imageBytes);
                    textures[i] = texture;
                }
                else
                    Debug.LogError("Cannot load game data!");
            }
        }

        isLoadedImages = true;
    }

    private void LoadImages()
    {
        Rect r;
        Vector2 v;
        sprites = new Sprite[questions.Length];

        for (int i = 0; i < questions.Length; i++)
        {
            r = new Rect(0, 0, textures[i].width, textures[i].height);
            v = new Vector2(0.5f, 0.5f);
            sprites[i] = Sprite.Create(textures[i], r, v);
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