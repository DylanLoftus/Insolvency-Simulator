using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Instance variables.
    public Tile StartTile;
    public int PlayerID = -1;
    public bool isInJail = false;
    public int jailTurn = 0;
    public int money = 1500;
    public int houseCount = 0;
    private int doubleCount = 0;
    public Tile currentTile;
    public Tile finalTile;
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
            Debug.Log("You are IN JAIL");

            if(theGameManager.doubleRoll == true || jailTurn == 3)
            {
                isInJail = false;
                jailTurn = 0;
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
                    doubleCount = 0;
                    theGameManager.GoToJail(this);
                }
                else
                {
                    doubleCount++;
                    theGameManager.RollAgain();
                }
            }

            theGameManager.doubleRoll = false;
        }
    }

    private void MoveSpaces()
    {
        // How many space we have to move.
        int spacesToMove = theGameManager.DiceTotal;
        finalTile = currentTile;

        for (int i = 0; i < spacesToMove; i++)
        {

            if (currentTile == null)
            {
                finalTile = StartTile;
            }
            else
            {
                finalTile = finalTile.NextTiles[0];
                if(finalTile.tileType == "Go")
                {
                    Debug.Log("Passing go or Landed on go?");
                    theGameManager.passedGo = true;
                }
            }
        }

        this.transform.position = finalTile.transform.position;

        currentTile = finalTile;
        theGameManager.IsDoneMoving = true;
    }
}
