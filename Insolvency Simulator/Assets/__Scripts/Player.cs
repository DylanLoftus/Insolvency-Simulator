using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Tile StartTile;
    Tile currentTile;
    Dice dice;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = StartTile.transform.position;
        dice = GameObject.FindObjectOfType<Dice>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        
        if (dice.IsDoneRolling == false)
        {
            // Do not move
            return;
        }
        
        int spacesToMove = dice.DiceTotal;

        Tile finalTile = currentTile;

        for (int i = 0; i < spacesToMove; i++)
        {
            if (currentTile == null)
            {
                finalTile = StartTile;
            }
            else
            {
                finalTile = finalTile.NextTiles[0];
            }
        }

        this.transform.position = finalTile.transform.position;

        currentTile = finalTile;
    }
}
