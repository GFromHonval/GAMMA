using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngineInternal;

public class PhotonNetworkManager : Photon.MonoBehaviour
{

	//Prefabs
	[SerializeField] private GameObject PrefabGirl;
	[SerializeField] private GameObject PrefabBoy;
	
	//Physics Variables
	private Transform SpawnPoint1;
	private Transform SpawnPoint2;
	private GameObject LobbyCamera;
	
	//Canvas
	private GameObject GameOverCanvas;
	private GameObject EscapeCanvas;
	
	//Game Parameters
	private float Life;
	private float Timer;

	public float GetLife
	{
		get { return Life; }
		set { Life = value; }
	}
	
	public GameObject GetGameOverCanvas
	{
		get { return GameOverCanvas; }
		set { GameOverCanvas = value; }
	}
	
	public GameObject GetEscapeCanvas
	{
		get { return EscapeCanvas; }
		set { EscapeCanvas = value; }
	}
	
	public void Awake()
	{
		GameOverCanvas = GameObject.Find("GameLogic/GameOverCanvas");
		EscapeCanvas = GameObject.Find("GameLogic/EscapeCanvas");
		Timer = 2f;
		
		PhotonNetwork.automaticallySyncScene = true;

		if (!PhotonNetwork.connected)
		{
			PhotonNetwork.ConnectUsingSettings("0.1");
		}

		SceneManager.activeSceneChanged += OnLoadScene;
	}
	
	[PunRPC]
	private void OnLoadScene(Scene previousScene, Scene newScene)
	{
		GameParameters gameParameters = GameObject.Find("GameParameters").GetComponent<GameParameters>();
		SpawnPoint1 = gameParameters.RespawnPoint1;
		SpawnPoint2 = gameParameters.RespawnPoint2;
		LobbyCamera = GameObject.Find("LobbyCamera");
		Life = gameParameters.LifeInThisLevel;
		
		string MasterName = "";
		foreach (PhotonPlayer player in PhotonNetwork.playerList)
		{
			if (player.IsMasterClient)
			{
				MasterName = player.NickName;
			}
		}

		GameObject ToDestroy1 = GameObject.FindGameObjectWithTag("PlayerBoy");
		if (ToDestroy1 != null)
			PhotonNetwork.Destroy(ToDestroy1);
		GameObject ToDestroy2 = GameObject.FindGameObjectWithTag("PlayerGirl");
		if (ToDestroy2 != null)
			PhotonNetwork.Destroy(ToDestroy2);
		
		if (PhotonNetwork.player.IsMasterClient)
		{
			if (PhotonNetwork.player.NickName == "FirstPlayerGirl")
			{
				PhotonNetwork.Instantiate(PrefabGirl.name, SpawnPoint1.position, SpawnPoint1.rotation, 0);
			}
			else
			{
				if (PhotonNetwork.player.NickName == "FirstPlayerBoy")
				{
					PhotonNetwork.Instantiate(PrefabBoy.name, SpawnPoint1.position, SpawnPoint1.rotation, 0);
				}
			}
		}
		else
		{
			if (MasterName == "FirstPlayerGirl")
			{
				PhotonNetwork.Instantiate(PrefabBoy.name, SpawnPoint2.position, SpawnPoint2.rotation, 0);
				PhotonNetwork.player.NickName = "SecondPlayerBoy";
			}
			else
			{
				if (MasterName == "FirstPlayerBoy")
				{
					PhotonNetwork.Instantiate(PrefabGirl.name, SpawnPoint2.position, SpawnPoint2.rotation, 0);
					PhotonNetwork.player.NickName = "SecondPlayerGirl";
				}
			}
		}
		
		if (newScene.name == "Menu without logic")
		{
			GameObject[] G = newScene.GetRootGameObjects();
			if (PhotonNetwork.player.NickName == "FirstPlayerGirl" || PhotonNetwork.player.NickName == "FirstPlayerBoy")
			{
				foreach (GameObject g in G)
				{
					if (g.name == "CanvasFirstPlayer")
						g.GetComponent<Canvas>().enabled = true;
				}
			}
			else
			{
				foreach (GameObject g in G)
				{
					if (g.name == "CanvasSecondPlayer")
						g.GetComponent<Canvas>().enabled = true;
				}
			}
		}
	}

	public void OnJoinedLobby()
	{
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = 2;
		PhotonNetwork.JoinOrCreateRoom("Gamma", roomOptions, null);
	}

	public virtual void OnJoinedRoom()
	{
		if (PhotonNetwork.isMasterClient)
		{		
			GameObject.Find("CanvasFirstPlayer").GetComponent<Canvas>().enabled = true;
			PhotonNetwork.Instantiate(PrefabGirl.name, SpawnPoint1.position, SpawnPoint1.rotation, 0);
			PhotonNetwork.playerName = "FirstPlayer";
		}
		else
		{
			GameObject.Find("CanvasSecondPlayer").GetComponent<Canvas>().enabled = true;
			PhotonNetwork.Instantiate(PrefabBoy.name, SpawnPoint2.position, SpawnPoint2.rotation, 0);
			PhotonNetwork.playerName = "SecondPlayer";
		}

		LobbyCamera.SetActive(false);
	}

	[PunRPC]
	private void OnGUI()
	{
		if (PhotonNetwork.connected)
		{
			GUILayout.Label(PhotonNetwork.playerName);

			GUILayout.Label(PhotonNetwork.player.isMasterClient.ToString());

			GUILayout.Label(Life.ToString());
		}
	}

	
	void Update()
	{
		if (PhotonNetwork.playerList.Length == 2)
		{
			GameObject PlayerBoy = GameObject.FindGameObjectWithTag("PlayerBoy");
			GameObject PlayerGirl = GameObject.FindGameObjectWithTag("PlayerGirl");
			float Life1 = PlayerBoy.GetComponent<RotationPlayer>().LifePerso;
			float Life2 = PlayerGirl.GetComponent<RotationPlayer>().LifePerso;
			Life = Math.Min(Life1, Life2);
		}
		
		if (GameOverCanvas.GetComponent<Canvas>().enabled && PhotonNetwork.isMasterClient)
		{
			Timer -= Time.deltaTime;
			if (Timer <= 0)
			{
				GameOverCanvas.GetComponent<Canvas>().enabled = false;
				Timer = 2f;
				PhotonNetwork.LoadLevel("Menu without logic");
			}
		}
	}
}
