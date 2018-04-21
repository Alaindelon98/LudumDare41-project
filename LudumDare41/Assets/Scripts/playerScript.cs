using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

	public float maxSpeed, initialSpeed;

	private float speed;
	private Rigidbody2D myRb;


	// Use this for initialization
	void Start () 
	{
		myRb = GetComponent<Rigidbody2D> ();
		speed = initialSpeed;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Mov ();

		if (Input.GetKeyDown("space"))
		{
			print ("espaciuuu");
		}

	}
	void Mov()
	{
		myRb.velocity = new Vector2 (speed, myRb.velocity.y);
	}
		
	void Jump()
	{

	}

	void Sprint()
	{

	}
}
