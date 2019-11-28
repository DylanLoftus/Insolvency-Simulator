using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyOwnMonopoly : MonoBehaviour
{
    Text text;
    public GameManager theGameManager;
    Button[] button;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        theGameManager = GameObject.FindObjectOfType<GameManager>();
        button = GetComponentsInChildren<Button>();

    }

    // Update is called once per frame
    void Update()
    {
        if(theGameManager.currentProperty.houseCount == 4)
        {
            text.text = "You've landed on " + theGameManager.currentProperty.propertyName + "!\n You also have a monopoly!\n You have maximum houses.";
            foreach(Button b in button)
            {
                b.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (Button b in button)
            {
                b.gameObject.SetActive(true);
            }
            text.text = "You've landed on " + theGameManager.currentProperty.propertyName + "!\n You also have a monopoly!\n Would you like to purchase a house?";
        }
    }

    public void BuyHouse()
    {
        theGameManager.BuyHouse();
        theGameManager.buttonPress = true;
    }

    public void NoBuyHouse()
    {
        theGameManager.buttonPress = true;
    }
}
