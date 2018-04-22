using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioSource _die;
	public AudioSource _coin;
	public AudioSource _activate;
	public AudioSource _end;
	public AudioSource _shop;

	public static AudioSource die;
	public static AudioSource coin;
	public static AudioSource activate;
	public static AudioSource end;
	public static AudioSource shop;


	// Use this for initialization
	void Start () {
		die = _die;
		coin = _coin;
		activate = _activate;
		end = _end;
		shop = _shop;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
