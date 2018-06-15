using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject HUD, pauseDisplay, keyWarning, oxygenTimeBar;
    public GameObject eventSystem, playerComponents, depthGauge;
    public Text recoveredKeys;
    public bool isGameActive;
    public float screenHeigth;

    private float timePerCent, playTimeAvaliable, timeRemaining;
    private float depthPerCent, rotation, playerY;

    private void Start()
    {
        Camera.main.GetComponent<AudioListener>().enabled = true;

        eventSystem.SetActive(true);
        playerComponents.SetActive(true);
        HUD.SetActive(true);
        pauseDisplay.SetActive(false);
        keyWarning.SetActive(false);

        isGameActive = true;

        timeRemaining = PlayerPrefs.GetFloat("timeRemaining");
        playTimeAvaliable = PlayerPrefs.GetFloat("playTimeAvaliable");
        timePerCent = timeRemaining / playTimeAvaliable;

        recoveredKeys.text = PlayerPrefs.GetInt("recoveredKeys").ToString();

        screenHeigth /= 100;
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("questionsAnswered") == PlayerPrefs.GetInt("questionsLength"))
            WinORLose("Win");
        else {
            if (isGameActive)
            {
                UpdateTimeBar();
                UpdateDepthGauge();

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

    private void UpdateDepthGauge()
    {
        playerY = PlayerPrefs.GetFloat("playerY");

       //retorna sua posicao em relacao a altura
       playerY = (screenHeigth/2) + playerY;

        //Acha a porcentagem da posicao do player em relaçao a altura da fase
        depthPerCent = playerY / screenHeigth;
        //Acha a rotacao que a agulha deve estar
        rotation = depthPerCent * 350;
        //Atualiza agulha do profundimetro
        depthGauge.transform.rotation = Quaternion.Euler(0, 0, rotation);
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

    public void LoadSceneAndDestroy(string name)
    {
        GameObject[] reload = GameObject.FindGameObjectsWithTag("Reload");
        foreach (GameObject go in reload)
            Destroy(go);
        
        SceneManager.LoadScene(name);
    }
}