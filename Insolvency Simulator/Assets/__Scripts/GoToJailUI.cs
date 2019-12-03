using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToJailUI : MonoBehaviour
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
        text.text = "You landed on GO TO JAIL!\n Go directly to Jail.";
    }
}
