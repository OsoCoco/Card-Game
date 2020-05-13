using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {START,PLAYERTURN,ENEMYTURN,WON,LOST }
public enum TurnState {SHUFFLE,BET,END}
public class TurnSystem : MonoBehaviour
{
    public GameState state;
    public TurnState turnState;

    public GameObject player;
    public GameObject enemy;

    public Transform playerDeckTransform;
    public Transform enemyDeckTransform;

    Unit playerUnit;
    Unit enemyUnit;

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
        enemyUnit = playerGo.GetComponent<Unit>();

        yield return new WaitForSeconds(2f);

        state = GameState.PLAYERTURN;
        PlayerTurn();

    }

   void  PlayerTurn()
    {
        
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


        for (int i = 0; i <= handSize; i++)
        {
            hand.Add((Card)h.Dequeue());
        }

        while (h.Count > 0)
        {
            deck.Add((Card)h.Dequeue());
        }

        return hand;
    }

    void OnShufflePlayerButton()
    {
        if (state != GameState.PLAYERTURN)
            return;

        playerUnit.deck = Shuffle(playerUnit.deck);
    }
}
