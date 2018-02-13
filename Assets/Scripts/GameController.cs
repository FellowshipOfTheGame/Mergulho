using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject pauseDisplay;

    private DataController dataController;
    private bool isRoundActive = false;

    private void Start()
    {
        isRoundActive = true;

        pauseDisplay.SetActive(false);

        dataController = FindObjectOfType<DataController>();
        dataController.GetPlayerPrefs();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            LoadScene("Album");
        else if (Input.GetKeyDown(KeyCode.Escape))
            ShowPauseMenu(true);

        if (isRoundActive)
        {
            dataController.timeRemaining -= Time.deltaTime; 

            print(dataController.timeRemaining.ToString());

            if (dataController.timeRemaining <= 0f)
                EndRound();
        }
    }

    private void EndRound()
    {
        isRoundActive = false;

        print("VOCE PERDEU");
    }

    public void SavesTimeRemaining()
    {
        dataController.timeRemaining += 10;
    }

    public void ShowPauseMenu(bool show)
    {
        isRoundActive = !show;

        pauseDisplay.SetActive(show);
    }

    public void LoadScene(string name)
    {
        isRoundActive = false;
        dataController.UpdatePlayerPrefs();

        SceneManager.LoadScene(name);
    }
}
