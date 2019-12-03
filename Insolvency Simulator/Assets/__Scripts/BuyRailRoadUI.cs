using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyRailRoadUI : MonoBehaviour
{
    Text text;
    public GameManager theGameManager;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        theGameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "You've landed on a RailRoad that isn't owned!\n Would you like to buy?";

    }

    public void BuyTrue()
    {
        theGameManager.buttonPress = true;
        theGameManager.propBuy = true;
    }

    public void BuyFalse()
    {
        theGameManager.buttonPress = true;
        theGameManager.propBuy = false;
    }
}
