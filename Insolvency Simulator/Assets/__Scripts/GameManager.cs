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
            CurrentPlayerID++;
            NewTurn();
        }
    }

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

    public void RollAgain()
    {
        Debug.Log("Rolled a Double! Roll again!");
        IsDoneRolling = false;
        IsDoneMoving = false;
        IsDoneInteraction = false;
    }

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
                        Debug.Log("Landed on property tile.");
                        Property currentProperty = tile.GetComponent<Property>();
                        if (currentProperty.isOwned)
                        {
                            Debug.Log("This property is owned.");
                            int playerID = currentProperty.playerID;

                            Player[] playerArray = GameObject.FindObjectsOfType<Player>();

                            foreach (Player players in playerArray)
                            {
                                if (players.PlayerID == playerID)
                                {
                                    players.money += currentProperty.propertyFaceValue;
                                    Debug.Log("Payed Player: " + players.PlayerID + 1);
                                }
                            }

                            player.money -= currentProperty.propertyFaceValue;
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
                        break;
                    case "Community":
                        Debug.Log("No implementation yet.");
                        break;
                    case "Tax":
                        Debug.Log("You've landed on a tax tile.");
                        player.money -= 200;
                        Debug.Log("You've paid 200 to the bank.");
                        Debug.Log("You now have: " + player.money);
                        break;
                    case "Railroad":
                        Debug.Log("No implementation yet.");
                        break;
                    case "Chance":
                        Debug.Log("No implementation yet.");
                        break;
                    case "Jail":
                        if (player.isInJail)
                        {
                            if(player.jailTurn == 3)
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
                        break;
                    case "Utility":
                        UtilityTile utilityTile = tile.GetComponent<UtilityTile>();
                        Debug.Log("You landed on a Utility tile.");
                        player.money -= utilityTile.utilityPrice;
                        Debug.Log("You paid " + utilityTile.utilityName + " " + utilityTile.utilityPrice);
                        break;
                    case "Free Parking":
                        Debug.Log("Nothing happens here.");
                        break;
                    case "Go To Jail":
                        Debug.Log("You've landed on the GO TO JAIL tile.");
                        player.transform.position = GameObject.FindGameObjectWithTag("Jail").transform.position;
                        Debug.Log("Moved to Jail.");
                        break;
                    case "LuxTax":
                        Debug.Log("You've landed on a luxary tax tile.");
                        player.money -= 75;
                        Debug.Log("You've paid 75 to the bank.");
                        Debug.Log("You now have: " + player.money);
                        break;
                }
            }
        }

        IsDoneInteraction = true;
    }

    // This method checks the ammount of money a player has.
    public void CheckPlayerMoney()
    {
        Player[] playerArray = GameObject.FindObjectsOfType<Player>();

        foreach (Player player in playerArray)
        {
            if (player.PlayerID == CurrentPlayerID)
            {
                CurrentPlayerMoney = player.money;
            }
        }
    }
}
