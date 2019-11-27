using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ownership : MonoBehaviour
{
    public Sprite[] playerIcons;
    public GameManager theGameManager;
    public SpriteRenderer sprite;
    public Property property;

    // Start is called before the first frame update
    void Start()
    {
        theGameManager = GameObject.FindObjectOfType<GameManager>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        property = gameObject.GetComponentInParent<Property>();
    }

    // Update is called once per frame
    void Update()
    {
        if (property.isOwned)
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
}
