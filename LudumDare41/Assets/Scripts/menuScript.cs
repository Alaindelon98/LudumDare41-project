using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class menuScript : MonoBehaviour

{

    public GameObject MenuPanel,winPanel;

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Scene")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (MenuPanel.gameObject.activeSelf)
                {
                    MenuPanel.SetActive(false);
                }
                else
                {
                    MenuPanel.SetActive(true);
                }
            }
        }
    }

	public void loadGame()
    {
		SceneManager.LoadScene (1);
	}

	public void quit(){
		Application.Quit ();
	}

    
}
