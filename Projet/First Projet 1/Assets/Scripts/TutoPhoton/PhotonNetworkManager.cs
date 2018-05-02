using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
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
	
	//Prefab Choice
	private string PrefabFirstPlayer;

	public GameObject GetPrefabBoy
	{
		get { return PrefabBoy; }
	}

	public GameObject GetPrefabGirl
	{
		get { return PrefabGirl; }
	}

	public string GetPrefabFirstPlayer
	{
		get { return PrefabFirstPlayer; }
		set { PrefabFirstPlayer = value; }
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
		LobbyCamera = GameObject.Find("LobbyCamera");
		
		GameOverCanvas.SetActive(false);
		EscapeCanvas.SetActive(false);		
		
		GameParameters gameParameters = GameObject.Find("GameParameters").GetComponent<GameParameters>();
		SpawnPoint1 = gameParameters.RespawnPoint1;
		SpawnPoint2 = gameParameters.RespawnPoint2;
		
		PhotonNetwork.automaticallySyncScene = true;

		if (!PhotonNetwork.connected)
		{
			PhotonNetwork.ConnectUsingSettings("0.1");
		}

		SceneManager.activeSceneChanged += OnLoadScene;
	}
	
	private void OnLoadScene(Scene previousScene, Scene newScene)
	{
		GameParameters gameParameters = GameObject.Find("GameParameters").GetComponent<GameParameters>();
		SpawnPoint1 = gameParameters.RespawnPoint1;
		SpawnPoint2 = gameParameters.RespawnPoint2;
		LobbyCamera = GameObject.Find("LobbyCamera");
	}


	public void OnJoinedLobby()
	{
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = 2;
		PhotonNetwork.JoinOrCreateRoom("Gamma", roomOptions, null);
	}

	public virtual void OnJoinedRoom()
	{
		if (PhotonNetwork.playerList.Length == 2)
		{
			GameObject.Find("CanvasSecondPlayer").SetActive(true);
			GameObject.Find("CanvasFirstPlayer").SetActive(false);
			PhotonNetwork.Instantiate(PrefabGirl.name, SpawnPoint2.position, SpawnPoint2.rotation, 0);
			PhotonNetwork.playerName = "SecondPlayer";
		}
		else
		{
			GameObject.Find("CanvasFirstPlayer").SetActive(true);
			GameObject.Find("CanvasSecondPlayer").SetActive(false);
			PhotonNetwork.Instantiate(PrefabGirl.name, SpawnPoint1.position, SpawnPoint1.rotation, 0);
			PhotonNetwork.playerName = "FirstPlayer";
		}

		LobbyCamera.SetActive(false);
	}

	private void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.playerName);
	}


	void Update()
	{
		
	}

}
