using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetField : MonoBehaviour
{
    [SerializeField ]CardController card;
    public int index = 0;

    [SerializeField]
    Unit player;
    [SerializeField]
    Unit enemy;
   

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Unit>();

        GameObject[] temp = GameObject.FindGameObjectsWithTag("PlayerCard");

        for(int i = 0; i < temp.Length; i++)
        {
            if (temp[i].GetComponent<CardController>().index == index)
            {
                card = temp[i].GetComponent<CardController>();
                return;
            }
        }
    }

    public void OnBet(string bet)
    {
        int temp = int.Parse(bet);

        player.money -= temp;
        card.betValue = temp;
    }
}
