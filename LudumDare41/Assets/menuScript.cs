using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class menuScript : MonoBehaviour {

	public void loadGame(){
		SceneManager.LoadScene (1);
	}

	public void quit(){
		Application.Quit ();
	}
}
