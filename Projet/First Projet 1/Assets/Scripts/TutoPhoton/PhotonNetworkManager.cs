using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Experimental.Rendering;
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
	private List<Canvas> HealthBar;
	
	//Game Parameters
	private float Life;
	private float Timer;
	private float DamageLevel;
	private int LevelSuceeded;
	
	//Hidden
	private bool Over;
	private int MusiqueLevel;

	public int GetLevelSuceeded
	{
		get { return LevelSuceeded; }
		set { LevelSuceeded = value; }
	}
	
	public int GetMusiqueLevel
	{
		get { return MusiqueLevel; }
		set { MusiqueLevel = value; }
	}
	
	public bool IsOver
	{
		get { return Over; }
		set { Over = value; }
	}
	
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
		Timer = 3f;
		DamageLevel = GameObject.Find("GameParameters").GetComponent<GameParameters>().DamageFallOfThisLevel;
		LevelSuceeded = 1;
		
		PhotonNetwork.automaticallySyncScene = true;

		if (!PhotonNetwork.connected)
		{
			PhotonNetwork.ConnectUsingSettings("0.1");
		}
	
		HealthBar = new List<Canvas>();
		HealthBar.Add(GameObject.Find("GameLogic/HealthBar/Health 0").GetComponent<Canvas>());
		HealthBar.Add(GameObject.Find("GameLogic/HealthBar/Health 0.5").GetComponent<Canvas>());
		HealthBar.Add(GameObject.Find("GameLogic/HealthBar/Health 1").GetComponent<Canvas>());
		HealthBar.Add(GameObject.Find("GameLogic/HealthBar/Health 1.5").GetComponent<Canvas>());
		HealthBar.Add(GameObject.Find("GameLogic/HealthBar/Health 2").GetComponent<Canvas>());
		HealthBar.Add(GameObject.Find("GameLogic/HealthBar/Health 2.5").GetComponent<Canvas>());
		HealthBar.Add(GameObject.Find("GameLogic/HealthBar/Health 3").GetComponent<Canvas>());
		HealthBar.Add(GameObject.Find("GameLogic/HealthBar/Health 3.5").GetComponent<Canvas>());
		HealthBar.Add(GameObject.Find("GameLogic/HealthBar/Health 4").GetComponent<Canvas>());
		HealthBar.Add(GameObject.Find("GameLogic/HealthBar/Health 4.5").GetComponent<Canvas>());
		HealthBar.Add(GameObject.Find("GameLogic/HealthBar/Health 5").GetComponent<Canvas>());
		//length = 11
		
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
		
		GameOverCanvas.GetComponent<Canvas>().enabled = false;
		
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

		}
	}


	void Update()
	{
		HealthGov();
		if (GameOverCanvas.GetComponent<Canvas>().enabled)
		{
			Over = true;
			Timer -= Time.deltaTime;
			if (Timer <= 0)
			{
				Timer = 3f;
				Animator animator = GameObject.Find("FadeTransition").GetComponent<Animator>();
				animator.SetTrigger("FadeOut");
			}
		}
	}
	
	private void HealthGov()
	{
		if (Life > 100)
		{
			HealthBar[0].enabled = false;
		}
		else
		{
			if (Life <= 0)
			{
				HealthBar[1].enabled = false;
			}
			else
			{
				HealthBar[0].enabled = true;
				if (Life > 90)
				{
					HealthBar[10].enabled = true;
				}
				else
				{
					if (Life > 80)
					{
						HealthBar[9].enabled = true;
						HealthBar[10].enabled = false;
					}
					else
					{
						if (Life > 70)
						{
							HealthBar[8].enabled = true;
							HealthBar[9].enabled = false;
						}
						else
						{
							if (Life > 60)
							{
								HealthBar[7].enabled = true;
								HealthBar[8].enabled = false;
							}
							else
							{
								if (Life > 50)
								{
									HealthBar[6].enabled = true;
									HealthBar[7].enabled = false;
								}
								else
								{
									if (Life > 40)
									{
										HealthBar[5].enabled = true;
										HealthBar[6].enabled = false;
									}
									else
									{
										if (Life > 30)
										{
											HealthBar[4].enabled = true;
											HealthBar[5].enabled = false;
										}
										else
										{
											if (Life > 20)
											{
												HealthBar[3].enabled = true;
												HealthBar[4].enabled = false;
											}
											else
											{
												if (Life > 10)
												{
													HealthBar[2].enabled = true;
													HealthBar[3].enabled = false;
												}
												else
												{
													HealthBar[1].enabled = true;
													HealthBar[2].enabled = false;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}
}
