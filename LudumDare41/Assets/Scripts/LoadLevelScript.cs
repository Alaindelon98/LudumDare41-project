using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelScript : MonoBehaviour {

	public playerScript player;
	public List<enemyScript> enemies;

	// Use this for initialization
	void Start ()
    {
        GameManagerScript.LoadLevelFunction(player, enemies);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
