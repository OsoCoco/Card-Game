using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GameState {START,PLAYERTURN,ENEMYTURN,WON,LOST }
public enum TurnState {SHUFFLE,BET,END}
public class TurnSystem : MonoBehaviour
{
    public Text text;
    public GameState state;
    public TurnState turnState;

    public GameObject player;
    public GameObject enemy;

    public Transform playerDeckTransform;
    public Transform enemyDeckTransform;

    Unit playerUnit;
    Unit enemyUnit;


    public int enemyBet;
    public int playerBet;

    public Card playerCard;
    public Card enemyCard;


    // Start is called before the first frame update
    void Start()
    {
        state = GameState.START;
        StartCoroutine (SetUpGame());
    }

    IEnumerator SetUpGame()
    {
        GameObject playerGo = Instantiate(player,playerDeckTransform);
        playerUnit = playerGo.GetComponent<Unit>();


        GameObject enemyGO = Instantiate(enemy,enemyDeckTransform);
        enemyUnit = enemyGO.GetComponent<Unit>();

        yield return new WaitForSeconds(2f);

        state = GameState.PLAYERTURN;
        PlayerTurn();

    }
    #region Player
    void PlayerTurn()
    {
        text.text = "Player 1 Turn";
    }

    public void OnPlayerShuffle()
    {
        if (state != GameState.PLAYERTURN)
            return;

        Debug.Log("Turno Jugador");
        StartCoroutine(PlayerShuffle());
    }
    IEnumerator PlayerShuffle()
    {
        playerUnit.deck = Shuffle(playerUnit.deck);
        yield return new WaitForSeconds(2f);
        text.text = "Player 1 Has Shuffled";
        yield return new WaitForSeconds(2f);

        state = GameState.ENEMYTURN;
        EnemyTurn();
    }

    IEnumerator PlayerHand()
    {
        if (playerUnit.hand.Count == 0)
            playerUnit.hand = TakeHand(playerUnit.deck, playerUnit.hand, 5);
        else
            text.text = "Already got hand";

        yield return new WaitForSeconds(2f);

        text.text = "Player 1 Has Taken Hand";

    }
    public void OnPlayerHand()
    {
        if (state != GameState.PLAYERTURN)
            return;

        Debug.Log("Turno Jugador");
        StartCoroutine(PlayerHand());
    }


    #endregion

    #region Enemy
    void EnemyTurn()
    {
        text.text = "Player 2 Turn";
    }
    public void OnEnemyShuffle()
    {
        if (state != GameState.ENEMYTURN)
            return;

        Debug.Log("Enemy Turn");
        StartCoroutine(EnemyShuffle());

    }

    public void OnEnemyHand()
    {
        if (state != GameState.ENEMYTURN)
            return;

        StartCoroutine(EnemyHand());
    }
    IEnumerator EnemyShuffle()
    {
        enemyUnit.deck = Shuffle(enemyUnit.deck);
        yield return new WaitForSeconds(2f);

        text.text = "Player 2 Has Shuffled";
        //yield return new WaitForSeconds(2f);
        //state = GameState.PLAYERTURN;
        //PlayerTurn();
    }

    IEnumerator EnemyHand()
    {
        if (enemyUnit.hand.Count == 0)
            enemyUnit.hand = TakeHand(enemyUnit.deck, enemyUnit.hand, 5);
        else
            text.text = "Already got hand";

        yield return new WaitForSeconds(2f);
        text.text = "Player 2 Has Taken Hand";
    }
    IEnumerator EnemyCard()
    {
        enemyCard = TurnCard(enemyUnit.hand);

        yield return new WaitForSeconds(2f);

        text.text = "Player 2 Has Taken Hand";

    }

    public void OnEnemyCard()
    {
        if (state != GameState.ENEMYTURN)
            return;

        StartCoroutine(EnemyCard());
    }
    #endregion
    
    public Card TurnCard(List<Card> hand)
    {
        if (hand.Count == 0)
            return null;

        Card turnCard;

        turnCard = hand[0];
        hand.Remove(turnCard);

        return turnCard;
        
    }
    public List<Card> Shuffle(List<Card> deck)
    {
        System.Random _random = new System.Random();

        Card mCard;

        int n = deck.Count;

        for (int i = 0; i < n; i++)
        {
            int r = i + (int)(_random.NextDouble() * (n - i));

            mCard = deck[r];

            deck[r] = deck[i];

            deck[i] = mCard;
        }

        return deck;

    }
    public List<Card> TakeHand(List<Card> deck, List<Card> hand, int handSize)
    {
        Queue h = new Queue();
        for (int i = 0; i < deck.Count; i++)
        {

            h.Enqueue(deck[i]);

        }

        deck.Clear();


        for (int i = 0; i < handSize; i++)
        {
            hand.Add((Card)h.Dequeue());
        }

        while (h.Count > 0)
        {
            deck.Add((Card)h.Dequeue());
        }

        return hand;
    }
  
}
