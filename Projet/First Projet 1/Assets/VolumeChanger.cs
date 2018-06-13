using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
	void Update()
	{
		AudioSource Musique;
		Musique = GameObject.Find("GameLogic").GetComponentInChildren<AudioSource>();
		Musique.volume = GetComponent<Scrollbar>().value * 0.2f;
	}
}