using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUImanager : MonoBehaviour {

    public Text MoneyCounter;
    public Text totalRuns, actualDistance, MaxDist, moneyLastRun;
    public Text jumpPrice, sprintPrice, reversePrice, wallJumpPrice;
    public GameObject winPanel;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        MoneyCounter.text = GameManagerScript.PlayerMoney.ToString();

        totalRuns.text = "Generations : " + GameManagerScript.totalRuns.ToString();

        actualDistance.text  ="Current Distance : "+ Mathf.FloorToInt (GameManagerScript.totalPlayerDistance);
        MaxDist.text = "Total Distance : " + +Mathf.FloorToInt(GameManagerScript.totalGamePlayerDistance);

        moneyLastRun.text = "Last Run Money :"+GameManagerScript.moneyLastRun;

        jumpPrice.text = GameManagerScript.jumpPrice.ToString();
        sprintPrice.text = GameManagerScript.sprintPrice.ToString();
        reversePrice.text = GameManagerScript.reversePrice.ToString();
        wallJumpPrice.text = GameManagerScript.wallJumpPrice.ToString();



    }
    public void ChangeCameraLook(bool lockonPlayer)
    {
        GameManagerScript.mainCamera.lockOnPlayer = lockonPlayer;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            winPanel.SetActive(true);
        }
    }
}
