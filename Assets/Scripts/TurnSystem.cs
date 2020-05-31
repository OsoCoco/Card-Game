using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GameState {START,PLAYERTURN,ENEMYTURN,WON,LOST }
public enum TurnState {SHUFFLE,BET,END}
public class TurnSystem : MonoBehaviour
{

    public List<Card> deck;
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


    private void Awake()
    {
    




        for (int i = 0; i < 50; i++)
        {
            deck.Add(new Card(i+1));
        }

        deck = Shuffle(deck);

    }
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


        playerUnit.shuffle = GameObject.FindGameObjectWithTag("PlayerShuffle");
        playerUnit.bet = GameObject.FindGameObjectWithTag("PlayerBet");
        playerUnit.takeHand = GameObject.FindGameObjectWithTag("PlayerTakeHand");

        enemyUnit.shuffle = GameObject.FindGameObjectWithTag("EnemyShuffle");
        enemyUnit.bet = GameObject.FindGameObjectWithTag("EnemyBet");
        enemyUnit.takeHand = GameObject.FindGameObjectWithTag("EnemyTakeHand");

        playerUnit.shuffle.SetActive(false);
        playerUnit.bet.SetActive(false);
        playerUnit.takeHand.SetActive(false);

        enemyUnit.shuffle.SetActive(false);
        enemyUnit.bet.SetActive(false);
        enemyUnit.takeHand.SetActive(false);



     
        yield return new WaitForSeconds(2f);

        state = GameState.PLAYERTURN;
        PlayerTurn();

    }
    #region Player
    void PlayerTurn()
    {
        text.text = "Player 1 Turn";

        playerUnit.shuffle.SetActive(true);
        playerUnit.takeHand.SetActive(true);
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
        deck = playerUnit. Shuffle(deck);
        playerUnit.shuffle.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);
        text.text = "Player 1 Has Shuffled";
        yield return new WaitForSeconds(2f);


        state = GameState.ENEMYTURN;
        EnemyTurn();
    }

    IEnumerator PlayerHand()
    {
        if (playerUnit.hand.Count == 0)
            playerUnit.hand = playerUnit.TakeHand(deck, playerUnit.hand, 5);
        else
            text.text = "Already got hand";

        playerUnit.takeHand.SetActive(false);

        yield return new WaitForSeconds(2f);

        text.text = "Player 1 Has Taken Hand";

        yield return new WaitForSeconds(2f);


        state = GameState.ENEMYTURN;
        EnemyTurn();

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
        deck = enemyUnit.Shuffle(deck);
        yield return new WaitForSeconds(2f);

        text.text = "Player 2 Has Shuffled";
        yield return new WaitForSeconds(2f);
        state = GameState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator EnemyHand()
    {
        if (enemyUnit.hand != null)
            enemyUnit.hand = enemyUnit.TakeHand(deck, enemyUnit.hand, 5);
        else
            text.text = "Already got hand";

        yield return new WaitForSeconds(2f);
        text.text = "Player 2 Has Taken Hand";
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

}
