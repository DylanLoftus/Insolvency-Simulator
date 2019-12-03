using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaxUI : MonoBehaviour
{
    Text text;
    public GameManager theGameManager;
    public int ammount;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        theGameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "You landed on a tax tile!\n You must pay €" + ammount;
    }
}
