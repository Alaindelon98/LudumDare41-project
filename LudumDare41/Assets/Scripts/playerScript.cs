﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

	public float maxSpeed, initialSpeed, jumpVelocity, speedDecrease, fallMultiplier, lowJumpMultiplier, jumpMultiplier;

    public Vector3 spawnPosition;
    private bool grounded;
	private float speed;
	private int directionX;
	private Rigidbody2D myRb;
	private float jumpSpeed;

	Animator animatorController;

	// Use this for initialization
	void Start () 
	{
		directionX = 1;
        spawnPosition = transform.position;
		myRb = GetComponent<Rigidbody2D> ();
		speed = initialSpeed;
		jumpSpeed = jumpMultiplier * speed;

		animatorController = GetComponentInChildren<Animator> ();

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

        if (speed > initialSpeed) 
		{
			speed -= speedDecrease * Time.deltaTime;
		}

	}
	void Move()
	{
		myRb.velocity = new Vector2 (directionX* speed, myRb.velocity.y);

	}
		
	void Jump()
	{
		speed = jumpSpeed;
		grounded = false;
		myRb.velocity =new Vector2(directionX * speed,jumpVelocity);

		animatorController.SetBool ("Jump", true);
		if (grounded = false && myRb.velocity.y <= 0f) {
			animatorController.SetBool ("Falling", true);
		}    
        
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

	void Reverse()
	{
		directionX *= -1;
	}

	void WallJump()
	{
		//Reverse ();
		directionX *= -1;
		Jump ();

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
				Reverse ();
				break;
			case "WallJump":
				WallJump ();
				break;
			
        }
    }
}
