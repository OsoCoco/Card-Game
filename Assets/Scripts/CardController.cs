using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public int value;
   
    [SerializeField]TurnSystem system;

    private void Start()
    {
        system = GameObject.Find("TurnSystem").GetComponent<TurnSystem>();
    }

    private void OnMouseDown()
    {
        if(system.state == GameState.PLAYERTURN)
        {
            StartCoroutine(PlayerCard());
        }
        else if(system.state == GameState.ENEMYTURN)
        {
            StartCoroutine(EnemyCard());
        }

    }
    IEnumerator PlayerCard()
    {
        Debug.Log(value);
        yield return new WaitForSeconds(1f);
        system.state = GameState.ENEMYTURN;
        system.PlayerTurn();
    }

    IEnumerator EnemyCard()
    {
        Debug.Log(value);
        yield return new WaitForSeconds(1f);
        system.state = GameState.PLAYERTURN;
        system.EnemyTurn();
    }
}
