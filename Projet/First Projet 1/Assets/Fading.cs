using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fading : MonoBehaviour
{

	public Animator animator;

	public void ChangeBoolValue()
	{
		if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().name == "Menu without logic")
		{
			GameObject.Find("CanvasFirstPlayer/Background").GetComponent<MenuActions>().ChangeLevel();
		}
		else
		{
			PhotonNetworkManager photonNetworkManager = GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>();
			
			if (PhotonNetwork.isMasterClient)
			{
				if (photonNetworkManager.IsOver)
				{
					photonNetworkManager.IsOver = false;
					PhotonNetwork.LoadLevel("Menu without logic");
				}
				else
				{
					PhotonNetwork.LoadLevel("Niveau musical " + photonNetworkManager.GetMusiqueLevel);
				}
			}
		}
	}

}
