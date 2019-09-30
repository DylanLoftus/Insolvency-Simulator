using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    List<Transform> waypoints;

    [SerializeField]
    float moveSpeed = 2f;

    int waypointIndex = 0;

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
        /*
        int numberOfSpacesMove = Roll();
        Debug.Log(numberOfSpacesMove);
        waypointIndex += numberOfSpacesMove;

        Debug.Log(waypointIndex);
        */

        waypointIndex = 6;
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
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
    /*
    public int Roll()
    {
        int MIN = 1;
        int MAX = 6;
        int d1, d2;
        int rollNumber;

        System.Random rndm = new System.Random();

        d1 = rndm.Next(MIN, MAX);
        d2 = rndm.Next(MIN, MAX);

        //CheckDoubleRoll(d1, d2);

        rollNumber = d1 + d2;

        return rollNumber;
    }
    */
}
