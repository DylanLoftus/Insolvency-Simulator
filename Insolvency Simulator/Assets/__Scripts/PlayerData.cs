using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int PlayerID { get; set; }
    public bool isInJail { get; set; }
    public bool lost { get; set; }
    public int jailTurn { get; set; }
    public int money { get; set; }
    public int houseCount { get; set; }
    public int doubleCount { get; set; }
    public float[] position { get; set; }
    public Tile currentTile { get; set; }
}
