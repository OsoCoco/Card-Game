using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GameState {START,PLAYERTURN,ENEMYTURN,COMPARECARDS,PLAYERBET,ENEMYBET,WON,LOST }
public enum TurnState {SHUFFLE,BET,END}
public class TurnSystem : MonoBehaviour
{

    public List<Card> deck;
    public Text text;
    public GameState state;
    public TurnState turnState;

    public GameObject player;
    public GameObject enemy;
    public GameObject playerCardprefab;
    public GameObject enemyCardprefab;
    public GameObject playerFireworks;
    public GameObject enemyFireworks;

    public Transform playerDeckTransform;
    public Transform enemyDeckTransform;

    public Unit playerUnit;
    public Unit enemyUnit;


    public int enemyBet;
    public int playerBet;


    public CardController playerCard;
    public CardController enemyCard;

    public int roundCounter;

    public InputField[] playerBetFields;
    public InputField[] enemyBetFields;
    
  


    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 50; i++)
        {
            deck.Add(new Card(i + 1));
        }

        deck = Shuffle(deck);


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

        for(int i  = 0; i < playerBetFields.Length;i++)
        {
            playerBetFields[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < enemyBetFields.Length; i++)
        {
            enemyBetFields[i].gameObject.SetActive(false);
        }




        yield return new WaitForSeconds(2f);

        state = GameState.PLAYERTURN;
        PlayerTurn();

    }
    #region Player
    public void PlayerTurn()
    {
       
        text.text = "Player 1 Turn";

        enemyUnit.shuffle.SetActive(false);
        enemyUnit.bet.SetActive(false);
        enemyUnit.takeHand.SetActive(false);

        if (!playerUnit.hasShuffle)
            playerUnit.shuffle.SetActive(true);

        if(!playerUnit.hasTakenHand)
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
        playerUnit.shuffle.SetActive(false);
        playerUnit.hasShuffle = true;

        yield return new WaitForSeconds(2f);
        text.text = "Player 1 Has Shuffled";
        yield return new WaitForSeconds(2f);

        if(!enemyUnit.hasTakenHand)
        {
            state = GameState.ENEMYTURN;
            EnemyTurn();
        }

        else
        {
            state = GameState.ENEMYBET;
            EnemeyBetTurn();
        }
       
    }

    IEnumerator PlayerHand()
    {
        float x = -3.6f;
        float y = -3;

        if (playerUnit.hand.Count == 0)
            playerUnit.hand = playerUnit.TakeHand(deck, playerUnit.hand, 5);
        else
            text.text = "Already got hand";

        playerUnit.takeHand.SetActive(false);
        playerUnit.shuffle.SetActive(false);
        playerUnit.hasTakenHand = true;
        playerUnit.hasShuffle = true;

        yield return new WaitForSeconds(2f);

        for(int i = 0; i < playerUnit.hand.Count;i++)
        {
            
            yield return new WaitForSeconds(1f);
            playerBetFields[i].gameObject.SetActive(true);
            playerBetFields[i].interactable = false;
            GameObject temp = Instantiate(playerCardprefab,new Vector2(x,y),Quaternion.identity);
            temp.GetComponent<CardController>().value = playerUnit.hand[i].value;
            temp.GetComponent<CardController>().index += i;
            x += 1.8f;
        }

        yield return new WaitForSeconds(2f);

        text.text = "Player 1 Has Taken Hand";

        yield return new WaitForSeconds(2f);

        if(enemyUnit.hasTakenHand)
        {
            if(enemyUnit.hasBet)
            {
                state = GameState.ENEMYTURN;
                EnemyTurn();
            }
            else
            {
                state = GameState.ENEMYBET;
                EnemeyBetTurn();
            }
        }
        else
        {
             state = GameState.ENEMYTURN;
             EnemyTurn();
        }

    }
    public void OnPlayerHand()
    {
        if (state != GameState.PLAYERTURN)
            return;

        Debug.Log("Turno Jugador");
        StartCoroutine(PlayerHand());
    }


    public void PlayerBetTurn()
    {
        text.text = "PLAYER 1 BET TURN";

        for (int i = 0; i < playerBetFields.Length; i++)
            playerBetFields[i].interactable = true;

        playerUnit.bet.SetActive(true);
 
    }

    public void OnPlayerBet()
    {
        if (state != GameState.PLAYERBET)
            return;

        StartCoroutine(PlayerBet());
    }

    IEnumerator PlayerBet()
    {
        for (int i = 0; i < playerBetFields.Length; i++)
        {
            playerBetFields[i].interactable = false;
            yield return new WaitForSeconds(0.5f);
            playerBetFields[i].gameObject.SetActive(false);
        }
        text.text = "Player 1 Has Placed Bets";
        playerUnit.hasBet = true;
        yield return new WaitForSeconds(2f);

        
        playerUnit.bet.SetActive(false);

        if(enemyUnit.hasTakenHand)
        {
            if(!enemyUnit.hasBet)
            {
                state = GameState.ENEMYBET;
                EnemeyBetTurn();
            }
            else
            {
              if(enemyCard!=null)
                {
                    if(playerCard !=null)
                    {
                        state = GameState.COMPARECARDS;
                        CompareCards();
                    }
                    else
                    {
                        state = GameState.PLAYERTURN;
                        PlayerTurn();
                    }
                }
                else
                {
                    state = GameState.ENEMYTURN;
                    EnemyTurn();
                }


            }
        }
        else
        {
            state = GameState.ENEMYTURN;
            EnemyTurn();
        }
       
    }

    #endregion

    #region Enemy
    public void EnemyTurn()
    {
        text.text = "Player 2 Turn";

        playerUnit.shuffle.SetActive(false);
        playerUnit.bet.SetActive(false);
        playerUnit.takeHand.SetActive(false);

        if (!enemyUnit.hasShuffle)
            enemyUnit.shuffle.SetActive(true);

        if (!enemyUnit.hasTakenHand)
            enemyUnit.takeHand.SetActive(true);
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

        enemyUnit.shuffle.SetActive(false);
        enemyUnit.hasShuffle = true;

        yield return new WaitForSeconds(2f);

        text.text = "Player 2 Has Shuffled";
        yield return new WaitForSeconds(2f);

        if (!playerUnit.hasTakenHand )
        {
            state = GameState.PLAYERTURN;
            PlayerTurn();
        }
        else
        {
            state = GameState.PLAYERBET;
            PlayerBetTurn();
        }
       
        
    }

    IEnumerator EnemyHand()
    {
        float x = -3.6f;
        float y = 3;
        if (enemyUnit.hand != null)
            enemyUnit.hand = enemyUnit.TakeHand(deck, enemyUnit.hand, 5);
        else
            text.text = "Already got hand";

        enemyUnit.takeHand.SetActive(false);
        enemyUnit.shuffle.SetActive(false);
        enemyUnit.hasShuffle = true;
        enemyUnit.hasTakenHand = true;

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < enemyUnit.hand.Count; i++)
        {

            yield return new WaitForSeconds(1f);
            GameObject temp = Instantiate(enemyCardprefab, new Vector2(x, y), Quaternion.identity);
            enemyBetFields[i].gameObject.SetActive(true);
            enemyBetFields[i].interactable = false;
            temp.GetComponent<CardController>().value = enemyUnit.hand[i].value;
            temp.GetComponent<CardController>().index += i;
            x += 1.8f;
        }

        yield return new WaitForSeconds(2f);
        text.text = "Player 2 Has Taken Hand";
        yield return new WaitForSeconds(2f);

        if(playerUnit.hasTakenHand)
        {
            if(playerUnit.hasBet)
            {
                state = GameState.PLAYERTURN;
                PlayerTurn();
            }
            else
            {
                state = GameState.PLAYERBET;
                PlayerBetTurn();

            }

        }
        else
        {
            state = GameState.PLAYERTURN;
            PlayerTurn();
        }
    }

    public void EnemeyBetTurn()
    {
        text.text = "PLAYER 2 BET TURN";

        for (int i = 0; i < playerBetFields.Length; i++)
            enemyBetFields[i].interactable = true;

        enemyUnit.bet.SetActive(true);

    }
    IEnumerator EnemyBet()
    {
        for (int i = 0; i < enemyBetFields.Length; i++)
        {
            enemyBetFields[i].interactable = false;
            yield return new WaitForSeconds(0.5f);
            enemyBetFields[i].gameObject.SetActive(false);
        }
        text.text = "Player 2 Has Placed Bets";
        enemyUnit.hasBet = true;
        yield return new WaitForSeconds(2f);
        enemyUnit.bet.SetActive(false);


        if (playerUnit.hasTakenHand)
        {
            if (!playerUnit.hasBet)
            {
                state = GameState.PLAYERBET;
                PlayerBetTurn();
            }
            else
            {
                if (playerCard != null)
                {
                    if (enemyCard != null)
                    {
                        state = GameState.COMPARECARDS;
                        CompareCards();
                    }
                    else
                    {
                        state = GameState.ENEMYTURN;
                        EnemyTurn();
                    }
                }
                else
                {
                    state = GameState.PLAYERTURN;
                    PlayerTurn();
                }


            }
        }
        else
        {
            state = GameState.PLAYERTURN;
            PlayerTurn();
        }

    }

    public void OnEnemyBet()
    {
        if (state != GameState.ENEMYBET)
            return;
        StartCoroutine(EnemyBet());
    }

    #endregion

    public void CompareCards()
    {
        if (state != GameState.COMPARECARDS)
            return;

        if (playerCard != null && enemyCard != null)
          StartCoroutine(Compare());

    }

    IEnumerator Compare()
    {
        if(playerCard.value > enemyCard.value)
        {
            text.text = "JUGADOR 1 GANA ESTA RONDA";
            playerUnit.money += playerCard.betValue + enemyCard.betValue;
            Debug.Log("JUGADOR 1 GANA ESTA RONDA");
            playerFireworks.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            playerFireworks.SetActive(false);
            roundCounter += 1;
            state = GameState.PLAYERTURN;
            PlayerTurn();
        }
        else
        {
            text.text = "JUGADOR 2 GANA ESTA RONDA";
            enemyUnit.money += playerCard.betValue + enemyCard.betValue;
            enemyFireworks.SetActive(true);
            Debug.Log("JUGADOR 2 GANA ESTA RONDA");

            yield return new WaitForSeconds(1.5f);
            enemyFireworks.SetActive(false);
            roundCounter += 1;
            state = GameState.PLAYERTURN;
            PlayerTurn();
        }
        playerCard = null;
        enemyCard = null;

        WinCondition();

    }


    void WinCondition()
    {
        GameObject[] temp;
        GameObject[] temp2;

        temp = GameObject.FindGameObjectsWithTag("PlayerCard");
        temp2 = GameObject.FindGameObjectsWithTag("EnemyCard");
        
        if (temp.Length != 0 && temp2.Length != 0)
        {
            return;
        }
        else
        {
            if(playerUnit.money > enemyUnit.money)
            {
                text.text = "PLAYER 1 WON";
                playerFireworks.SetActive(true);
            }
            else
            {
                text.text = "PLAYER 2 WON";
                enemyFireworks.SetActive(true);
            }
        }
    }

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
