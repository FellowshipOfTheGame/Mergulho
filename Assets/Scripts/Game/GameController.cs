using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject HUD, keyWarning, oxygenTimeBar, eventSystem, playerComponents, depthGauge;
    public Text recoveredKeys;
    public bool isGameActive;
    public float screenHeigth;

    private float timePerCent, playTimeAvaliable, timeRemaining;
    private float depthPerCent, rotation, playerY;
    private int keyWarnings;

    private void Start()
    {
        Camera.main.GetComponent<AudioListener>().enabled = true;

        eventSystem.SetActive(true);
        playerComponents.SetActive(true);
        HUD.SetActive(true);
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
        // Verifica se o jgoador respondeu todas as perguntas
        if (PlayerPrefs.GetInt("questionsAnswered") == PlayerPrefs.GetInt("questionsLength"))
            WinORLose("Win");
        else if (isGameActive)
        {
            UpdateKeys();
            UpdateDepthGauge();
            UpdateTimeBar();
        }
    }

    public void WinORLose(string sceneName)
    {
        PauseGame(true);

        SceneManager.LoadScene(sceneName);
    }

    private void UpdateTimeBar()
    {
        timeRemaining = PlayerPrefs.GetFloat("timeRemaining");
        
        //se o mergulhador abaixo de 10 metros, o consumo de ar é dobrado
        if (rotation <= 270)
            timeRemaining -= Time.deltaTime * 2;
        else
            timeRemaining -= Time.deltaTime;

        // Atualiza tempo restante
        PlayerPrefs.SetFloat("timeRemaining", timeRemaining);

        // Transforma o tempo restante em porcentagem
        timePerCent = timeRemaining / playTimeAvaliable;
        // Nao deixa a porcentagem de tempo passar de 100%
        if (timePerCent > 1)
            timePerCent = 1;

        oxygenTimeBar.transform.localScale = new Vector3(timePerCent, oxygenTimeBar.transform.localScale.y);

        // Checa se o tempo acabou
        if (timeRemaining <= 0f)
            WinORLose("Lose");
    }

    private void UpdateDepthGauge()
    {
        playerY = PlayerPrefs.GetFloat("playerY") + 10;
        //retorna sua posicao em relacao a altura
        playerY = (screenHeigth/2) + playerY;
        //Acha a porcentagem da posicao do player em relaçao a altura da fase
        depthPerCent = playerY / screenHeigth;
        //Acha a rotacao que a agulha deve estar
        rotation = depthPerCent * 350;
        //Atualiza agulha do profundimetro
        depthGauge.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    private void UpdateKeys()
    {
        recoveredKeys.text = PlayerPrefs.GetInt("recoveredKeys").ToString();

        if (PlayerPrefs.GetInt("keyWarningTimes") > 0  && PlayerPrefs.GetInt("clickChestWKey") == 1)
        {
            PlayerPrefs.SetInt("clickChestWKey", 0);
            keyWarning.SetActive(true);
            PauseGame(true);
        }
    }

    public void PauseGame(bool pause)
    {
        isGameActive = !pause;
        PlayerPrefs.SetFloat("timeRemaining", timeRemaining);
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