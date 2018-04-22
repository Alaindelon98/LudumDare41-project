using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript current;
    public static float PlayerMoney, totalRuns;
    public static playerScript player;
    public static List<enemyScript> enemies;
    public static List<Transform> actions;
    public float sumMoneyDistance;
    public float RespawnTime;
    public static float sumMoneyDistance_s;
    public static float playerDistanceCounter, totalPlayerDistance, totalGamePlayerDistance, moneyLastRun, theseRunMoney;
    public static CameraScript mainCamera;
	public static float moneyFromCoin;
	public float initialCoinValue;

    public static float jumpPrice, sprintPrice, crouchPrice, reversePrice, wallJumpPrice;


    public enum GameState { Dead, OnRun };
    public static GameState actualGameState;

    // Use this for initialization
    void Start()
    {
        sumMoneyDistance_s = sumMoneyDistance;
        mainCamera = Camera.main.GetComponent<CameraScript>();

        enemies = new List<enemyScript>();
        actions = new List<Transform>();

		moneyFromCoin = initialCoinValue;
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
                player.gameObject.SetActive(false);
                moneyLastRun = theseRunMoney;
                playerDistanceCounter = 0;
                totalGamePlayerDistance = totalPlayerDistance;
                totalPlayerDistance = 0;
                player.transform.position = player.spawnPosition;
                break;
            case GameState.OnRun:

                break;
        }
        actualGameState = newState;
    }

    public static void GetPlayerDistance()
    {


        float newDistance = Mathf.Abs(player.transform.position.x - player.spawnPosition.x);



        if (totalPlayerDistance < newDistance)
        {
            playerDistanceCounter += newDistance - totalPlayerDistance;
            totalPlayerDistance += newDistance - totalPlayerDistance;

            if (playerDistanceCounter >= sumMoneyDistance_s)
            {
                PlayerMoney++;
                theseRunMoney++;
                playerDistanceCounter = 0;
            }
        }

    }

    public static void LoadLevelFunction(playerScript _player, List<enemyScript> _enemies)
    {
        player = _player;
        enemies = _enemies;
    }
    public  void PlayerDeath()
    {

        ChangePlayerState(GameState.Dead);


    }
    public void RespawnPlayer()
    {
        totalRuns++;

        foreach (enemyScript e in enemies)
        {
            e.ResetEnemy();
        }

        player.gameObject.SetActive(true);

    }
}