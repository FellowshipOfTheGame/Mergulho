using UnityEngine;
using UnityEngine.UI;

public class I18nTextTranslator : MonoBehaviour
{
    public string TextId;

    void Start()
    {
        var text = GetComponent<Text>();
        if (text != null)
            if(TextId == "ISOCode")
                text.text = I18n.GetLanguage();
            else
                text.text = I18n.Fields[TextId];
    }
}