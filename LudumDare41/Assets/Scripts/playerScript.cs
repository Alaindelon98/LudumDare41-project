using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

	public float maxSpeed, initialSpeed, jumpVelocity, speedDecrease, fallMultiplier, lowJumpMultiplier, jumpMultiplier;

    public Vector3 spawnPosition;
    public bool grounded;
	public float speed;
	private Rigidbody2D myRb;
	private float jumpSpeed;
	private float counter;
	private Vector3 currentPosition;

    public float direction;

    public float initialDirection;

    Animator anim;
    public ParticleSystem blood;

	

	// Use this for initialization
	void Start () 
	{
        spawnPosition = transform.position;
		myRb = GetComponent<Rigidbody2D> ();
        anim = GetComponentInChildren<Animator>();
		speed = initialSpeed;
		jumpSpeed = jumpMultiplier * speed;
        direction = initialDirection;

    }
	
	// Update is called once per frame
	void Update () 
	{
		Move ();

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
		if (currentPosition == transform.position) {
			counter += Time.deltaTime;
		} else {
			counter = 0;
		}
		currentPosition = transform.position;
		if (counter >= 3) {
            GameManagerScript.ChangePlayerState(GameManagerScript.GameState.Dead);
        }


        PassParameters();
    }
    public void Move()
	{
		//myRb.velocity = new Vector2 (speed, myRb.velocity.y);
		myRb.velocity = new Vector2 (direction* speed, myRb.velocity.y);
	}

    public void Jump()
	{
		speed = jumpSpeed;
		grounded = false;
		//myRb.velocity =new Vector2( speed,jumpVelocity);
		myRb.velocity =new Vector2(direction * speed,jumpVelocity);
	}

    public void Sprint()
	{
		if (speed <= maxSpeed) {
			speed = maxSpeed;
		} 

		//myRb.velocity = new Vector2 (speed, myRb.velocity.y);
		myRb.velocity = new Vector2 (direction* speed, myRb.velocity.y);
	}

    public void Crouch()
	{
		//changeState (animStates.RunningToCrouch);
	}

    public void Reverse()
	{

        direction *= -1;
		//speed *=-1;
        Flip();
	}

    void Flip()
    {
        Vector3 theScale = transform.localScale;

        theScale.x *= -1;

        transform.localScale = theScale;

		/*print ("changeee");
			if (item.flipX == false) {
				
				item.flipX = true;
			} else if (item.flipX == true) {
				item.flipX = false;
			}
		}*/
    }
    public void WallJump()
	{
		Jump ();
        //Reverse();
		direction *= -1;
		//speed *=-1;
		Flip ();
	}

    void PassParameters()
    {
        anim.SetFloat("xSpeed", myRb.velocity.x);
        anim.SetFloat("ySpeed", myRb.velocity.y);
        anim.SetBool("Grounded", grounded);
        //anim.SetBool("Crouch", currentState == )
    }

    
	
}
