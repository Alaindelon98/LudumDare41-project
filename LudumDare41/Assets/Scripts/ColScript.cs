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
    void OnCollisionEnter2D()
    {

    }
}
