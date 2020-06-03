using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    //public List<Card> deck;
    public List<Card> hand;
    public int money;

    public GameObject bet;
    public GameObject shuffle;
    public GameObject takeHand;
    //public int numberOfShuffles;

    public bool hasShuffle;
    public bool hasTakenHand;


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
