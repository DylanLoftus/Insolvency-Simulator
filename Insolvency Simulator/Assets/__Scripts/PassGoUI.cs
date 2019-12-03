using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassGoUI : MonoBehaviour
{
    Text text;
    public GameManager theGameManager;
    public int player, rent;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "YOU PASSED GO! COLLECT €200";
    }
}
