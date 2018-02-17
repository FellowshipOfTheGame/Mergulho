using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject pauseDisplay;
    public GameObject oxygenTimeBar;

    private bool isRoundActive;
    private float timePerCent;
    private float playTimeAvaliable;
    private float timeRemaining;

    private void Start()
    {
        pauseDisplay.SetActive(false);

        timeRemaining = PlayerPrefs.GetFloat("timeRemaining");
        playTimeAvaliable = PlayerPrefs.GetFloat("playTimeAvaliable");
        timePerCent = timeRemaining / playTimeAvaliable;

        isRoundActive = true;
    }

    private void Update()
    {
        if (isRoundActive)
        {
            CheckKeyDown();

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

    private void ChangeRound(bool active)
    {
        isRoundActive = active;
        PlayerPrefs.SetFloat("timeRemaining", timeRemaining);
    }

    private void EndRound()
    {
        ChangeRound(false);
        print("VOCE PERDEU");
    }

    public void ShowPauseMenu(bool show)
    {
        ChangeRound(!show);
        pauseDisplay.SetActive(show);
    }

    public void LoadScene(string name)
    {
        ChangeRound(false);
        SceneManager.LoadScene(name);
    }
}