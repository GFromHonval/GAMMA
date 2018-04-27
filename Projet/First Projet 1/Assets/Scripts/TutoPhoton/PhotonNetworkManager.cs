using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhotonNetworkManager : Photon.MonoBehaviour
{

	[SerializeField] private Text ConnectText;
	[SerializeField] private PlayerClass Player;
	private Transform SpawnPoint1;
	private Transform SpawnPoint2;
	private GameObject LobbyCamera;
	[SerializeField] private Canvas CanvasForMaster;
	[SerializeField] private Canvas CanvasForSecond;
	
	// Use this for initialization
	void Start ()
	{
		SpawnPoint1 = GameObject.Find("GameParameters").GetComponent<GameParameters>().RespawnPoint1;
		SpawnPoint2 = GameObject.Find("GameParameters").GetComponent<GameParameters>().RespawnPoint2;
		LobbyCamera = GameObject.Find("Main Camera");
		DontDestroyOnLoad(gameObject);
		if (CanvasForMaster != null)
			CanvasForMaster.enabled = false;
		if (CanvasForSecond != null)
			CanvasForSecond.enabled = false;
		PhotonNetwork.automaticallySyncScene = true;
		if (!PhotonNetwork.connected)
		{
			PhotonNetwork.ConnectUsingSettings("0.1");
		}
		else
		{
			
		}
		SceneManager.activeSceneChanged += OnLoadScene;
	}
	

	private void OnLoadScene(Scene previousScene, Scene newScene)
	{
		SpawnPoint1 = GameObject.Find("GameParameters").GetComponent<GameParameters>().RespawnPoint1;
		SpawnPoint2 = GameObject.Find("GameParameters").GetComponent<GameParameters>().RespawnPoint2;
		LobbyCamera = GameObject.Find("Main Camera");
		Debug.Log("T'as change de scene");
		Debug.Log(Player.GetPlayerName);
		GameParameters gameParameters = GameObject.Find("GameParameters").GetComponent<GameParameters>();
		if (Player.GetPlayerName == "TheGirl")
			PhotonNetwork.Instantiate(Player.GetPlayerPrefab,SpawnPoint1.position, SpawnPoint1.rotation, 0);
		else
			PhotonNetwork.Instantiate(Player.GetPlayerPrefab,SpawnPoint2.position, SpawnPoint1.rotation, 0);
	}
	
	

	private void OnJoinedLobby()
	{
		Debug.Log("You are now in the lobby");
		
		//RoomOptions pour changer les options de la room
		//Join room if it exist or create one
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = 2;
		PhotonNetwork.JoinOrCreateRoom("Gamma", roomOptions, null); 	
	}

	public virtual void OnJoinedRoom()
	{
		//differencier menu des autres scenes
		//trouver un moyen de garder les choix des joueurs en chargeant les scenes
		if (PhotonNetwork.playerList.Length == 2)
		{
			if (CanvasForSecond!= null)
				CanvasForSecond.enabled = true;
			PhotonNetwork.Instantiate(Player.GetPlayerPrefab,SpawnPoint2.position, SpawnPoint2.rotation, 0);
			Player.GetComponent<PlayerClass>().GetPlayerName = "PlayerTwo";
		}
		else
		{
			if (CanvasForMaster != null)
				CanvasForMaster.enabled = true;
			PhotonNetwork.Instantiate(Player.GetPlayerPrefab,SpawnPoint1.position, SpawnPoint1.rotation, 0);
			Player.GetComponent<PlayerClass>().GetPlayerName = "PlayerOne";
		}
		//disable the lobby camera
		LobbyCamera.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		ConnectText.text = PhotonNetwork.connectionStateDetailed.ToString();
	}
}
