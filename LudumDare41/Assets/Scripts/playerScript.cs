using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

	public float maxSpeed, initialSpeed, jumpVelocity, speedDecrease;

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
        }
    }
}
