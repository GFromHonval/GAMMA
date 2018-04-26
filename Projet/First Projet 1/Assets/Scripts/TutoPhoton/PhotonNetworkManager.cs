using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonNetworkManager : Photon.MonoBehaviour
{

	[SerializeField] private Text ConnectText;
	[SerializeField] private PlayerClass Player;
	[SerializeField] private Transform SpawnPoint1;
	[SerializeField] private Transform SpawnPoint2;
	[SerializeField] private GameObject LobbyCamera;
	[SerializeField] private Canvas CanvasForMaster;
	[SerializeField] private Canvas CanvasForSecond;
	
	// Use this for initialization
	void Start ()
	{
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
			Debug.Log("Joined second scene");
			PhotonNetwork.Instantiate(Player.GetPlayerPrefab,SpawnPoint1.position, SpawnPoint1.rotation, 0);
		}
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
			Player.GetComponent<PlayerClass>().GetPlayerName = "Player Two";
		}
		else
		{
			if (CanvasForMaster != null)
				CanvasForMaster.enabled = true;
			PhotonNetwork.Instantiate(Player.GetPlayerPrefab,SpawnPoint1.position, SpawnPoint1.rotation, 0);
			Player.GetComponent<PlayerClass>().GetPlayerName = "Player One";
		}

		Debug.Log(Player.name);
		//disable the lobby camera
		LobbyCamera.SetActive(false);
	}

	// Update is called once per frame
	void Update ()
	{
		ConnectText.text = PhotonNetwork.connectionStateDetailed.ToString();
	}
}
