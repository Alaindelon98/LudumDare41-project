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
        if (col.gameObject.tag == "Enemy")
        {
            GameManagerScript.ChangePlayerState(GameManagerScript.GameState.Dead);
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
                    myPlayerScript.Reverse();

                    break;      
            }
        }
		if (theseColType == ColType.Body) 
		{
			if(col.gameObject.tag == "WallJump")
			{
				myPlayerScript.WallJump();
			}
		}

		if (col.gameObject.tag == "Coin") 
		{
			GameManagerScript.PlayerMoney += GameManagerScript.moneyFromCoin;

			Destroy (col.gameObject);
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
