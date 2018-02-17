using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject pauseDisplay;
    public GameObject oxygenTimeBar;

    private float timePerCent;
    private float playTimeAvaliable;
    private float timeRemaining;

    private void Start()
    {
        Time.timeScale = 1;

        pauseDisplay.SetActive(false);

        timeRemaining = PlayerPrefs.GetFloat("timeRemaining");
        playTimeAvaliable = PlayerPrefs.GetFloat("playTimeAvaliable");
        timePerCent = timeRemaining / playTimeAvaliable;
    }

    private void Update()
    {
        CheckKeyDown();

        if (Time.timeScale == 1)
        {
            timeRemaining = PlayerPrefs.GetFloat("timeRemaining");

            timeRemaining -= Time.deltaTime;

            PlayerPrefs.SetFloat("timeRemaining", timeRemaining);

            timePerCent = timeRemaining / playTimeAvaliable;

            oxygenTimeBar.transform.localScale = new Vector3(timePerCent, oxygenTimeBar.transform.localScale.y);

            if (timeRemaining <= 0f)
                EndRound();
        }
    }

    private void CheckKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.A))
            LoadScene("Album");
        else if (Input.GetKeyDown(KeyCode.Escape))
            ShowPauseMenu(true);
    }

    private void PauseGame()
    {
        PlayerPrefs.SetFloat("timeRemaining", timeRemaining);
        Time.timeScale = 0;
    }

    private void EndRound()
    {
        PauseGame();
        print("VOCE PERDEU");
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        ShowPauseMenu(false);
    }

    public void ShowPauseMenu(bool show)
    {
        PauseGame();
        pauseDisplay.SetActive(show);
    }

    public void LoadScene(string name)
    {
        PauseGame();
        SceneManager.LoadScene(name);
    }
}