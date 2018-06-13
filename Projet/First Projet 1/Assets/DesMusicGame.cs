using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.Collections;
using UnityEngine.SceneManagement;

public class DesMusicGame : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		if (GameObject.Find("GameLogic"))
			GameObject.Find("GameLogic").GetComponentInChildren<AudioSource>().mute = true;

		SceneManager.activeSceneChanged += UnmuteMusic;
	}

	private void UnmuteMusic(Scene prev, Scene next)
	{
		if (next.name == "Menu without logic")
			GameObject.Find("GameLogic").GetComponentInChildren<AudioSource>().mute = false;
	}
}
