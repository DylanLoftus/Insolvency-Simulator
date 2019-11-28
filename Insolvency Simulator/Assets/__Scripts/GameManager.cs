using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int CurrentPlayerID = 0;
    public int CurrentPlayerMoney;
    public int DiceTotal;
    public ChanceCard[] chanceCards;
    public CommunityChest[] communityChests;
    public Ownership owner;
    public PropertyUI propUI;
    public PropertyOwnMonopoly propMonop;

    public Property currentProperty;

    public int NumberOfPlayers = 3;

    // To check if the dice is done rolling or not.
    public bool IsDoneRolling = false;
    // To check if the player is done moving.
    public bool IsDoneMoving = false;
    // To check if the player has finished their tile interaction.
    public bool IsDoneInteraction = false;

    public bool doubleRoll = false;

    public bool passedGo = false;

    public bool buttonPress = false;

    public bool propBuy = false;

    public bool running = true;

    float timeLeft = 60;

    // Start is called before the first frame update
    void Start()
    {
        // Get an instance of the propUI object and set it to false ( we ain't using it yet ).
        propUI = GameObject.FindObjectOfType<PropertyUI>();
        propUI.gameObject.SetActive(false);

        propMonop = GameObject.FindObjectOfType<PropertyOwnMonopoly>();
        propMonop.gameObject.SetActive(false);

        // Get an instance of the ownership class.
        owner = GameObject.FindObjectOfType<Ownership>();

        // Check the players money.
        CheckPlayerMoney();
        Debug.Log("Current player is: " + (CurrentPlayerID + 1));
    }

    // Update is called once per frame
    void Update()
    {
        // If all are true.
        if (IsDoneRolling && IsDoneMoving && IsDoneInteraction)
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
        Debug.Log("Current player is: " + (CurrentPlayerID + 1));
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
                CheckPassingGo(player);
                CheckTileType(player);
            }
        }
    }

    // Checks to see if the player has passed Go.
    public void CheckPassingGo(Player player)
    {
        if(passedGo == true)
        {
            Debug.Log("PASSED GO!!!!!!!!");
            Debug.Log("Added 200 to player " + CurrentPlayerID);
            Debug.Log("Player " + (CurrentPlayerID + 1) + " has " + player.money);
            player.money += 200;
            Debug.Log("Player " + (CurrentPlayerID + 1) + " has " + player.money);
            passedGo = false;
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
                        IsDoneInteraction = true;
                        break;
                    case "Property":
                        StartCoroutine(PropertyHandler(player, tile));
                        break;
                    case "Community":
                        CommunityChestHandler(player);
                        break;
                    case "Tax":
                        TaxHandler(player, tile);
                        break;
                    case "Rail":
                        RailRoadHandler(player, tile);
                        break;
                    case "Chance":
                        ChanceCardHandler(player);
                        break;
                    case "Jail":
                        JailHandler(player);
                        break;
                    case "Utility":
                        UtilityHandler(player, tile);
                        break;
                    case "Free Parking":
                        Debug.Log("Nothing happens here.");
                        IsDoneInteraction = true;
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
        Debug.Log("----- END TURN -----");
    }

    // Pulls a chance card and does that chance cards action.
    private void ChanceCardHandler(Player player)
    {
        int random = Random.Range(0, 7);
        ChanceCard card = chanceCards[random];

        switch (card.chanceId)
        {
            case 1:
                GameObject goTileObj = GameObject.FindGameObjectWithTag("Go");
                Tile goTile = goTileObj.GetComponent<Tile>();
                player.currentTile = goTile;
                break;
            case 2:
                // TODO: pass go mallarcy  
                GameObject nunsIslandObj = GameObject.FindGameObjectWithTag("Nun");
                Tile nunTile = nunsIslandObj.GetComponent<Tile>();
                player.currentTile = nunTile;
                break;
            case 3:
                player.money += 50;
                break;
            case 4:
                GoToJail(player);
                break;
            case 5:
                player.money -= 25 * player.houseCount;
                break;
            case 6:
                player.money -= 15;
                break;
            case 7:
                player.money += 150;
                break;
        }
        IsDoneInteraction = true;
    }

    // Pulls a community chest card and does the action.
    private void CommunityChestHandler(Player player)
    {
        int random = Random.Range(0, 8);
        CommunityChest chest = communityChests[random];

        switch (chest.chestId)
        {
            case 1:
                GameObject goTileObj = GameObject.FindGameObjectWithTag("Go");
                Tile goTile = goTileObj.GetComponent<Tile>();
                player.currentTile = goTile;
                break;
            case 2:
                player.money += 50;
                break;
            case 3:
                player.money += 200;
                break;
            case 4:
                player.money += 20;
                break;
            case 5:
                player.money -= 50;
                break;
            case 6:
                player.money -= 50;
                break;
            case 7:
                player.money -= 40 * player.houseCount;
                break;
            case 8:
                player.money += 100;
                break;
        }
        IsDoneInteraction = true;
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
    public IEnumerator PropertyHandler(Player player, Tile tile)
    {
        Debug.Log("Landed on property tile.");
        Property currentProperty = tile.GetComponent<Property>();
        SetProperty(currentProperty);
        if (currentProperty.isOwned)
        {
            Debug.Log("This property is owned.");
            int playerID = currentProperty.playerID;

            if (playerID == player.PlayerID)
            {
                int ownCount = 0;
                int propCount = 0;
                Debug.Log("You own this property.");
                // CHECK TO SEE IF MONOPOLY
                foreach(Property p in currentProperty.propertyGroup)
                {
                    propCount++;
                    if(player.PlayerID == p.playerID)
                    {
                        ownCount++;
                    }
                }

                if(ownCount == propCount)
                {
                    Debug.Log("You have a monopoly.");
                    // TODO: Allow player to buy house if he/she wants.
                    propMonop.gameObject.SetActive(true);

                    timeLeft -= Time.deltaTime;
                    while (buttonPress == false)
                    {
                        if (timeLeft < 0)
                            buttonPress = true;
                        yield return null;
                    }

                    IsDoneInteraction = true;
                }

                IsDoneInteraction = true;
            }
            else
            {

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
                IsDoneInteraction = true;
            }
        }
        else
        {
            Debug.Log("This property is not owned.");
            propUI.gameObject.SetActive(true);
            IsDoneInteraction = false;
            timeLeft -= Time.deltaTime;
            while (buttonPress == false)
            {
                IsDoneInteraction = false;
                if (timeLeft < 0)
                {
                    buttonPress = true;
                }
                IsDoneInteraction = false;
                yield return null;
            }

            if (propBuy)
            {
                BuyProperty(player, currentProperty);
            }

            if(!propBuy)
            {
                propUI.gameObject.SetActive(false);
                propBuy = false;
                buttonPress = false;
                IsDoneInteraction = true;
            }
        }
    }

    // This method buys the property the player is standing on.
    public void BuyProperty(Player player, Property property)
    {
        propUI.gameObject.SetActive(false);
        player.money -= property.propertyFaceValue;
        Debug.Log("Paid :" + property.propertyFaceValue);
        Debug.Log("You now have: " + player.money);
        property.isOwned = true;
        property.playerID = CurrentPlayerID;
        buttonPress = false;
        propBuy = false;
        IsDoneInteraction = true;
    }

    // This method handles all tax tile interactions.
    public void TaxHandler(Player player, Tile tile)
    {
        TaxTile taxTile = tile.GetComponent<TaxTile>();
        Debug.Log("You've landed on a tax tile.");
        player.money -= taxTile.taxAmmount;
        Debug.Log("You've paid 200 to the bank.");
        Debug.Log("You now have: " + player.money);
        IsDoneInteraction = true;
    }

    public void RailRoadHandler(Player player, Tile tile)
    {
        RailRoad railRoad = tile.GetComponent<RailRoad>();
        if (railRoad.isOwned)
        {
            Debug.Log("This railroad is owned.");
            int playerID = railRoad.playerID;
            int rent = railRoad.rent;

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
            IsDoneInteraction = true;
        }
        else
        {
            Debug.Log("You've landed on a rail road tile.");
            player.money -= railRoad.buyAmmount;
            railRoad.isOwned = true;
            railRoad.playerID = CurrentPlayerID;
            Debug.Log("Purchased Railroad");
            Debug.Log("You now have: " + player.money);
            IsDoneInteraction = true;
        }
    }

    // This method handles when a player is in jail.
    public void JailHandler(Player player)
    {
        Debug.Log("Just visiting :D.");
        IsDoneInteraction = true;
    }

    // This method handles all utility tile interactions.
    public void UtilityHandler(Player player, Tile tile)
    {
        UtilityTile utilityTile = tile.GetComponent<UtilityTile>();
        Debug.Log("You landed on a Utility tile.");
        player.money -= utilityTile.utilityPrice;
        Debug.Log("You paid " + utilityTile.utilityName + " " + utilityTile.utilityPrice);
        IsDoneInteraction = true;
    }

    // Sends a player to jail.
    public void GoToJail(Player player)
    {
        player.isInJail = true;
        Debug.Log("You've landed on the GO TO JAIL tile.");
        player.transform.position = GameObject.FindGameObjectWithTag("Jail").transform.position;
        GameObject jailTileObj = GameObject.FindGameObjectWithTag("Jail");
        Tile jailTile = jailTileObj.GetComponent<Tile>();
        player.currentTile = jailTile;
        Debug.Log("Moved to Jail.");
        IsDoneInteraction = true;
    }

    // Getters and setters for current property.
    public void SetProperty(Property p)
    {
        currentProperty = p;
    }

    public Property GetProperty()
    {
        return currentProperty;
    }

    // Buys a house
    public void BuyHouse()
    {
        currentProperty.houseCount++;
        currentProperty.AddHouse();
    }
}
