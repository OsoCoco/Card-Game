using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextMPLanguage : MonoBehaviour
{
    Language language;
    public List<Language> languages;
    public List<string> text;

    public Dictionary<Language, string> languageText;
    TextMeshProUGUI textT;

    private void Awake()
    {
        int lenght = languages.Count;
        languageText = new Dictionary<Language, string>(lenght);

        for(int i = 0;i < lenght;i++)
        {
            languageText.Add(languages[i], text[i]);
        }
    }
    private void Start()
    {
        textT = GetComponent<TextMeshProUGUI>();
        language = LanguageManager.language;

        textT.text = languageText[language];
    }

    public void ChangeLanguage()
    {
        language = LanguageManager.language;
        textT.text = languageText[language];
    }
}
