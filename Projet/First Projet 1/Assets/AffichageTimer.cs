using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AffichageTimer : MonoBehaviour
{

	private Timer Timer;
	private Text TimeText;
		
	// Use this for initialization
	void Start ()
	{
		Timer = GameObject.Find("GameLogic").GetComponent<Timer>();
		TimeText = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (SceneManager.GetActiveScene().name == "MAP 1 Texturisé" || SceneManager.GetActiveScene().name ==
		                                                            "Map 2 texturisée"
		                                                            || SceneManager.GetActiveScene().name ==
		                                                            "Map 3 texturisée"
		                                                            || SceneManager.GetActiveScene().name ==
		                                                            "MAP 4 TEXTURISE")
		{
			GetComponent<Canvas>().enabled = true;
		}
		else
			GetComponent<Canvas>().enabled = false;

		int _time = (int) Timer.GetTime;
		TimeText.text = _time.ToString();

	}
}
