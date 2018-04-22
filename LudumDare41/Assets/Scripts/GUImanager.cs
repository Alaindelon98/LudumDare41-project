using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUImanager : MonoBehaviour {

    public Text MoneyCounter;
    public Text totalRuns, actualDistance, MaxDist, moneyLastRun;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoneyCounter.text = GameManagerScript.PlayerMoney.ToString();

        totalRuns.text = "Generations : " + GameManagerScript.totalRuns.ToString();

        actualDistance.text  ="Actual Distance : "+ Mathf.FloorToInt (GameManagerScript.totalPlayerDistance);
        MaxDist.text = "Total Distance : " + +Mathf.FloorToInt(GameManagerScript.totalGamePlayerDistance);

        moneyLastRun.text = "Last Run Money :"+GameManagerScript.moneyLastRun;


    }
    public void ChangeCameraLook(bool lockonPlayer)
    {
        GameManagerScript.mainCamera.lockOnPlayer = lockonPlayer;
    }
}
