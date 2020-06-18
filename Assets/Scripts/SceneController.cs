using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{
    public TMP_Text mP_Text;
    public GameObject about;
    [SerializeField] AudioSource source;

    TextMPLanguage mPLanguage;

    Language language;

    private void Start()
    {
        mPLanguage = mP_Text.GetComponent<TextMPLanguage>();
    }
    public void OnPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnButtonEsp(string text)
    {
        language = LanguageManager.language;

        if (language != Language.ESPAÑOL)
            return;
        mPLanguage.languageText[language] = text;
        mPLanguage.ChangeLanguage();
    }
    public void OnButtonEn(string text)
    {
        language = LanguageManager.language;

        if (language != Language.ENGLISH)
            return;
        mPLanguage.languageText[language] = text;
        mPLanguage.ChangeLanguage();
    }
    public void OnButton(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
    public void OnAbout()
    {
        this.gameObject.SetActive(false);
        about.SetActive(true);

    }
    
}
