using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject pauseDisplay;
    public GameObject oxygenTimeBar;
    public Text recoveredKeys;

    private float timePerCent;
    private float playTimeAvaliable;
    private float timeRemaining;
    private bool isGameActive;

    private void Start()
    {
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

        CheckKeyDown();

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

    private void CheckKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.A))
            LoadScene("Album");
        else if (Input.GetKeyDown(KeyCode.Escape))
            ShowPauseMenu(true);
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
        //Parar animação de qualquer objeto no cenário

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
        SceneManager.LoadScene(name);
    }
}