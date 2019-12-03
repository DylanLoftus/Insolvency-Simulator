using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RailOwnedUI : MonoBehaviour
{
    Text text;
    public GameManager theGameManager;
    public int player, rent;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        theGameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "You owe Player " + (player + 1) + " €" + rent;
    }
}
