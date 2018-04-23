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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Castle")
        {
            GameManagerScript.ChangePlayerState(GameManagerScript.GameState.Win);
        }

        if (col.gameObject.tag == "Enemy" && myPlayerScript.gameObject.activeSelf )
        {
            
            GameManagerScript.ChangePlayerState(GameManagerScript.GameState.Dead);
           
        }
        if (theseColType == ColType.Legs)
        {
            switch (col.tag)
            {
			case "Jump":
				GameManagerScript.active.Play ();
                   myPlayerScript.Jump();
                    break;
			case "Sprint":
				GameManagerScript.active.Play ();
                    myPlayerScript.Sprint();
                    break;
			case "Crouch":
				GameManagerScript.active.Play ();
                    myPlayerScript.Crouch();
                    break;
			case "Reverse":
				GameManagerScript.active.Play ();
                    myPlayerScript.Reverse();
                    break;      
            }
        }
		if (theseColType == ColType.Body) 
		{
			if(col.gameObject.tag == "WallJump")
			{
				GameManagerScript.active.Play ();
				myPlayerScript.WallJump();
			}
		}

		if (col.gameObject.tag == "Coin") 
		{
			GameManagerScript.coin.Play ();
			GameManagerScript.PlayerMoney += GameManagerScript.moneyFromCoin;
			GameManagerScript.takenCoins.Add (col.gameObject);
			col.gameObject.SetActive (false);
			//Destroy (col.gameObject);
		}

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (theseColType == ColType.Legs)
        {
            myPlayerScript.grounded = true;

        }
    }
}
