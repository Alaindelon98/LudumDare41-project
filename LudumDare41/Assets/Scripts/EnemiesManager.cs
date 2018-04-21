using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour {

	//public GameObject player;
	public float distanceMax;
	public List<enemyScript> enemies;
	private Vector3 distanceVec;
	private float distance;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		ActivateEnemies ();
	}

	void ActivateEnemies()
	{
		foreach (enemyScript item in enemies) {
			distanceVec = GameManagerScript.player.transform.position - item.gameObject.transform.position;
			//distanceVec = player.transform.position - item.gameObject.transform.position;
			distance = distanceVec.magnitude;
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
