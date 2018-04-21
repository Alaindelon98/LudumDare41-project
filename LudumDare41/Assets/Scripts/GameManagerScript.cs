using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static int PlayerMoney;
    public float sumMoneyDistance;
    public static float sumMoneyDistance_s;
    public static float playerDistanceCounter, totalPlayerDistance;

    // Use this for initialization
    void Start()
    {
        sumMoneyDistance_s = sumMoneyDistance;


    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void GetPlayerDistance(float newDistance)
    {
        if (totalPlayerDistance < newDistance)
        {
            playerDistanceCounter += totalPlayerDistance - newDistance;
            totalPlayerDistance += totalPlayerDistance - newDistance;

            if (playerDistanceCounter >= sumMoneyDistance_s)
            {
                PlayerMoney++;
                playerDistanceCounter = 0;

            }
        }

    }
}