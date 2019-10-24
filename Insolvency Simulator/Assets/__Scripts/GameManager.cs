using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int CurrentPlayerID = 0;
    public int DiceTotal;

    public int NumberOfPlayers = 3;

    // To check if the dice is done rolling or not.
    public bool IsDoneRolling = false;
    // To check if the player is done moving.
    public bool IsDoneMoving = false;
    // To check if the player has finished their tile interaction.
    // NOT IN USE YET.
    //public bool IsDoneInteraction = false;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Current player is: " + CurrentPlayerID);
    }

    // Update is called once per frame
    void Update()
    {
        // If all are true.
        if(IsDoneRolling && IsDoneMoving)
        {
            Debug.Log("Turn completed!");
            // We have finished the turn.
            CurrentPlayerID++;
            NewTurn();
        }
    }

    public void NewTurn()
    {
        if (CurrentPlayerID > 2)
        {
            CurrentPlayerID = 0;
        }
        Debug.Log("Current player is: " + CurrentPlayerID);

        IsDoneRolling = false;
        IsDoneMoving = false;
        //IsDoneInteraction = false;
    }

    public void CheckCorrectPlayer()
    {
        Player[] playerArray = GameObject.FindObjectsOfType<Player>();

        foreach (Player player in playerArray)
        {
            if (player.PlayerID == CurrentPlayerID)
            {
                player.Move();
            }
        }
    }
}
