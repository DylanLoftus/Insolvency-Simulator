using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ownership : MonoBehaviour
{
    public Sprite[] playerIcons;
    public GameManager theGameManager;
    public Property property;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        theGameManager = GameObject.FindObjectOfType<GameManager>();
        property = gameObject.GetComponentInParent<Property>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (property.playerID)
        {
            case 0:
                sprite.sprite = playerIcons[0];
                break;
            case 1:
                sprite.sprite = playerIcons[1];
                break;
            case 2:
                sprite.sprite = playerIcons[2];
                break;
            default:
                sprite.sprite = null;
                break;

        }
    }
}
