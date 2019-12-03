using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UtilityUI : MonoBehaviour
{
    Text text;
    public GameManager theGameManager;
    public string utilName;
    public int utilPrice;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        theGameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "You landed on a utility!\n" + "You payed €" + utilPrice + " for " + utilName;
    }
}
