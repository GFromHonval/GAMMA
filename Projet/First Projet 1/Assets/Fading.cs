using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fading : MonoBehaviour
{

	public Animator animator;
	private bool FinishFade;

	public bool IsFinish
	{
		get { return FinishFade; }
		set { FinishFade = value; }
	}
	
	public void ChangeBoolValue()
	{
		if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().name == "Menu without logic")
		{
			GameObject.Find("CanvasFirstPlayer/Background").GetComponent<MenuActions>().ChangeLevel();
		}
		else
		{
			if (PhotonNetwork.isMasterClient)
				PhotonNetwork.LoadLevel("Menu without logic");
		}
	}

}
