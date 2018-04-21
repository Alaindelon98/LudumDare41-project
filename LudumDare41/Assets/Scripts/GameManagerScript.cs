using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static int PlayerMoney;
    public static playerScript player;
	public static List<enemyScript> enemies;
    public float sumMoneyDistance;
    public int RespawnTime;
    public static float sumMoneyDistance_s;
    public static float playerDistanceCounter, totalPlayerDistance;


    public enum GameState {Dead,OnRun };
    public static GameState actualGameState;

    // Use this for initialization
    void Start()
    {
        sumMoneyDistance_s = sumMoneyDistance;
    }

    // Update is called once per frame
    void Update()
    {
        switch (actualGameState)
        {
            case GameState.Dead:

                Invoke("RespawnPlayer", RespawnTime);
                ChangePlayerState(GameState.OnRun);

                break;
            case GameState.OnRun:

                GetPlayerDistance();

                break;
        }
       
    }
    public static void ChangePlayerState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Dead:
                break;
            case GameState.OnRun:
                break;
        }
        actualGameState = newState;
    }

    public static void GetPlayerDistance()
    {
        //Debug.Log("StandardDistance "+sumMoneyDistance_s+ " //  DistanceCounter: " + playerDistanceCounter);
        //Debug.Log("TotalDistance "+totalPlayerDistance);


        float newDistance =  Mathf.Abs(player.transform.position.x- player.spawnPosition.x);

    

        if (totalPlayerDistance < newDistance)
        {
            playerDistanceCounter += newDistance - totalPlayerDistance;
            totalPlayerDistance += newDistance - totalPlayerDistance;

            if (playerDistanceCounter >= sumMoneyDistance_s)
            {
                PlayerMoney++;
                Debug.Log("ActualMoney " + PlayerMoney);
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
        player.transform.position = player.spawnPosition;
        ChangePlayerState(GameState.Dead);


    }
    public void RespawnPlayer()
    {
        foreach(enemyScript e in enemies)
        {
            e.ResetEnemy();
        }
        
        player.gameObject.SetActive(true);
        
    }
}