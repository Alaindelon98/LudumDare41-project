using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelScript : MonoBehaviour {

	public playerScript player;
	public List<enemyScript> enemies;

	void Awake()
	{
		GameManagerScript.LoadLevelFunction (player, enemies);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
