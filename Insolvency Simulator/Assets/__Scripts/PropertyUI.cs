using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyUI : MonoBehaviour
{
    Text text;
    public GameManager theGameManager;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        theGameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
