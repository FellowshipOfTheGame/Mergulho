using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageController : MonoBehaviour {

    public void SelectLanguage(string lang)
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("lang", lang);
        I18n.LoadLanguage();
        SceneManager.LoadScene("Persistent");
    }
}
