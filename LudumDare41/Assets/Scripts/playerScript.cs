﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

	public float maxSpeed, initialSpeed, jumpVelocity, speedDecrease, fallMultiplier, lowJumpMultiplier;

    public Vector3 spawnPosition;
    private bool grounded;
	private float speed;
	public Rigidbody2D myRb;


	// Use this for initialization
	void Start () 
	{
        spawnPosition = transform.position;
		myRb = GetComponent<Rigidbody2D> ();
		speed = initialSpeed;

      
    }
	
	// Update is called once per frame
	void Update () 
	{
		Move ();

        /*if (Input.GetKeyDown("space"))
		{
			Jump();
		}*/

        /*Sprint ();
		print (speed);*/

        if (myRb.velocity.y < 0)
        {
            myRb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        else if (myRb.velocity.y > 0)
        {
            myRb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (speed > initialSpeed) {
			speed -= speedDecrease * Time.deltaTime;
		}
	}
	void Move()
	{
		myRb.velocity = new Vector2 (speed, myRb.velocity.y);
	}
		
	void Jump()
	{
		myRb.velocity =new Vector2(speed*4,jumpVelocity);
        
	}

	void Sprint()
	{
		if (speed <= maxSpeed) {
			speed = maxSpeed;
		} 

		myRb.velocity = new Vector2 (speed, myRb.velocity.y);
	}


	void Crouch()
	{
		
	}

	void Change()
	{
		//speed = -Mathf.Abs(sp)
	}

	void WallJump()
	{
		
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        grounded = true;


    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Jump":
                Jump();
                break;
            case "Sprint":
                Sprint();
                break;
			case "Crouch":
				Crouch ();
				break;
			case "Change":
				Change ();
				break;
			case "WallJump":
				WallJump ();
				break;
			
        }
    }
}
