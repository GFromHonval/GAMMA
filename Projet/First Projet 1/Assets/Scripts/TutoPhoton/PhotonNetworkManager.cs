using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonNetworkManager : Photon.MonoBehaviour
{

	[SerializeField] private Text ConnectText;
	[SerializeField] private GameObject Player;
	[SerializeField] private Transform SpawnPoint1;
	[SerializeField] private Transform SpawnPoint2;
	[SerializeField] private GameObject LobbyCamera;
	[SerializeField] private List<string> Joueurs;
	[SerializeField] private Canvas CanvasToDisable;
	
	// Use this for initialization
	void Start ()
	{
		PhotonNetwork.automaticallySyncScene = true;
		if (!PhotonNetwork.connected)
		{
			PhotonNetwork.ConnectUsingSettings("0.1");
		}
		else
		{
			Debug.Log("Joined second scene");
			PhotonNetwork.Instantiate(Player.name,SpawnPoint1.position, SpawnPoint1.rotation, 0);
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
		if (PhotonNetwork.playerList.Length == 2)
		{
			//differencier menu des autres scenes
			//trouver un moyen de garder les choix des joueurs en chargeant les scenes
			CanvasToDisable.enabled = false;
		}
		//spawn the player

		PhotonNetwork.Instantiate(Player.name,SpawnPoint1.position, SpawnPoint1.rotation, 0);
		Player.tag = "Player." + PhotonNetwork.countOfPlayersInRooms  ;
		Debug.Log(Player.tag);

		//disable the lobby camera
		LobbyCamera.SetActive(false);
	}

	// Update is called once per frame
	void Update ()
	{
		ConnectText.text = PhotonNetwork.connectionStateDetailed.ToString();
	}
}
