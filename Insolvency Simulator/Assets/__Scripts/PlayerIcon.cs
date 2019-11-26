using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIcon : MonoBehaviour
{
    public Sprite[] playerIcons;
    public GameManager theGameManager;

    // Start is called before the first frame update
    void Start()
    {
        theGameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (theGameManager.CurrentPlayerID)
        {
            case 0:
                GetComponent<Image>().sprite = playerIcons[0];
                break;
            case 1:
                GetComponent<Image>().sprite = playerIcons[1];
                break;
            case 2:
                GetComponent<Image>().sprite = playerIcons[2];
                break;
        }
    }
}
