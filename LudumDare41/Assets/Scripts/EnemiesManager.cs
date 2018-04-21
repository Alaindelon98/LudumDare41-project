using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour {

	//public GameObject player;
	public float distanceMax;
	private List<enemyScript> enemies;

	private float distance;


	// Use this for initialization
	void Start () 
	{
		enemies = GameManagerScript.enemies;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//ActivateEnemies ();
	}

	void ActivateEnemies()
	{
		foreach (enemyScript item in enemies) {
            distance = GameManagerScript.player.transform.position.x - item.gameObject.transform.position.x;
			//distanceVec = player.transform.position - item.gameObject.transform.position;
			
			if (distance <= distanceMax)
			{
				item.enabled = true;
			}
			else if (distance >= distanceMax)
			{
				item.enabled = false;
			}
		}

	}
}
