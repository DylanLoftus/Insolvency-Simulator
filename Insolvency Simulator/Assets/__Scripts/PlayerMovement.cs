using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Transform[] waypoints;

    [SerializeField]
    float moveSpeed = 2f;

    int rollChange = 0;

    int waypointIndex = 0;
    private bool hasRolled = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (hasRolled == false)
        {
            hasRolled = true;
            int numberOfSpacesMove = Roll();
            Debug.Log(numberOfSpacesMove);
            waypointIndex = 12;
        }

        if(rollChange != 400)
        {
            rollChange++;

            Debug.Log(waypointIndex);
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
        }
        
        /*
        if(transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }

        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
        */
    }
    
    public int Roll()
    {
        int MIN = 1;
        int MAX = 6;
        int d1, d2;
        int rollNumber;

        System.Random rndm = new System.Random();

        d1 = rndm.Next(MIN, MAX);
        Debug.Log(d1);
        d2 = rndm.Next(MIN, MAX);
        Debug.Log(d2);

        //CheckDoubleRoll(d1, d2);

        rollNumber = d1 + d2;

        return rollNumber;
    }
    
}
