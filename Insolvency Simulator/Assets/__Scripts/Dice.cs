using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    public int[] DiceValues;
    public int DiceTotal;

    public Sprite[] DiceImages;

    public bool IsDoneRolling = false;

    private void Start()
    {
        DiceValues = new int[2];
    }

    private void Update()
    {
        
    }

    public void RollDice()
    {
        DiceTotal = 0;

        for (int i = 0; i < DiceValues.Length; i++)
        {
            DiceValues[i] = Random.Range(1, 7);
            DiceTotal += DiceValues[i];
            switch (DiceValues[i])
            {
                case 1:
                    transform.GetChild(i).GetComponent<Image>().sprite = DiceImages[0];
                    break;
                case 2:
                    transform.GetChild(i).GetComponent<Image>().sprite = DiceImages[1];
                    break;
                case 3:
                    transform.GetChild(i).GetComponent<Image>().sprite = DiceImages[2];
                    break;
                case 4:
                    transform.GetChild(i).GetComponent<Image>().sprite = DiceImages[3];
                    break;
                case 5:
                    transform.GetChild(i).GetComponent<Image>().sprite = DiceImages[4];
                    break;
                case 6:
                    transform.GetChild(i).GetComponent<Image>().sprite = DiceImages[5];
                    break;
            }
        }

        IsDoneRolling = true;
        Debug.Log("Rolled: " + DiceTotal);

        Player player;
        player = GameObject.FindObjectOfType<Player>();
        player.Move();
        //NewTurn();
    }
 
    public void NewTurn()
    {
        IsDoneRolling = false;
    }
}
