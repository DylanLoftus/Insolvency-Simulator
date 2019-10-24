using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Instance variables.
    public Tile StartTile;
    [SerializeField]
    private int playerNumber;
    Tile currentTile;
    Dice dice;

    // Start by putting the players at their respective start positions.
    // In this case they all get sent to the 'Go' tile.
    // Get a reference for the dice as well.
    void Start()
    {
        this.transform.position = StartTile.transform.position;
        dice = GameObject.FindObjectOfType<Dice>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Handles player movement.
    public void Move()
    {
        // Check to see if it's our turn.
        if (dice.IsDoneRolling == false)
        {
            // Do not move
            return;
        }
        
        int spacesToMove = dice.DiceTotal;

        Tile finalTile = currentTile;

        for (int i = 0; i < spacesToMove; i++)
        {
            if (currentTile == null)
            {
                finalTile = StartTile;
            }
            else
            {
                finalTile = finalTile.NextTiles[0];
            }
        }

        this.transform.position = finalTile.transform.position;

        currentTile = finalTile;

    }
}
