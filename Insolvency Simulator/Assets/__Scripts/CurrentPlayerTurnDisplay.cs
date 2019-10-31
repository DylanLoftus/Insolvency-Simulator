using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentPlayerTurnDisplay : MonoBehaviour
{
    // Instance Variables
    GameManager theGameManager;
    Text currentPlayerText;
    Sprite currentPlayerSprite;

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference for the game manager and the text element.
        theGameManager = GameObject.FindObjectOfType<GameManager>();
        currentPlayerText = GetComponent<Text>();
        currentPlayerSprite = GetComponent<Image>().sprite;
    }



    // Prints out the words instead of the number.
    string[] numberWords = {"One", "Two", "Three" };

    // Update is called once per frame
    void Update()
    {
        // Updates the text component depending on which player's turn it is.
        currentPlayerText.text = "Current Player: " + numberWords[theGameManager.CurrentPlayerID];
        currentPlayerSprite = theGameManager.playerImages[theGameManager.CurrentPlayerID];
    }
}
