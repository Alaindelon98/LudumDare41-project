using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelScript : MonoBehaviour {

	public AudioSource active, shop, coin, death, win;
	public playerScript player;
	public List<enemyScript> enemies;

	void Awake()
	{
		GameManagerScript.LoadLevelFunctionAwake (shop, coin, active, death, win);
	}

	// Use this for initialization
	void Start ()
    {
        GameManagerScript.LoadLevelFunction(player, enemies);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
