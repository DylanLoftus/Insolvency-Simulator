using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property : MonoBehaviour
{
    private string propertyName;
    private int propertyFaceValue;
    private int houseCost;
    private string propertyColour;

    public Property(string propertyName, int propertyFaceValue, int houseCost, string propertyColour)
    {
        this.propertyName = propertyName;
        this.propertyFaceValue = propertyFaceValue;
        this.houseCost = houseCost;
        this.propertyColour = propertyColour;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
