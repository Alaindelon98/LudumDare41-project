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

	Animator anim;
	int jumpToHash = Animator.StringToHash("Jump");

	// Use this for initialization
	void Start () 
	{
        spawnPosition = transform.position;
		myRb = GetComponent<Rigidbody2D> ();
		speed = initialSpeed;
		jumpSpeed = jumpMultiplier * speed;

		anim = GetComponent<Animator> ();
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
		myRb.velocity = new Vector2 (speed, myRb.velocity.y);
	}
		
	void Jump()
	{
		speed = jumpSpeed;
		grounded = false;
		myRb.velocity =new Vector2(speed,jumpVelocity);

		//anim.SetTrigger (jumpToHash);
        
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
		if (speed > 0) {
			speed = -Mathf.Abs (speed);
		}
		else if (speed < 0) {
			speed = Mathf.Abs (speed);
		}
	}

	void WallJump()
	{
		Jump ();
		Reverse ();
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
			case "Reverse":
				Reverse ();
				break;
			case "WallJump":
				WallJump ();
				break;
			
        }
    }
}
