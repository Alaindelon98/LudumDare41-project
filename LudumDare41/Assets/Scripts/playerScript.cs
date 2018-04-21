using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

	public float maxSpeed, initialSpeed, jumpVelocity, speedDecrease, fallMultiplier, lowJumpMultiplier;

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
		myRb.velocity = Vector2.up*jumpVelocity;
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
