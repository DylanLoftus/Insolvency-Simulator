using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RailData
{
    public int buyAmmount;
    public int rent;
    public bool isOwned;
    public int playerID;

    public RailData (RailRoad railRoad)
    {
        buyAmmount = railRoad.buyAmmount;
        rent = railRoad.rent;
        isOwned = railRoad.isOwned;
        playerID = railRoad.playerID;
    }
}
