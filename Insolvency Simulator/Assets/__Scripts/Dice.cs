using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    // Instance variables.
    public int[] DiceValues;

    // Images of dice faces.
    public Sprite[] DiceImages;

    public GameManager theGameManager;

    // Start by initializing the DiceValues array and getting a reference to the game manager.
    private void Start()
    {
        DiceValues = new int[2];
        theGameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        
    }

    // Method that rolls the Dice

    public void RollDice()
    {
        if(theGameManager.IsDoneRolling == true)
        {
            // We already rolled.
            return;
        }

        // Set DiceTotal to 0 for now.
        theGameManager.DiceTotal = 0;

        // Run the loop for as many dices as we have and store the values.
        // Also display the sprite that corresponds with the dice roll.
        for (int i = 0; i < DiceValues.Length; i++)
        {
            // Get a random number between 1 and 7 exclusive and put it into the DiceValues array.
            DiceValues[i] = Random.Range(1, 7);

            // Add that number to DiceTotal.
            theGameManager.DiceTotal += DiceValues[i];

            // Checks to see which sprite needs to be displayed.
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

        // Rolling the dice is done at this point. Put the player's turn has not finished yet...
        theGameManager.IsDoneRolling = true;
        Debug.Log("Rolled: " + theGameManager.DiceTotal);

        theGameManager.CheckCorrectPlayer();
    }
}
