using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

	public float maxSpeed, initialSpeed, jumpVelocity, speedDecrease, fallMultiplier, lowJumpMultiplier, jumpMultiplier;

    public Vector3 spawnPosition;
    private bool grounded;
	private float speed;
	private Rigidbody2D myRb;
	private float jumpSpeed;

	// Use this for initialization
	void Start () 
	{
        spawnPosition = transform.position;
		myRb = GetComponent<Rigidbody2D> ();
		speed = initialSpeed;
		jumpSpeed = jumpMultiplier * speed;
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
    public void Move()
	{
		myRb.velocity = new Vector2 (speed, myRb.velocity.y);
	}

    public void Jump()
	{
		speed = jumpSpeed;
		grounded = false;
		myRb.velocity =new Vector2(speed,jumpVelocity);
        
	}

    public void Sprint()
	{
		if (speed <= maxSpeed) {
			speed = maxSpeed;
		} 

		myRb.velocity = new Vector2 (speed, myRb.velocity.y);
	}

    public void Crouch()
	{
		
	}

    public void Change()
	{
		if (speed > 0) {
			speed = -Mathf.Abs (speed);
		}
		else if (speed < 0) {
			speed = Mathf.Abs (speed);
		}
	}

    public void WallJump()
	{
		Jump ();
		Change ();
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        grounded = true;
    }
}
