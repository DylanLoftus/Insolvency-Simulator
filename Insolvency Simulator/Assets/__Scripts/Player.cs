using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Instance variables.
    public Tile StartTile;
    public int PlayerID;
    public bool isInJail = false;
    public int jailTurn = 0;
    public int money = 1500;
    private int doubleCount = 0;
    Tile currentTile;
    GameManager theGameManager;

    // Start by putting the players at their respective start positions.
    // In this case they all get sent to the 'Go' tile.
    // Get a reference for the theGameManager as well.
    void Start()
    {
        this.transform.position = StartTile.transform.position;
        theGameManager = GameObject.FindObjectOfType<GameManager>();
        currentTile = StartTile;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    // Handles player movement.
    public void Move()
    {

        // Check to see if it's our turn.
        if (theGameManager.CurrentPlayerID != PlayerID)
        {
            // It's not our turn yet!
            return;
        }

        if (theGameManager.IsDoneRolling == false)
        {
            // Do not move
            return;
        }

        if(theGameManager.IsDoneMoving == true)
        {
            // We've already done a move
            return;
        }

        // Check to see if in jail.
        if (isInJail)
        {
            if(theGameManager.doubleRoll == true || jailTurn == 3)
            {
                MoveSpaces();
            }
            else
            {
                jailTurn++;
                theGameManager.IsDoneMoving = true;
            }
        }
        else
        {
            MoveSpaces();
            if(theGameManager.doubleRoll == true)
            {
                if (doubleCount == 3)
                {
                    this.transform.position = GameObject.FindGameObjectWithTag("Jail").transform.position;
                    isInJail = true;
                }
                else
                {
                    doubleCount++;
                    theGameManager.RollAgain();
                }
            }
        }
    }

    private void MoveSpaces()
    {
        // How many space we have to move.
        int spacesToMove = theGameManager.DiceTotal;
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
        theGameManager.IsDoneMoving = true;
    }
}
