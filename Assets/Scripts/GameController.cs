using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int sizeOfDecks;

    public List<Card> playerDeck;

    public List<Card> playerHand;
  
    private void Start()
    {
       for(int i = 0; i < sizeOfDecks;i++)
        {
            playerDeck.Add(new Card(i+1));
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerDeck = Shuffle(playerDeck);
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            playerHand = TakeHand(playerDeck, playerHand, 5);
        }
    }

    /// With the Fisher-Yates shuffle, first implemented on computers by Durstenfeld in 1964, 
    ///   we randomly sort elements. This is an accurate, effective shuffling method for all array types.

    public List<Card> Shuffle(List<Card> deck)
    {
        System.Random _random = new System.Random();

        Card mCard;

        int n = deck.Count;

        for(int i = 0; i < n;i++)
        {
            int r = i + (int)(_random.NextDouble() * (n - i));

            mCard = deck[r];

            deck[r] = deck[i];

            deck[i] = mCard;
        }

        return deck;

    }

    public List<Card> TakeHand(List<Card> deck,List<Card> hand,int handSize)
    {
        Queue h = new Queue();
        for (int i = 0;i < deck.Count;i++)
        {

            h.Enqueue(deck[i]);

        }

        deck.Clear();


        for (int i = 0; i <= handSize; i++)
        {
            hand.Add((Card)h.Dequeue());
        }

        for (int i =0; i < h.Count;i++)
        {
            deck.Add((Card)h.Dequeue());
        }

      return hand;
    }
}
