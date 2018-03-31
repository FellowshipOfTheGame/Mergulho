using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject HUD, pauseDisplay, keyWarning, oxygenTimeBar, eventSystem, playerComponents;
    public Text recoveredKeys;
    public bool isGameActive;
 
    private float timePerCent, playTimeAvaliable, timeRemaining;

    private void Start()
    {
        Camera.main.GetComponent<AudioListener>().enabled = true;

        eventSystem.SetActive(true);
        playerComponents.SetActive(true);
        HUD.SetActive(true);
        pauseDisplay.SetActive(false);

        isGameActive = true;

        timeRemaining = PlayerPrefs.GetFloat("timeRemaining");
        playTimeAvaliable = PlayerPrefs.GetFloat("playTimeAvaliable");
        timePerCent = timeRemaining / playTimeAvaliable;

        recoveredKeys.text = PlayerPrefs.GetInt("recoveredKeys").ToString();
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("questionsAnswered") == PlayerPrefs.GetInt("questionsLength"))
            WinORLose("Win");
        else {
            if (isGameActive)
            {
                UpdateTimeBar();

                if (timeRemaining <= 0f)
                    WinORLose("Lose");
            }
        }
    }

    public void WinORLose(string sceneName)
    {
        PauseGame(true);

        SceneManager.LoadScene(sceneName);
    }

    private void UpdateTimeBar()
    {
        recoveredKeys.text = PlayerPrefs.GetInt("recoveredKeys").ToString();

        timeRemaining = PlayerPrefs.GetFloat("timeRemaining");

        timeRemaining -= Time.deltaTime;

        PlayerPrefs.SetFloat("timeRemaining", timeRemaining);

        timePerCent = timeRemaining / playTimeAvaliable;

        if (timePerCent > 1)
            timePerCent = 1;

        oxygenTimeBar.transform.localScale = new Vector3(timePerCent, oxygenTimeBar.transform.localScale.y);
    }

    private void PauseGame(bool pause)
    {
        isGameActive = !pause;
        PlayerPrefs.SetFloat("timeRemaining", timeRemaining);
    }

    public void ShowPauseMenu(bool show)
    {
        PauseGame(show);

        pauseDisplay.SetActive(show);
    }

    public void ShowKeyWarning(bool show) {
        PauseGame(show);

        keyWarning.SetActive(show);
    }

    public void LoadScene(string name)
    {
        PauseGame(true);

        playerComponents.SetActive(false);
        HUD.SetActive(false);
        eventSystem.SetActive(false);

        Camera.main.GetComponent<AudioListener>().enabled = false;

        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }
}