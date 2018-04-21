using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {


	public float speed;
	private int direction=1;

	// Use this for initialization
	void Start () {
		this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Mov ();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Right")
		{
			direction = -Mathf.Abs(direction);
		}
		if (col.gameObject.tag == "Left")
		{
			direction = Mathf.Abs(direction);
		}
	}

	private void Mov()
	{
		transform.position += new Vector3 (direction*speed*Time.deltaTime,0,0);
		//saltito 
	}
}
