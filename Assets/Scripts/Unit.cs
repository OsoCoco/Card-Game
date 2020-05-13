using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public List<Card> deck;
    public List<Card> hand;
    public int money;

    private void Awake()
    {
        for(int i = 0;i < 51; i ++)
        {
            deck.Add(new Card(Random.Range(1,51)));
        }
    }
   

   
}
