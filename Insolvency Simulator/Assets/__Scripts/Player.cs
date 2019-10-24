using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Instance variables.
    public Tile StartTile;
    public int PlayerID;
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
            Debug.Log(theGameManager.CurrentPlayerID);
            Debug.Log(PlayerID);
            Debug.Log("Not my turn");
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

        Debug.Log("Beginning to move player " + theGameManager.CurrentPlayerID);


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
        Debug.Log("Moved player " + theGameManager.CurrentPlayerID);
    }
}
