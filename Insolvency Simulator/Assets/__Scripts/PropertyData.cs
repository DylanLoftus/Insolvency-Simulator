using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PropertyData
{
    public string propertyName;
    public int propertyFaceValue;
    public int houseCost;
    public int houseCount;
    public bool isOwned;
    public int playerID;
    //public Property[] propertyGroup;

    public PropertyData (Property property)
    {
        propertyName = property.propertyName;
        propertyFaceValue = property.propertyFaceValue;
        houseCost = property.houseCost;
        houseCount = property.houseCount;
        isOwned = property.isOwned;
        playerID = property.playerID;
        //propertyGroup = property.propertyGroup;
    }
}
