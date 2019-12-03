using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InJailUI : MonoBehaviour
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
        text.text = "You are IN JAIL!";
    }
}
