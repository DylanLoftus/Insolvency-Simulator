using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    public int Roll()
    {
        int MIN = 1;
        int MAX = 6;
        int d1, d2;
        int rollNumber;

        System.Random rndm = new System.Random();

        d1 = rndm.Next(MIN, MAX);
        d2 = rndm.Next(MIN, MAX);

        CheckDoubleRoll(d1, d2);

        rollNumber = d1 + d2;

        return rollNumber;
    }

    private bool CheckDoubleRoll(int d1, int d2)
    {
        bool doubleJump;

        if (d1.Equals(d2))
        {
            doubleJump = true;
        }
        else
        {
            doubleJump = false;
        }

        return doubleJump;
    }
}
