using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject HUD;
    public GameObject pauseDisplay;
    public GameObject oxygenTimeBar;
    public Text recoveredKeys;
    public bool isGameActive;

    private float timePerCent;
    private float playTimeAvaliable;
    private float timeRemaining;

    private void Start()
    {
        HUD.SetActive(true);
        isGameActive = true;
        pauseDisplay.SetActive(false);

        timeRemaining = PlayerPrefs.GetFloat("timeRemaining");
        playTimeAvaliable = PlayerPrefs.GetFloat("playTimeAvaliable");
        timePerCent = timeRemaining / playTimeAvaliable;

        recoveredKeys.text = PlayerPrefs.GetInt("recoveredKeys").ToString();
    }

    private void Update()
    {
        recoveredKeys.text = PlayerPrefs.GetInt("recoveredKeys").ToString();

        if (isGameActive)
        {
            timeRemaining = PlayerPrefs.GetFloat("timeRemaining");

            timeRemaining -= Time.deltaTime;

            PlayerPrefs.SetFloat("timeRemaining", timeRemaining);

            timePerCent = timeRemaining / playTimeAvaliable;

            if (timePerCent > 1)
                timePerCent = 1;

            oxygenTimeBar.transform.localScale = new Vector3(timePerCent, oxygenTimeBar.transform.localScale.y);

            if (timeRemaining <= 0f)
                EndRound();
        }
    }

    private void PauseGame()
    {
        isGameActive = false;
        PlayerPrefs.SetFloat("timeRemaining", timeRemaining);
    }

    private void EndRound()
    {
        PauseGame();
        print("VOCE PERDEU");
    }

    public void ContinueGame()
    {
        pauseDisplay.SetActive(false);

        isGameActive = true;
    }

    public void ShowPauseMenu(bool show)
    {
        PauseGame();
        pauseDisplay.SetActive(show);
    }

    public void LoadScene(string name)
    {
        PauseGame();
        HUD.SetActive(false);
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }
}