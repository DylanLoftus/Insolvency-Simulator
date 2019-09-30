using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string name;
    private int money = 1500;
    private Vector2 position;
    private GameObject token;
    List<Property> propertiesOwned = new List<Property>();

    public Player(string name, int money, Vector2 position, GameObject token,List<Property> propertiesOwned)
    {
        this.name = name;
        this.money = money;
        this.position = position;
        this.token = token;
        this.propertiesOwned = propertiesOwned;
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
