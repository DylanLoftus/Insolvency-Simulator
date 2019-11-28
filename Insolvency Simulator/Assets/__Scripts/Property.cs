using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property : MonoBehaviour
{
    public string propertyName;
    public int propertyFaceValue;
    public int houseCost;
    public int houseCount;
    public bool isOwned = false;
    public int playerID;
    public Property[] propertyGroup;
    public Transform houses;
    public Transform houseT;
    public SpriteRenderer house;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHouse()
    {
        switch (houseCount)
        {
            case 1:
                houses = gameObject.transform.Find("Houses");
                houseT = houses.gameObject.transform.GetChild(0);
                house = houseT.GetComponent<SpriteRenderer>();
                house.gameObject.SetActive(true);
                break;
            case 2:
                houses = gameObject.transform.Find("Houses");
                houseT = houses.gameObject.transform.GetChild(1);
                house = houseT.GetComponent<SpriteRenderer>();
                house.gameObject.SetActive(true);
                break;
            case 3:
                houses = gameObject.transform.Find("Houses");
                houseT = houses.gameObject.transform.GetChild(2);
                house = houseT.GetComponent<SpriteRenderer>();
                house.gameObject.SetActive(true);
                break;
            case 4:
                houses = gameObject.transform.Find("Houses");
                houseT = houses.gameObject.transform.GetChild(3);
                house = houseT.GetComponent<SpriteRenderer>();
                house.gameObject.SetActive(true);
                break;
        }
    }
}
