using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColScript : MonoBehaviour {

    public enum ColType { Legs, Head, Body };
    public ColType theseColType;
    private playerScript myPlayerScript;

	// Use this for initialization
	void Start ()
    {
        myPlayerScript = gameObject.GetComponentInParent<playerScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
	void OnCollisionEnter2D(Collision2D col)
    {
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            GameManagerScript.PlayerDeath();
        }
        if (theseColType == ColType.Legs)
        {
            switch (col.tag)
            {
                case "Jump":
                   myPlayerScript.Jump();
                    break;
                case "Sprint":
                    myPlayerScript.Sprint();
                    break;
                case "Crouch":
                    myPlayerScript.Crouch();
                    break;
                case "Reverse":
                    myPlayerScript.Change();
                    break;
                case "WallJump":
                    myPlayerScript.WallJump();
                    break;


            }
        }
		if (col.gameObject.tag == "Coin") 
		{
			GameManagerScript.PlayerMoney += GameManagerScript.moneyFromCoin;

			Destroy (col.gameObject);
		}

    }
}
