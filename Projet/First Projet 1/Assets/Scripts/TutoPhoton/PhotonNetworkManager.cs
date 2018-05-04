using System.Collections;
using System.Collections.Generic;
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
	private string NameSecondPlayer = "";
	private float MyLife;
	private float OtherLife;
	
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
				NameSecondPlayer = "FirstPlayerGirl";
				PhotonNetwork.player.NickName = "SecondPlayerBoy";
			}
			else
			{
				if (MasterName == "FirstPlayerBoy")
				{
					PhotonNetwork.Instantiate(PrefabGirl.name, SpawnPoint2.position, SpawnPoint2.rotation, 0);
					NameSecondPlayer = "FirstPlayerBoy";
					PhotonNetwork.player.NickName = "SecondPlayerGirl";
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
		if (PhotonNetwork.playerList.Length == 2)
		{		
			GameObject.Find("CanvasSecondPlayer").GetComponent<Canvas>().enabled = true;
			PhotonNetwork.Instantiate(PrefabBoy.name, SpawnPoint2.position, SpawnPoint2.rotation, 0);
			PhotonNetwork.playerName = "SecondPlayer";
		}
		else
		{
			GameObject.Find("CanvasFirstPlayer").GetComponent<Canvas>().enabled = true;
			PhotonNetwork.Instantiate(PrefabGirl.name, SpawnPoint1.position, SpawnPoint1.rotation, 0);
			PhotonNetwork.playerName = "FirstPlayer";
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
			
			GUILayout.Label(MyLife.ToString());
			
			GUILayout.Label(OtherLife.ToString());
		}
	}

	void Update()
	{
		if (PhotonNetwork.playerList.Length == 2)
		{
			GameObject PlayerBoy = GameObject.FindGameObjectWithTag("PlayerBoy");
			GameObject PlayerGirl = GameObject.FindGameObjectWithTag("PlayerGirl");

			if (PhotonNetwork.player.NickName == "FirstPlayerBoy" || PhotonNetwork.player.NickName == "SecondPlayerBoy")
			{
				MyLife = GameObject.FindGameObjectWithTag("PlayerBoy").GetComponent<RotationPlayer>().GetLife;
				OtherLife = GameObject.FindGameObjectWithTag("PlayerGirl").GetComponent<RotationPlayer>().GetLife;
			}
			else
			{
				MyLife = GameObject.FindGameObjectWithTag("PlayerGirl").GetComponent<RotationPlayer>().GetLife;
				OtherLife = GameObject.FindGameObjectWithTag("PlayerBoy").GetComponent<RotationPlayer>().GetLife;
			}
		}
	}
}
