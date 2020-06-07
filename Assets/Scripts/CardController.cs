using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardController : MonoBehaviour
{
    public int value;
    public int betValue;

    public int index = 0;
    bool clicked;
   
    [SerializeField]TurnSystem system;

    private void Start()
    {
        system = GameObject.Find("TurnSystem").GetComponent<TurnSystem>();
    }

    private void OnMouseDown()
    {
        if(system.state != GameState.COMPARECARDS || system.state != GameState.PLAYERBET || system.state != GameState.ENEMYBET)
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
        yield return new WaitForSeconds(1f);

        this.gameObject.SetActive(false);
        system.state = GameState.ENEMYTURN;
        system.EnemyTurn();
    }

    IEnumerator EnemyCard()
    {
        Debug.Log(value);
        clicked = true;
        system.enemyCard = this;
        yield return new WaitForSeconds(1f);

        this.gameObject.SetActive(false);
        system.state = GameState.COMPARECARDS;
        system.CompareCards();
    }
}
