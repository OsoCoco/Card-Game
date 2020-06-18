using UnityEngine;

public enum Language {ESPAÑOL,ENGLISH};
public class LanguageManager : MonoBehaviour
{
    public static Language language = Language.ESPAÑOL;

    public void OnLanguageChange(int i)
    {
        language = (Language)i;
    }

    public void Change()
    {
        TextMPLanguage[] texts = null;

        ButtonLanguage[] buttonLanguages = null;

        texts = FindObjectsOfType<TextMPLanguage>();
        buttonLanguages = FindObjectsOfType<ButtonLanguage>();

        for (int i = 0; i < texts.Length; i++)
            texts[i].ChangeLanguage();

        for (int i = 0; i < buttonLanguages.Length; i++)
            buttonLanguages[i].ChangeLanguage();
    }
}
