using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VolumeChangerEscape : MonoBehaviour {


	void Update () {

		if (SceneManager.GetActiveScene().name != "Menu principal" &&
		    SceneManager.GetActiveScene().name != "Menu without logic")
		{
			
			AudioSource Musique;
			Musique = GameObject.Find("GameLogic").GetComponentInChildren<AudioSource>();
			Musique.volume = GetComponent<Scrollbar>().value * 0.2f;
		}
	}
}
