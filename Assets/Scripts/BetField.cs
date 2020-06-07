using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetField : MonoBehaviour
{
    [SerializeField ]CardController card;
    [SerializeField] CardController card2;

    public int index = 0;

    [SerializeField]
    Unit player;
    [SerializeField]
    Unit enemy;

    InputField field;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Unit>();
        field = GetComponent<InputField>();

        GameObject[] temp = GameObject.FindGameObjectsWithTag("PlayerCard");
        GameObject[] temp2 = GameObject.FindGameObjectsWithTag("EnemyCard");

        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].GetComponent<CardController>().index == index)
            {
                card = temp[i].GetComponent<CardController>();
                
                break;
            }

        }

        for(int i = 0; i < temp2.Length;i++)
        {
            if(temp2[i].GetComponent<CardController>().index == index)
            {
                card2 = temp2[i].GetComponent<CardController>();
                break;
            }
        }
    }

    public void OnBet(string bet)
    {
        int temp = int.Parse(bet);

        if (temp > player.money)
            temp = player.money;
        else if (player.money == 0)
        {
            temp = 0;
            field.interactable = false;
        }


        field.interactable = false;

        player.money -= temp;
        card.betValue = temp;
    }

    public void OnBetEnemy(string bet)
    {
        int temp = int.Parse(bet);

        if (temp > enemy.money)
            temp = enemy.money;
        else if (enemy.money == 0)
        {
            temp = 0;
            field.interactable = false;
        }


        field.interactable = false;

        enemy.money -= temp;
        card2.betValue = temp;
    }
}
