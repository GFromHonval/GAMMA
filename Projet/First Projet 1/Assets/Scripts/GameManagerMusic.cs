using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerMusic : MonoBehaviour
{
	public int multiplier = 1;
	public int streak = 0;
	public int valuetogetstreak1;
	public int valuetogetstreak2;
	public int valuetogetstreak3;
	public int notetotales;
	public int notestowin;

	private int Notes;
	private Canvas CanvasWin;
	private Canvas CanvasLose;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("Score", 0);
		PlayerPrefs.SetInt("Streak", 0);
		PlayerPrefs.SetInt("Mult", 1);
		PlayerPrefs.SetInt("Note Touch", 0);

		CanvasWin = GameObject.Find("MessageFin").GetComponent<MessageMusique>().Win;
		CanvasLose = GameObject.Find("MessageFin").GetComponent<MessageMusique>().Lose;

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameObject.Find("PrefabAnimatedBoy(Clone)/Camera") != null)
			Destroy(GameObject.Find("PrefabAnimatedBoy(Clone)"));
		if (GameObject.Find("PrefabAnimatedGirl(Clone)/Camera") != null)
			Destroy(GameObject.Find("PrefabAnimatedGirl(Clone)"));
		
		ActivatorScript[] Childrens = GameObject.Find("Active").GetComponentsInChildren<ActivatorScript>();
		int NotesValided = 0;
		
		foreach (var c in Childrens)
		{
			NotesValided += c.GetCountActivated;
		}

		Notes = GameObject.Find("Destroyer").GetComponent<Destroy>().GetCountDestroyed + NotesValided;

		
		if (Notes == notetotales)
		{
			GameObject.Find("Musique").GetComponent<AudioSource>().mute = true;
			if (NotesValided >= notestowin)
				Win();
			else
				Lose();
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		ResetStreak();
	}

	public int GetScore()
	{
		return 100 * multiplier;
	}

	public void AddStreak()
	{

		streak++;
		if (streak >= valuetogetstreak1)
			multiplier = 2;
			PlayerPrefs.SetInt("Mult", 2);
		if (streak >= valuetogetstreak2)
			multiplier = 4;
			PlayerPrefs.SetInt("Mult", 4);
		if (streak >= valuetogetstreak3)
			multiplier = 8;
			PlayerPrefs.SetInt("Mult", 8);
		
		PlayerPrefs.SetInt("Note Touch", PlayerPrefs.GetInt("Note Touch")+1);
		UptadeGUI();
	}

	public void ResetStreak()
	{
		streak = 0;
		multiplier = 1;
		PlayerPrefs.SetInt("Mult", 1);
		UptadeGUI();
	}

	public void Win()
	{
		CanvasWin.enabled = true;
		GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>().GetLevelSuceeded = GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>().GetMusiqueLevel + 1;
		GetBackToMenu();
	}

	public void Lose()
	{
		CanvasLose.enabled = true;
		GetBackToMenu();
	}

	private void GetBackToMenu()
	{
		GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>().IsOver = true;
		Animator animator = GameObject.Find("FadeTransition").GetComponent<Animator>();
		animator.SetTrigger("FadeOut");
	}

	void UptadeGUI()
	{
		PlayerPrefs.SetInt("Streak", streak);
		PlayerPrefs.SetInt("Mult", multiplier);
	}
	
}
