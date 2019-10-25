using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentPlayerMoney : MonoBehaviour
{
    // Instance Variables
    GameManager theGameManager;
    Text currentPlayerMoney;

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference for the game manager and the text element.
        theGameManager = GameObject.FindObjectOfType<GameManager>();
        currentPlayerMoney = GetComponent<Text>();
    }

    

    // Update is called once per frame
    void Update()
    {
        // Updates the text component depending on which player's turn it is.
        currentPlayerMoney.text = "Money: " + theGameManager.CheckPlayerMoney();
    }
}
