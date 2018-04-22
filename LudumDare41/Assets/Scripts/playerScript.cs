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
	private float counter;
	private Vector3 currentPosition;

	Animator anim;

	public enum animStates
	{
		Running,
		Jump,
		Falling, 
		Land, 
		RunningToCrouch, 
		CrouchToRunning
	}

	private animStates currentState;
	private animStates newState;


	// Use this for initialization
	void Start () 
	{
        spawnPosition = transform.position;
		myRb = GetComponent<Rigidbody2D> ();
		speed = initialSpeed;
		jumpSpeed = jumpMultiplier * speed;

		newState = animStates.Running;
		currentState = newState;
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
		if (currentPosition == transform.position) {
			counter += Time.deltaTime;
		} else {
			counter = 0;
		}
		currentPosition = transform.position;
		if (counter >= 3) {
            GameManagerScript.ChangePlayerState(GameManagerScript.GameState.Dead);
        }

		if (myRb.velocity.y <= 0f) {
			changeState (animStates.Falling);
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

		changeState (animStates.Jump);
        
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
		changeState (animStates.RunningToCrouch);
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
	void changeState(animStates newState)
	{
		currentState = newState;

		if (newState == animStates.Running) {
			anim.SetBool ("Running", true);
			anim.SetBool ("Jump", false);
			anim.SetBool ("Falling", false);
			anim.SetBool ("Land", false);
			anim.SetBool ("RunningToCrouch", false);
			anim.SetBool ("Crouch", false);
			anim.SetBool ("CrouchToRunning", false);
		}

		else if (newState == animStates.Jump) {
			anim.SetBool ("Jump", true);
			anim.SetBool ("Running", false);
			anim.SetBool ("Falling", false);
			anim.SetBool ("Land", false);
			anim.SetBool ("RunningToCrouch", false);
			anim.SetBool ("Crouch", false);
			anim.SetBool ("CrouchToRunning", false);
		}

		else if (newState == animStates.Falling) {
			anim.SetBool ("Running", false);
			anim.SetBool ("Jump", false);
			anim.SetBool ("Falling", true);
			anim.SetBool ("Land", false);
			anim.SetBool ("RunningToCrouch", false);
			anim.SetBool ("Crouch", false);
			anim.SetBool ("CrouchToRunning", false);
		}

		else if (newState == animStates.Land) {
			anim.SetBool ("Running", false);
			anim.SetBool ("Jump", false);
			anim.SetBool ("Falling", false);
			anim.SetBool ("Land", true);
			anim.SetBool ("RunningToCrouch", false);
			anim.SetBool ("Crouch", false);
			anim.SetBool ("CrouchToRunning", false);
		}

		else if (newState == animStates.RunningToCrouch) {
			anim.SetBool ("Running", false);
			anim.SetBool ("Jump", false);
			anim.SetBool ("Falling", false);
			anim.SetBool ("Land", false);
			anim.SetBool ("RunningToCrouch", true);
			anim.SetBool ("Crouch", false);
			anim.SetBool ("CrouchToRunning", false);
		}

		else if (newState == animStates.CrouchToRunning) {
			anim.SetBool ("Running", false);
			anim.SetBool ("Jump", false);
			anim.SetBool ("Falling", false);
			anim.SetBool ("Land", false);
			anim.SetBool ("RunningToCrouch", false);
			anim.SetBool ("Crouch", false);
			anim.SetBool ("CrouchToRunning", true);
		}
	}
}
