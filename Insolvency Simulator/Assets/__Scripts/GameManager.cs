using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int CurrentPlayerID = 0;
    public int DiceTotal;

    public int NumberOfPlayers = 3;

    // To check if the dice is done rolling or not.
    public bool IsDoneRolling = false;
    // To check if the player is done moving.
    public bool IsDoneMoving = false;
    // To check if the player has finished their tile interaction.
    // NOT IN USE YET.
    //public bool IsDoneInteraction = false;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Current player is: " + CurrentPlayerID);
    }

    // Update is called once per frame
    void Update()
    {
        // If all are true.
        if(IsDoneRolling && IsDoneMoving)
        {
            Debug.Log("Turn completed!");
            // We have finished the turn.
            CurrentPlayerID++;
            NewTurn();
        }
    }

    public void NewTurn()
    {
        if (CurrentPlayerID > 2)
        {
            CurrentPlayerID = 0;
        }
        Debug.Log("Current player is: " + CurrentPlayerID);

        IsDoneRolling = false;
        IsDoneMoving = false;
        //IsDoneInteraction = false;
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
                        Property currentProperty = tile.GetComponent<Property>();
                        if (currentProperty.isOwned)
                        {
                            int playerID = currentProperty.playerID;

                            Player[] playerArray = GameObject.FindObjectsOfType<Player>();

                            foreach (Player players in playerArray)
                            {
                                if (players.PlayerID == playerID)
                                {
                                    players.money += currentProperty.propertyFaceValue;
                                }
                            }

                            player.money -= currentProperty.propertyFaceValue;

                        }
                        else
                        {
                            player.money -= currentProperty.propertyFaceValue;
                            currentProperty.isOwned = true;
                            currentProperty.playerID = CurrentPlayerID;
                        }
                        break;
                    case "Community":
                        break;
                    case "Tax":
                        player.money -= 200;
                        break;
                    case "Railroad":
                        break;
                    case "Chance":
                        break;
                    case "Jail":
                        break;
                    case "Utility":
                        break;
                    case "Free Parking":
                        break;
                    case "Go To Jail":
                        player.transform.position = GameObject.FindGameObjectWithTag("Jail").transform.position;
                        break;
                    case "LuxTax":
                        player.money -= 75;
                        break;
                }
            }
        }
    }
}
