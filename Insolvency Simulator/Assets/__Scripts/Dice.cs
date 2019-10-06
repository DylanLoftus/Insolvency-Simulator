using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    public int[] DiceValues;
    public int DiceTotal;

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
        }

        Debug.Log("Rolled: " + DiceTotal);
    }
}
