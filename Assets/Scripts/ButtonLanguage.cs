using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLanguage : MonoBehaviour
{
    Language language;
    public List<Language> languages;
    public List<Sprite> botones;

    public Dictionary<Language, Sprite> languageButton;
    Image image;
    private void Awake()
    {
        int lenght = languages.Count;
        languageButton = new Dictionary<Language, Sprite>(lenght);


        for (int i = 0; i < lenght; i++)
        {
            languageButton.Add(languages[i], botones[i]);
        }
       

    }
    private void Start()
    {
        image = GetComponent<Image>();
        language = LanguageManager.language;

        image.sprite = languageButton[language];
    }

    public void ChangeLanguage()
    {
        
            language = LanguageManager.language;

            image.sprite = languageButton[language];

    }
}
