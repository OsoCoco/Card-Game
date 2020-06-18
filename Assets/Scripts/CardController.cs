using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardController : MonoBehaviour
{
    public int value;
    public int betValue;

    public int index = 0;
    bool clicked;

    Text playerValue;
    Text enemyValue;
    [SerializeField] AudioClip woosh;
    AudioSource source;
    [SerializeField] TurnSystem system;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(woosh);
        system = GameObject.Find("TurnSystem").GetComponent<TurnSystem>();
        playerValue = GameObject.Find("PlayerCardValue").GetComponent<Text>();
        enemyValue = GameObject.Find("EnemyCardValue").GetComponent<Text>();

    }

    private void OnMouseDown()
    {
        if (system.state != GameState.COMPARECARDS || system.state != GameState.PLAYERBET || system.state != GameState.ENEMYBET)
        {
            if (system.state == GameState.PLAYERTURN)
            {
                if (gameObject.CompareTag("EnemyCard") || clicked)
                    return;


                StartCoroutine(PlayerCard());
            }
            else if (system.state == GameState.ENEMYTURN || clicked)
            {
                if (gameObject.CompareTag("PlayerCard"))
                    return;
                StartCoroutine(EnemyCard());
            }
        }



    }
    IEnumerator PlayerCard()
    {
        Debug.Log(value);
        clicked = true;
        system.playerCard = this;
        playerValue.text = value.ToString();
        yield return new WaitForSeconds(2f);
        //playerValue.text = null;
        this.gameObject.SetActive(false);

        if (!system.enemyUnit.hasBet)
        {
            system.state = GameState.ENEMYBET;
            system.EnemeyBetTurn();

        }
        else
        {
            if (system.enemyCard != null)
            {
                if(system.playerCard != null)
                {
                    system.state = GameState.COMPARECARDS;
                    system.CompareCards();
                }
                else
                {
                    system.state = GameState.PLAYERTURN;
                    system.PlayerTurn();
                }
                
            }
            else
            {
                system.state = GameState.ENEMYTURN;
                system.EnemyTurn();
            }

        }
        
    }

    IEnumerator EnemyCard()
    {
        Debug.Log(value);
        clicked = true;
        system.enemyCard = this;
        enemyValue.text = value.ToString();
        yield return new WaitForSeconds(2f);
        //enemyValue.text = null;
        this.gameObject.SetActive(false);
        system.state = GameState.COMPARECARDS;

        if (!system.playerUnit.hasBet)
        {
            system.state = GameState.PLAYERBET;
            system.PlayerBetTurn();
        }
        else
        {
            if (system.playerCard != null)
            {
                if(system.enemyCard != null)
                {
                    system.state = GameState.COMPARECARDS;
                    system.CompareCards();
                }
                else
                {
                    system.state = GameState.ENEMYTURN;
                    system.EnemyTurn();
                }
               
            }
            else
            {
                system.state = GameState.PLAYERTURN;
                system.PlayerTurn();

            }
        }


    }
}
