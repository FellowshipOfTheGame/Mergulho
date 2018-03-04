﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataController : MonoBehaviour
{
    public float timePerQuestion;

    private QuestionData[] allQuestionData;
    private string gameDataFileName = "data.json";
    private float playTimeAvaliable;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        //Carregar progresso do player de json 

        DontDestroyOnLoad(gameObject);

        LoadGameData();

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
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
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