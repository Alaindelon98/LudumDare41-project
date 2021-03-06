﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript current;
	public static AudioSource active, shop, coin, death, win;
    public static float PlayerMoney, totalRuns;
    public static playerScript player;
    public static List<enemyScript> enemies;
    public static List<Transform> actions;
	public static List<GameObject> takenCoins;
    public float sumMoneyDistance;
    public float RespawnTime;
    public static float sumMoneyDistance_s;
    public static float playerDistanceCounter, totalPlayerDistance, totalGamePlayerDistance, moneyLastRun, theseRunMoney;
    public static CameraScript mainCamera;
	public static float moneyFromCoin;
	public float initialCoinValue;
    public static GameObject WinPanel;
    public static float jumpPrice, sprintPrice, crouchPrice, reversePrice, wallJumpPrice;

    public static float priceIncrement;

    public enum GameState { Dead, OnRun ,Win};
    public static GameState actualGameState;

    // Use this for initialization
    void Start()
    {
        sumMoneyDistance_s = sumMoneyDistance;
        mainCamera = Camera.main.GetComponent<CameraScript>();
        actualGameState = GameState.OnRun;
        enemies = new List<enemyScript>();
        actions = new List<Transform>();
		takenCoins = new List<GameObject> ();
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


                Instantiate(player.blood.gameObject, player.transform.position, player.transform.rotation);
                death.Play();
                player.gameObject.SetActive(false);
                moneyLastRun = theseRunMoney;
                theseRunMoney = 0;
                playerDistanceCounter = 0;
                totalGamePlayerDistance = totalPlayerDistance;
                totalPlayerDistance = 0;
                player.transform.position = player.spawnPosition;
                break;
            case GameState.OnRun:

                break;
            case GameState.Win:

                WinPanel.SetActive(true);
                win.Play();


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

	public static void LoadLevelFunctionAwake(AudioSource _shop, AudioSource _coin, AudioSource _active, AudioSource _death, AudioSource _win,GameObject _winPanel)
	{
		shop = _shop;
		coin = _coin;
		active = _active;
		death = _death;
		win = _win;
        WinPanel = _winPanel;
	}

    public static void LoadLevelFunction(playerScript _player, List<enemyScript> _enemies)
    {
        player = _player;
        enemies = _enemies;
    }
    public  void PlayerDeath()
    {
		death.Play ();
        ChangePlayerState(GameState.Dead);
    }
    public void RespawnPlayer()
    {
		player.direction = player.initialDirection;
		player.speed = player.initialSpeed;
		if (player.transform.localScale.x < 0) 
		{
			Vector3 scale = player.transform.localScale;
			scale.x = 1;
			player.transform.localScale = scale;
		}
        totalRuns++;
		foreach (GameObject coin in takenCoins) 
		{
			coin.SetActive (true);
		}
		takenCoins.Clear ();
        foreach (enemyScript e in enemies)
        {
            e.ResetEnemy();
        }

        player.gameObject.SetActive(true);

    }
}