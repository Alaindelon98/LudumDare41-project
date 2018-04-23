using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{


	public float speed;
    private Vector3 originalPos;
	public int direction=1;
    private int originalDirection;
    public SpriteRenderer sprite;
    private bool originalOrientation;

    

    // Use this for initialization

    // Use this for initialization
    void Start () {
        originalPos = transform.position;
        originalDirection=direction;
        originalOrientation = sprite.flipX;

    }

    // Update is called once per frame
    void Update () 
	{
		Mov ();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Limite")
        {
			direction *= -1;
            Vector3 scale = transform.localScale;
            if (sprite.flipX == true)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
           
		}
	}

	private void Mov()
	{
		transform.position += new Vector3 (direction*speed*Time.deltaTime,0,0);
	}
    public void ResetEnemy()
    {
        direction = originalDirection;
        transform.position = originalPos;
        sprite.flipX = originalOrientation;
    }

}
