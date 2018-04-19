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
	

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("Score", 0);
		PlayerPrefs.SetInt("Streak", 0);
		PlayerPrefs.SetInt("Mult", 1);
		PlayerPrefs.SetInt("Note Touch", 0);
	}
	
	// Update is called once per frame
	void Update () {
		
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
		if (streak >= valuetogetstreak2)
			multiplier = 4;
		if (streak >= valuetogetstreak3)
			multiplier = 8;
		
		PlayerPrefs.SetInt("Note Touch", PlayerPrefs.GetInt("Note Touch")+1);
		UptadeGUI();
	}

	public void ResetStreak()
	{
		streak = 0;
		multiplier = 1;
		UptadeGUI();
	}

	public void Win()
	{
		SceneManager.GetSceneByName("SceneMusicalWIn");
	}

	void UptadeGUI()
	{
		PlayerPrefs.SetInt("Streak", streak);
		PlayerPrefs.SetInt("Mult", multiplier);
		PlayerPrefs.SetInt("Mult", 1);
		
	}
	
}
