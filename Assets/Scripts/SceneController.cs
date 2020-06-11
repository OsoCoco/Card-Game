using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{
    public TMP_Text mP_Text;
    public GameObject about;
    public void OnPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnButton(string text)
    {
        mP_Text.text = text;
    }
    public void OnAbout()
    {
        this.gameObject.SetActive(false);
        about.SetActive(true);

    }
}
