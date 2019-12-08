using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
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
    public PropertyOwnedUI propOwnedUi;
    public PassGoUI passGoUi;
    public BuyRailRoadUI buyRail;
    public RailOwnedUI railOwned;
    public ChanceCardUI chanceCardUI;
    public CommunityChestUI commUI;
    public UtilityUI utilUI;
    public TaxUI taxUI;
    public JailLandedUI jailLanded;
    public GoToJailUI goToJailUI;
    public InJailUI inJailUI;
    public DoubleRollUI doubleRollUI;

    public Property currentProperty;

    public PauseMenu menu;

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
    public bool noButtonPress = false;

    public bool propBuy = false;

    public bool running = true;

    public int playerLoseID;
    public int playerLoseCount = 0;

    float timeLeft = 60;

    // Start is called before the first frame update
    void Start()
    {
        // Get an instance of the propUI object and set it to false ( we ain't using it yet ).
        propUI = GameObject.FindObjectOfType<PropertyUI>();
        propUI.gameObject.SetActive(false);

        // Get an instance of the buyRail object and set it to false ( we ain't using it yet ).
        buyRail = GameObject.FindObjectOfType<BuyRailRoadUI>();
        buyRail.gameObject.SetActive(false);

        // Get an instance of the railOwnedUI object and set it to false.
        railOwned = GameObject.FindObjectOfType<RailOwnedUI>();
        railOwned.gameObject.SetActive(false);

        // Get an instance of the ChanceCardUI object and set it to false.
        chanceCardUI = GameObject.FindObjectOfType<ChanceCardUI>();
        chanceCardUI.gameObject.SetActive(false);

        // Get an instance of the CommunityChestUI object and set it to false.
        commUI = GameObject.FindObjectOfType<CommunityChestUI>();
        commUI.gameObject.SetActive(false);

        // Get instance of property monopoly UI.
        propMonop = GameObject.FindObjectOfType<PropertyOwnMonopoly>();
        propMonop.gameObject.SetActive(false);

        // Get instance of property owned UI.
        propOwnedUi = GameObject.FindObjectOfType<PropertyOwnedUI>();
        propOwnedUi.gameObject.SetActive(false);

        // Get instance of pass go UI.
        passGoUi = GameObject.FindObjectOfType<PassGoUI>();
        passGoUi.gameObject.SetActive(false);

        // Get instance of TaxUI.
        taxUI = GameObject.FindObjectOfType<TaxUI>();
        taxUI.gameObject.SetActive(false);

        // Get instance of UtilUI.
        utilUI = GameObject.FindObjectOfType<UtilityUI>();
        utilUI.gameObject.SetActive(false);

        // Get instance of jailLandedUI.
        jailLanded = GameObject.FindObjectOfType<JailLandedUI>();
        jailLanded.gameObject.SetActive(false);

        // Get instance of GoToJailUI.
        goToJailUI = GameObject.FindObjectOfType<GoToJailUI>();
        goToJailUI.gameObject.SetActive(false);

        // Get instance of InJailUI
        inJailUI = GameObject.FindObjectOfType<InJailUI>();
        inJailUI.gameObject.SetActive(false);

        // Get instance of InJailUI
        doubleRollUI = GameObject.FindObjectOfType<DoubleRollUI>();
        doubleRollUI.gameObject.SetActive(false);

        menu = GameObject.FindObjectOfType<PauseMenu>();
        menu.gameObject.SetActive(false);

        // Get an instance of the ownership class.
        owner = GameObject.FindObjectOfType<Ownership>();

        // Check the players money.
        CheckPlayerMoney();
        Debug.Log("Current player is: " + (CurrentPlayerID + 1));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.gameObject.SetActive(true);
            GameObject rollButton = GameObject.FindGameObjectWithTag("RollButton");
            rollButton.gameObject.SetActive(false);
        }

        // If all are true.
        if (IsDoneRolling && IsDoneMoving && IsDoneInteraction)
        {
            // We have finished the turn.
            // Move to the next player.
            CurrentPlayerID++;

            Player[] playerArray = GameObject.FindObjectsOfType<Player>();

            foreach(Player p in playerArray)
            {
                if(CurrentPlayerID == p.PlayerID)
                {
                    if(p.lost == true)
                    {
                        SkipTurn();
                    }
                }
            }

            // Reset all values.
            NewTurn();
        }
    }

    // Skips the turn of a player who has lost.
    public void SkipTurn()
    {
        CurrentPlayerID++;
    }

    // This method checks the players money and sets the current player money to the value assossiated with the current players turn.
    // Sets all the 'Is' values to false.
    public void NewTurn()
    {
        CheckPlayerMoney();

        CheckInJailForUI();

        if (CurrentPlayerID > 2)
        {
            CurrentPlayerID = 0;
        }
        Debug.Log("Current player is: " + (CurrentPlayerID + 1));
        IsDoneRolling = false;
        IsDoneMoving = false;
        IsDoneInteraction = false;
    }

    private IEnumerator CheckInJailForUI()
    {
        Player[] playerArray = GameObject.FindObjectsOfType<Player>();

        foreach (Player p in playerArray)
        {
            if (CurrentPlayerID == p.PlayerID)
            {
                if (p.isInJail == true)
                {
                    inJailUI.gameObject.SetActive(true);
                    yield return new WaitForSeconds(3);
                    inJailUI.gameObject.SetActive(false);
                }
            }
        }
    }

    // This method resets all 'Is' values so the player can roll again.
    public IEnumerator RollAgain()
    {
        IsDoneRolling = false;
        IsDoneMoving = false;
        IsDoneInteraction = false;
        doubleRoll = false;
        doubleRollUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        doubleRollUI.gameObject.SetActive(false);
        Debug.Log("Rolled a Double! Roll again!");
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
    public IEnumerator CheckPassingGo(Player player)
    {
        if (passedGo == true)
        {
            player.money += 200;
            passGoUi.gameObject.SetActive(true);
            Debug.Log("PASSED GO!!!!!!!!");
            yield return new WaitForSeconds(3);
            passGoUi.gameObject.SetActive(false);
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
                        if(doubleRoll == true)
                        {
                            return;
                        }
                        else
                        {
                            IsDoneInteraction = true;
                        }
                        break;
                    case "Property":
                        StartCoroutine(PropertyHandler(player, tile));
                        break;
                    case "Community":
                        StartCoroutine(CommunityChestHandler(player));
                        break;
                    case "Tax":
                        StartCoroutine(TaxHandler(player, tile));
                        break;
                    case "Rail":
                        StartCoroutine(RailRoadHandler(player, tile));
                        break;
                    case "Chance":
                        StartCoroutine(ChanceCardHandler(player));
                        break;
                    case "Jail":
                        StartCoroutine(JailHandler(player));
                        break;
                    case "Utility":
                        StartCoroutine(UtilityHandler(player, tile));
                        break;
                    case "Free Parking":
                        if (doubleRoll == true)
                        {
                            StartCoroutine(RollAgain());
                        }
                        else
                        {
                            IsDoneInteraction = true;
                        }
                        break;
                    case "Go To Jail":
                        StartCoroutine(GoToJail(player));
                        break;
                    case "LuxTax":
                        StartCoroutine(TaxHandler(player, tile));
                        break;
                }
            }
        }
        Debug.Log("----- END TURN -----");
    }

    // Pulls a chance card and does that chance cards action.
    private IEnumerator ChanceCardHandler(Player player)
    {
        int random = Random.Range(0, 7);
        ChanceCard card = chanceCards[random];

        chanceCardUI.chanceString = card.chanceAction;
        chanceCardUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        IsDoneInteraction = false;
        chanceCardUI.gameObject.SetActive(false);

        switch (card.chanceId)
        {
            case 1:
                GameObject goTileObj = GameObject.FindGameObjectWithTag("Go");
                Tile goTile = goTileObj.GetComponent<Tile>();
                player.currentTile = goTile;
                player.transform.position = goTile.transform.position;
                player.money += 200;
                break;
            case 2:
                // TODO: pass go mallarcy  
                GameObject nunsIslandObj = GameObject.FindGameObjectWithTag("Nun");
                Tile nunTile = nunsIslandObj.GetComponent<Tile>();
                player.currentTile = nunTile;
                player.transform.position = nunTile.transform.position;
                break;
            case 3:
                player.money += 50;
                break;
            case 4:
                GoToJailChance(player);
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
        if (doubleRoll == true)
        {
            StartCoroutine(RollAgain());
        }
        else
        {
            IsDoneInteraction = true;
        }
    }

    // Pulls a community chest card and does the action.
    private IEnumerator CommunityChestHandler(Player player)
    {
        int random = Random.Range(0, 8);
        CommunityChest chest = communityChests[random];

        commUI.communityString = chest.chestAction;
        commUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        commUI.gameObject.SetActive(false);

        switch (chest.chestId)
        {
            case 1:
                GameObject goTileObj = GameObject.FindGameObjectWithTag("Go");
                Tile goTile = goTileObj.GetComponent<Tile>();
                player.currentTile = goTile;
                player.transform.position = goTile.transform.position;
                player.money += 200;
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

        yield return new WaitForSeconds(1);
        if (doubleRoll == true)
        {
            StartCoroutine(RollAgain());
        }
        else
        {
            IsDoneInteraction = true;
        }
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

        if (CurrentPlayerMoney <= 0)
        {
            foreach (Player player in playerArray)
            {
                if (player.PlayerID == CurrentPlayerID)
                {
                    playerLoseCount++;
                    PlayerLose(player);
                    return 0;
                }
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
                Debug.Log("Checking monopoly");
                StartCoroutine(CheckMonopoly(player, currentProperty));
            }
            else
            {
                int rent = ((currentProperty.propertyFaceValue / 100) * 20) + currentProperty.houseCount * 50;
                propOwnedUi.rent = rent;

                Player[] playerArray = GameObject.FindObjectsOfType<Player>();

                foreach (Player players in playerArray)
                {
                    if (players.PlayerID == playerID)
                    {
                        propOwnedUi.player = players.PlayerID;
                        int playerPreviousMoney = players.money;
                        players.money += rent;
                        Debug.Log("Payed Player: " + (players.PlayerID + 1) + " " + rent);
                        Debug.Log("Player " + (players.PlayerID + 1) + " had " + playerPreviousMoney + " and now has " + players.money);
                    }
                }

                player.money -= rent;
                propOwnedUi.gameObject.SetActive(true);
                IsDoneInteraction = false;
                yield return new WaitForSeconds(3);
                propOwnedUi.gameObject.SetActive(false);
                Debug.Log(CurrentPlayerMoney);
                if (doubleRoll == true)
                {
                    StartCoroutine(RollAgain());
                }
                else
                {
                    IsDoneInteraction = true;
                }
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

            if (!propBuy)
            {
                propUI.gameObject.SetActive(false);
                propBuy = false;
                buttonPress = false;
                if (doubleRoll == true)
                {
                    StartCoroutine(RollAgain());
                }
                else
                {
                    IsDoneInteraction = true;
                }
            }
        }
    }

    public IEnumerator CheckMonopoly(Player player, Property property)
    {
        int ownCount = 0;
        int propCount = property.propertyGroup.Length;
        Debug.Log("You own this property.");
        // CHECK TO SEE IF MONOPOLY
        foreach (Property p in property.propertyGroup)
        {
            if (p.isOwned)
            {
                if (player.PlayerID == p.playerID)
                {
                    ownCount++;
                }
            }
        }

        if (ownCount == propCount)
        {
            Debug.Log("You have a monopoly.");
            propMonop.gameObject.SetActive(true);
            PropertyOwnMonopoly propMonopComp = GetComponent<PropertyOwnMonopoly>();

            timeLeft -= Time.deltaTime;
            while (noButtonPress == false)
            {
                if (timeLeft < 0)
                    buttonPress = true;
                yield return null;
            }
            if (property.houseCount == 4 || noButtonPress == true)
            {
                propMonop.gameObject.SetActive(false);
                if (doubleRoll == true)
                {
                    StartCoroutine(RollAgain());
                }
                else
                {
                    IsDoneInteraction = true;
                }
            }
        }
        else
        {
            if (doubleRoll == true)
            {
                StartCoroutine(RollAgain());
            }
            else
            {
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
        property.playerID = player.PlayerID;
        buttonPress = false;
        propBuy = false;
        if (doubleRoll == true)
        {
            StartCoroutine(RollAgain());
        }
        else
        {
            IsDoneInteraction = true;
        }
    }

    // This method handles all tax tile interactions.
    public IEnumerator TaxHandler(Player player, Tile tile)
    {
        TaxTile taxTile = tile.GetComponent<TaxTile>();
        taxUI.ammount = taxTile.taxAmmount;
        taxUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        taxUI.gameObject.SetActive(false);
        Debug.Log("You've landed on a tax tile.");
        player.money -= taxTile.taxAmmount;
        Debug.Log("You've paid 200 to the bank.");
        Debug.Log("You now have: " + player.money);
        yield return new WaitForSeconds(1);
        if (doubleRoll == true)
        {
            StartCoroutine(RollAgain());
        }
        else
        {
            IsDoneInteraction = true;
        }
    }

    public IEnumerator RailRoadHandler(Player player, Tile tile)
    {
        RailRoad railRoad = tile.GetComponent<RailRoad>();
        if (railRoad.isOwned)
        {
            Debug.Log("This railroad is owned.");
            int playerID = railRoad.playerID;
            int rent = railRoad.rent;
            railOwned.rent = rent;

            Player[] playerArray = GameObject.FindObjectsOfType<Player>();

            if (playerID == player.PlayerID)
            {
                Debug.Log("You own this railroad");
                if (doubleRoll == true)
                {
                    StartCoroutine(RollAgain());
                }
                else
                {
                    IsDoneInteraction = true;
                }
            }

            foreach (Player players in playerArray)
            {
                if (players.PlayerID == playerID)
                {
                    railOwned.player = players.PlayerID;
                    int playerPreviousMoney = players.money;
                    players.money += rent;
                    Debug.Log("Payed Player: " + (players.PlayerID + 1) + " " + rent);
                    Debug.Log("Player " + (players.PlayerID + 1) + " had " + playerPreviousMoney + " and now has " + players.money);
                }
            }

            player.money -= rent;
            railOwned.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            railOwned.gameObject.SetActive(false);
            Debug.Log(CurrentPlayerMoney);
            if (doubleRoll == true)
            {
                StartCoroutine(RollAgain());
            }
            else
            {
                IsDoneInteraction = true;
            }
        }
        else
        {
            Debug.Log("landed on unowned railroad");
            buyRail.gameObject.SetActive(true);
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
                BuyRail(player, railRoad);
            }

            if (!propBuy)
            {
                buyRail.gameObject.SetActive(false);
                propBuy = false;
                buttonPress = false;
                if (doubleRoll == true)
                {
                    StartCoroutine(RollAgain());
                }
                else
                {
                    IsDoneInteraction = true;
                }
            }
        }
    }

    public void BuyRail(Player player, RailRoad railRoad)
    {
        buyRail.gameObject.SetActive(false);
        player.money -= railRoad.buyAmmount;
        railRoad.isOwned = true;
        railRoad.playerID = CurrentPlayerID;
        Debug.Log("Purchased Railroad");
        Debug.Log("You now have: " + player.money);
        buttonPress = false;
        propBuy = false;
        if (doubleRoll == true)
        {
            StartCoroutine(RollAgain());
        }
        else
        {
            IsDoneInteraction = true;
        }
    }

    // This method handles when a player is in jail.
    public IEnumerator JailHandler(Player player)
    {
        if (player.isInJail)
        {
            jailLanded.inJail = true;
        }
        Debug.Log("Just visiting :D.");
        jailLanded.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        jailLanded.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        if (doubleRoll == true)
        {
            StartCoroutine(RollAgain());
        }
        else
        {
            IsDoneInteraction = true;
        }
    }

    // This method handles all utility tile interactions.
    public IEnumerator UtilityHandler(Player player, Tile tile)
    {
        UtilityTile utilityTile = tile.GetComponent<UtilityTile>();
        utilUI.utilName = utilityTile.utilityName;
        utilUI.utilPrice = utilityTile.utilityPrice;
        utilUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        utilUI.gameObject.SetActive(false);
        Debug.Log("You landed on a Utility tile.");
        player.money -= utilityTile.utilityPrice;
        Debug.Log("You paid " + utilityTile.utilityName + " " + utilityTile.utilityPrice);
        yield return new WaitForSeconds(1);
        if (doubleRoll == true)
        {
            StartCoroutine(RollAgain());
        }
        else
        {
            IsDoneInteraction = true;
        }
    }

    // Sends a player to jail.
    public IEnumerator GoToJail(Player player)
    {
        goToJailUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        goToJailUI.gameObject.SetActive(false);
        player.isInJail = true;
        Debug.Log("You've landed on the GO TO JAIL tile.");
        player.transform.position = GameObject.FindGameObjectWithTag("Jail").transform.position;
        GameObject jailTileObj = GameObject.FindGameObjectWithTag("Jail");
        Tile jailTile = jailTileObj.GetComponent<Tile>();
        player.currentTile = jailTile;
        Debug.Log("Moved to Jail.");
        if (doubleRoll == true)
        {
            StartCoroutine(RollAgain());
        }
        else
        {
            IsDoneInteraction = true;
        }
    }

    // Sends a player to jail from chance card.
    public void GoToJailChance(Player player)
    {
        player.isInJail = true;
        Debug.Log("You've landed on the GO TO JAIL tile.");
        player.transform.position = GameObject.FindGameObjectWithTag("Jail").transform.position;
        GameObject jailTileObj = GameObject.FindGameObjectWithTag("Jail");
        Tile jailTile = jailTileObj.GetComponent<Tile>();
        player.currentTile = jailTile;
        Debug.Log("Moved to Jail.");
        if (doubleRoll == true)
        {
            StartCoroutine(RollAgain());
        }
        else
        {
            IsDoneInteraction = true;
        }
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
        Property currentProp = GetProperty();
        currentProp.houseCount++;
        Player[] playerArray = GameObject.FindObjectsOfType<Player>();

        foreach (Player player in playerArray)
        {
            if (player.PlayerID == CurrentPlayerID)
            {
                player.houseCount++;
                player.money -= 100;
            }
        }
        currentProp.AddHouse();

    }

    public void PlayerLose(Player player)
    {
        if(playerLoseCount == 2)
        {
            // DISPLAY WINNER
        }
        Property[] propArray = GameObject.FindObjectsOfType<Property>();

        playerLoseID = player.PlayerID;

        foreach(Property p in propArray)
        {
            if(p.playerID == player.PlayerID)
            {
                p.isOwned = false;
            }
        }

        RailRoad[] railArray = GameObject.FindObjectsOfType<RailRoad>();

        foreach (RailRoad r in railArray)
        {
            if (r.playerID == player.PlayerID)
            {
                r.isOwned = false;
            }
        }

        player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        player.lost = true;
        IsDoneRolling = true;
        IsDoneMoving = true;
        IsDoneInteraction = true;
    }

    
}
