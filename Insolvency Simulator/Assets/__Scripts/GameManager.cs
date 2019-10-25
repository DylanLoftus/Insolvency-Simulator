using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int CurrentPlayerID = 0;
    public int CurrentPlayerMoney;
    public int DiceTotal;

    public int NumberOfPlayers = 3;

    // To check if the dice is done rolling or not.
    public bool IsDoneRolling = false;
    // To check if the player is done moving.
    public bool IsDoneMoving = false;
    // To check if the player has finished their tile interaction.
    public bool IsDoneInteraction = false;

    public bool doubleRoll = false;



    // Start is called before the first frame update
    void Start()
    {
        // Check the players money.
        CheckPlayerMoney();
        Debug.Log("Current player is: " + CurrentPlayerID);
    }

    // Update is called once per frame
    void Update()
    {
        // If all are true.
        if(IsDoneRolling && IsDoneMoving)
        {
            // We have finished the turn.
            // Move to the next player.
            CurrentPlayerID++;
            // Reset all values.
            NewTurn();
        }
    }

    // This method checks the players money and sets the current player money to the value assossiated with the current players turn.
    // Sets all the 'Is' values to false.
    public void NewTurn()
    {
        CheckPlayerMoney();

        if (CurrentPlayerID > 2)
        {
            CurrentPlayerID = 0;
        }
        Debug.Log("Current player is: " + CurrentPlayerID);

        IsDoneRolling = false;
        IsDoneMoving = false;
        IsDoneInteraction = false;
    }

    // This method resets all 'Is' values so the player can roll again.
    public void RollAgain()
    {
        Debug.Log("Rolled a Double! Roll again!");
        IsDoneRolling = false;
        IsDoneMoving = false;
        IsDoneInteraction = false;
        doubleRoll = false;
    }

    // This method checks to see if we're moving the correct player.
    public void CheckCorrectPlayer()
    {
        Player[] playerArray = GameObject.FindObjectsOfType<Player>();

        foreach (Player player in playerArray)
        {
            if (player.PlayerID == CurrentPlayerID)
            {
                player.Move();
                CheckTileType(player);
            }
        }
    }

    // This method checks for the type of tile a player has landed on.
    public void CheckTileType(Player player)
    {
        Tile[] tiles = GameObject.FindObjectsOfType<Tile>();

        foreach (Tile tile in tiles)
        {
            if (player.transform.position == tile.transform.position)
            {
                switch (tile.tileType)
                {
                    case "Go":
                        player.money += 200;
                        break;
                    case "Property":
                        PropertyHandler(player, tile);
                        break;
                    case "Community":
                        Debug.Log("No implementation yet.");
                        break;
                    case "Tax":
                        TaxHandler(player, tile);
                        break;
                    case "Rail":
                        Debug.Log("No implementation yet.");
                        break;
                    case "Chance":
                        Debug.Log("No implementation yet.");
                        break;
                    case "Jail":
                        JailHandler(player);
                        break;
                    case "Utility":
                        UtilityHandler(player, tile);
                        break;
                    case "Free Parking":
                        Debug.Log("Nothing happens here.");
                        break;
                    case "Go To Jail":
                        GoToJail(player);
                        break;
                    case "LuxTax":
                        TaxHandler(player, tile);
                        break;
                }
            }
        }

        IsDoneInteraction = true;
        Debug.Log("----- END TURN -----");
    }

    // This method checks the ammount of money a player has.
    public int CheckPlayerMoney()
    {
        Player[] playerArray = GameObject.FindObjectsOfType<Player>();

        foreach (Player player in playerArray)
        {
            if (player.PlayerID == CurrentPlayerID)
            {
                CurrentPlayerMoney = player.money;
            }
        }

        return CurrentPlayerMoney;
    }

    // This method handles all property interactions.
    public void PropertyHandler(Player player, Tile tile)
    {
        Debug.Log("Landed on property tile.");
        Property currentProperty = tile.GetComponent<Property>();
        if (currentProperty.isOwned)
        {
            Debug.Log("This property is owned.");
            int playerID = currentProperty.playerID;
            int rent = (currentProperty.propertyFaceValue / 100) * 20;

            Player[] playerArray = GameObject.FindObjectsOfType<Player>();

            foreach (Player players in playerArray)
            {
                if (players.PlayerID == playerID)
                {
                    int playerPreviousMoney = players.money;
                    players.money += rent;
                    Debug.Log("Payed Player: " + (players.PlayerID + 1) + " " + rent);
                    Debug.Log("Player " + (players.PlayerID + 1) + " had " + playerPreviousMoney + " and now has " + players.money);
                }
            }

            player.money -= rent;
            Debug.Log(CurrentPlayerMoney);

        }
        else
        {
            Debug.Log("This property is not owned.");
            player.money -= currentProperty.propertyFaceValue;
            Debug.Log("Paid :" + currentProperty.propertyFaceValue);
            Debug.Log("You now have: " + player.money);
            currentProperty.isOwned = true;
            currentProperty.playerID = CurrentPlayerID;
        }
    }

    // This method handles all tax tile interactions.
    public void TaxHandler(Player player, Tile tile)
    {
        TaxTile taxTile = tile.GetComponent<TaxTile>();
        Debug.Log("You've landed on a tax tile.");
        player.money -= taxTile.taxAmmount;
        Debug.Log("You've paid 200 to the bank.");
        Debug.Log("You now have: " + player.money);
    }

    // This method handles when a player is in jail.
    public void JailHandler(Player player)
    {
        if (player.isInJail)
        {
            if (player.jailTurn == 3)
            {
                player.isInJail = false;
            }

            // Do jail logic
            if (doubleRoll)
            {
                player.isInJail = false;
            }
        }
        else
        {
            Debug.Log("Just visiting :D.");
        }
    }

    // This method handles all utility tile interactions.
    public void UtilityHandler(Player player, Tile tile)
    {
        UtilityTile utilityTile = tile.GetComponent<UtilityTile>();
        Debug.Log("You landed on a Utility tile.");
        player.money -= utilityTile.utilityPrice;
        Debug.Log("You paid " + utilityTile.utilityName + " " + utilityTile.utilityPrice);
    }

    // Sends a player to jail.
    public void GoToJail(Player player)
    {
        Debug.Log("You've landed on the GO TO JAIL tile.");
        player.transform.position = GameObject.FindGameObjectWithTag("Jail").transform.position;
        Debug.Log("Moved to Jail.");
    }
}
