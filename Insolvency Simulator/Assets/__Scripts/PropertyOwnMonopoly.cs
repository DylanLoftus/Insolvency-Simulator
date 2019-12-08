using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyOwnMonopoly : MonoBehaviour
{
    Text text;
    public GameManager theGameManager;
    Button[] button;
    public bool noButtonPress = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        theGameManager = GameObject.FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(theGameManager.GetProperty().houseCount == 4)
        {
            this.gameObject.SetActive(false);
            if (theGameManager.doubleRoll == true)
            {
                theGameManager.StartCoroutine(theGameManager.RollAgain());
            }
            else
            {
                theGameManager.IsDoneInteraction = true;
            }
        }
        else
        {
            text.text = "You've landed on " + theGameManager.GetProperty().propertyName + "!\n You also have a monopoly!\n Would you like to purchase a house?";
        }
    }

    public void BuyHouse()
    {
        theGameManager.BuyHouse();
        theGameManager.buttonPress = true;
    }

    public void NoBuyHouse()
    {
        theGameManager.noButtonPress = true;
    }
}
