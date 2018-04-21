using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{


	public float speed;
    private Vector3 originalPos;
	public int direction=1;
    private int originalDirection;

	// Use this for initialization
	void Start () {
		this.enabled = false;
        originalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Mov ();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
        direction *= -1;
	}

	private void Mov()
	{
		transform.position += new Vector3 (direction*speed*Time.deltaTime,0,0);
		//saltito 
	}
    public void ResetEnemy()
    {
        direction = originalDirection;
        transform.position = originalPos;
    }

}
