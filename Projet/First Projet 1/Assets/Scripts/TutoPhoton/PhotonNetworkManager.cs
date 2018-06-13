using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.AI;
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
	private int[] EnabledHealth;
	
	//Game Parameters
	private bool PlayingLocal;
	private float Life;
	private float Timer;
	private float DamageLevel;
	private int LevelSuceeded;
	private Dictionary<string, KeyCode> DictionaryFirst;
	private Dictionary<string, KeyCode> DictionarySecond;
	private Dictionary<string, KeyCode> DictionaryOne;
	
	//Hidden
	private bool Over;
	private int MusiqueLevel;
	private string NameRoom;

	public string GetNameRoom
	{
		get { return NameRoom; }
		set { NameRoom = value; }
	}

	public Dictionary<string, KeyCode> GetDictionaryOne
	{
		get { return DictionaryOne; }
	}

	public Dictionary<string, KeyCode> GetDictionaryFirst
	{
		get { return DictionaryFirst; }
	}

	public Dictionary<string, KeyCode> GetDictionarySecond
	{
		get { return DictionarySecond; }
	}
	
	public bool IsPlayingLocal
	{
		get { return PlayingLocal; }
		set { PlayingLocal = value; }
	}
	
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
		NameRoom = GameObject.Find("RoomName").GetComponent<RoomName>().GetNomRoom;
		Over = false;
		MusiqueLevel = 1;
		GameOverCanvas = GameObject.Find("GameLogic/GameOverCanvas");
		EscapeCanvas = GameObject.Find("GameLogic/EscapeCanvas");
		Timer = 3f;
		DamageLevel = GameObject.Find("GameParameters").GetComponent<GameParameters>().DamageFallOfThisLevel;
		LevelSuceeded = 5;
		
		PhotonNetwork.automaticallySyncScene = true;

		if (!PhotonNetwork.connected)
		{
			PhotonNetwork.ConnectUsingSettings(NameRoom);
		}
		
		EnabledHealth = new int[2];
		EnabledHealth[0] = -1;
		EnabledHealth[1] = -1;
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
		
		DictionaryFirst = new Dictionary<string, KeyCode>();
		DictionaryFirst.Add("Up", KeyCode.UpArrow);
		DictionaryFirst.Add("Down", KeyCode.DownArrow);
		DictionaryFirst.Add("Left", KeyCode.LeftArrow);
		DictionaryFirst.Add("Right", KeyCode.RightArrow);
		DictionaryFirst.Add("Jump", KeyCode.RightControl);
		
		DictionarySecond = new Dictionary<string, KeyCode>();
		DictionarySecond.Add("Jump", KeyCode.Space);
		DictionarySecond.Add("Up", KeyCode.Z);
		DictionarySecond.Add("Left", KeyCode.Q);
		DictionarySecond.Add("Right", KeyCode.D);
		DictionarySecond.Add("Down", KeyCode.S);
		
		DictionaryOne = new Dictionary<string, KeyCode>();
		DictionaryOne.Add("Jump", KeyCode.Space);
		DictionaryOne.Add("Up", KeyCode.UpArrow);
		DictionaryOne.Add("Left", KeyCode.LeftArrow);
		DictionaryOne.Add("Right", KeyCode.RightArrow);
		DictionaryOne.Add("Down", KeyCode.DownArrow);
		
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

		//if (newScene.name == "Menu principal" || newScene.name == "Menu without logic")
		{
		//	IsPlayingLocal = false;
		}
		
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

		if (PlayingLocal)
		{
			
			if (newScene.name == "Menu without logic")
			{
				PhotonNetwork.Instantiate(PrefabGirl.name, SpawnPoint1.position, SpawnPoint1.rotation, 0);
				GameObject[] G = newScene.GetRootGameObjects();
				foreach (GameObject g in G)
				{
					if (g.name == "CanvasFirstPlayer")
						g.GetComponent<Canvas>().enabled = true;
				}
			}
			else
			{
				PhotonNetwork.Instantiate(PrefabGirl.name, SpawnPoint1.position, SpawnPoint1.rotation, 0);
				PhotonNetwork.Instantiate(PrefabBoy.name, SpawnPoint2.position, SpawnPoint2.rotation, 0);
			}
		}
		else
		{
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
						PhotonNetwork.Instantiate(PrefabBoy.name, SpawnPoint2.position, SpawnPoint2.rotation, 0);
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
						PhotonNetwork.Instantiate(PrefabGirl.name, SpawnPoint1.position, SpawnPoint1.rotation, 0);
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
	}

	public void OnJoinedLobby()
	{
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = 2;
		PhotonNetwork.JoinOrCreateRoom(NameRoom, roomOptions, null);
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
				PlayingLocal = false;
				animator.SetTrigger("FadeOut");
			}
		}
		
		if (SceneManager.GetActiveScene().name == "Menu principal" || SceneManager.GetActiveScene().name == "Menu without logic")
			PlayingLocal = GameObject.Find("CanvasFirstPlayer/Background/PlayingLocal").GetComponent<Toggle>().isOn;
	}
	
	private void HealthGov()
    	{
    		if (Life > 100)
    		{
    			EnabledHealth[0] = -1;
			    EnabledHealth[1] = -1;
		    }
    		else
		    {
			    if (SceneManager.GetActiveScene().name != "Menu principal" &&
			        SceneManager.GetActiveScene().name != "Menu without logic" && SceneManager.GetActiveScene().buildIndex != 7
			        && SceneManager.GetActiveScene().buildIndex != 8 && SceneManager.GetActiveScene().buildIndex != 9
			        && SceneManager.GetActiveScene().buildIndex != 10)
			    {
				    EnabledHealth[0] = 0;
				    print("HB enlevee");
			    }
			    else
				    return;
    			if (Life <= 0)
    			{
    				EnabledHealth[1] = -1;
    			}
			    else
			    {
				    if (Life > 0)
					    EnabledHealth[1] = 1;
				    if (Life > 10)
					    EnabledHealth[1] = 2;
				    if (Life > 20)
					    EnabledHealth[1] = 3;
				    if (Life > 30)
					    EnabledHealth[1] = 4;
				    if (Life > 40)
					    EnabledHealth[1] = 5;
				    if (Life > 50)
					    EnabledHealth[1] = 6;
				    if (Life > 60)
					    EnabledHealth[1] = 7;
				    if (Life > 70)
					    EnabledHealth[1] = 8;
				    if (Life > 80)
					    EnabledHealth[1] = 9;
				    if (Life > 90)
					    EnabledHealth[1] = 10;
			    }
		    }
    
    		if (EnabledHealth[0] == -1)
    			HealthBar[0].enabled = false;
    		else
    			HealthBar[0].enabled = true;
    		int j = 1;
    		while (j < HealthBar.Count)
    		{
    			if (j == EnabledHealth[1])
    				HealthBar[j].enabled = true;
    			else
    				HealthBar[j].enabled = false;
    			j++;
    		}
    	}
}
