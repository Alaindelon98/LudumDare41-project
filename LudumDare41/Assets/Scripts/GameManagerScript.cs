using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static int PlayerMoney;
    public static playerScript player;
	public static List<enemyScript> enemies;
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
        GetPlayerDistance();
    }

    public static void GetPlayerDistance()
    {
        Debug.Log(sumMoneyDistance_s);
        Debug.Log(""+totalPlayerDistance);

        float newDistance = player.transform.position.x;

        if (totalPlayerDistance < newDistance)
        {
            playerDistanceCounter += newDistance - totalPlayerDistance;
            totalPlayerDistance += newDistance - totalPlayerDistance;

            if (playerDistanceCounter >= sumMoneyDistance_s)
            {
                PlayerMoney++;
                Debug.Log(PlayerMoney);
                playerDistanceCounter = 0;
            }
        }

    }

	public static void LoadLevelFunction(playerScript _player, List<enemyScript> _enemies)
	{
		player = _player;
		enemies = _enemies;
	}
    public static void PlayerDeath()
    {
        playerDistanceCounter = 0;
        totalPlayerDistance = 0;

    }
    public void RespawnPlayer()
    {
        player.transform.position = player.spawnPosition;
        player.gameObject.SetActive(true);
    }
}