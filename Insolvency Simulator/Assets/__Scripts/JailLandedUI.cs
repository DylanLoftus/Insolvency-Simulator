using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JailLandedUI : MonoBehaviour
{
    Text text;
    public GameManager theGameManager;
    public bool inJail = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        theGameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inJail)
        {
            text.text = "You're in JAIL!";
        }
        else
        {
            text.text = "You landed on Jail but you are just visiting :D";
        }
    }
}
