using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField]
    Unit player;
    [SerializeField]
    Unit enemy;

    [SerializeField] Text playerBet;
    [SerializeField] Text enemyBet;

    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Unit>();

        
    }

    private void LateUpdate()
    {
        playerBet.text = player.money.ToString();
        enemyBet.text = enemy.money.ToString();
    }

    public void OnReload()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExit()
    {
        Application.Quit();
    }

  
}
