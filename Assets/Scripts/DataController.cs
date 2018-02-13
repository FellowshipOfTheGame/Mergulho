using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO; //This namespace contains functions related to loading and saving files

public class DataController : MonoBehaviour
{
    private RoundData[] allRoundData;

    private string gameDataFileName = "data.json";

    public float timeRemaining;
    public int score;
    public int currentQuestion;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        LoadGameData();

        StartPlayerPrefs();

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

            // Retrieve the allRoundData property of loadedData
            allRoundData = loadedData.allRoundData;
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

    private void StartPlayerPrefs()
    {
        PlayerPrefs.SetFloat("timeRemaining", 30);
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("clickedChest", 0);
    }

    public void GetPlayerPrefs()
    {
        timeRemaining = PlayerPrefs.GetFloat("timeRemaining");
        score = PlayerPrefs.GetInt("score");
        currentQuestion = PlayerPrefs.GetInt("currentQuestion");
    }

    public void UpdatePlayerPrefs()
    {
        PlayerPrefs.SetFloat("timeRemaining", timeRemaining);
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("currentQuestion", currentQuestion);
    }

    public RoundData GetCurrentRoundData()
    {
        // If we wanted to return different rounds, we could do that here
        // We could store an int representing the current round index in PlayerProgress
        return allRoundData[currentQuestion];
    }
}