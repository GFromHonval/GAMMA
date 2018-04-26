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
	
	// Use this for initialization
	void Start ()
	{
<<<<<<< HEAD
		PhotonNetwork.automaticallySyncScene = true;
=======
>>>>>>> parent of 3bb08cf... build
		if (!PhotonNetwork.connected)
		{
			PhotonNetwork.ConnectUsingSettings("0.1");
		}
		else
		{
			Debug.Log("Joined second scene");
			PhotonNetwork.Instantiate(Player.name,SpawnPoint1.position, SpawnPoint1.rotation, 0);
		}
<<<<<<< HEAD
		PhotonNetwork.ConnectUsingSettings("0.1");
		}
=======
	}
>>>>>>> parent of 3bb08cf... build

	private void OnJoinedLobby()
	{
		Debug.Log("You are now in the lobby");
		if (Joueurs.Count == 0)
		{
			Joueurs.Add("MainPlayer");
			//differencier menu des autres scenes
			//trouver un moyen de garder les choix des joueurs en chargeant les scenes
			
		}
		else
		{
			
		}
		
		//RoomOptions pour changer les options de la room
		//Join room if it exist or create one
		PhotonNetwork.JoinOrCreateRoom("Gamma", null, null); 	
	}

	public virtual void OnJoinedRoom()
	{
		//spawn the player
<<<<<<< HEAD
		Player.tag = "Player." + PhotonNetwork.countOfPlayersInRooms  ;
		Debug.Log(Player.tag);
		PhotonNetwork.Instantiate(Player.name,SpawnPoint1.position, SpawnPoint1.rotation, 0);
=======
		Debug.Log(PhotonNetwork.countOfPlayersOnMaster);
		PhotonNetwork.Instantiate(Player.name,SpawnPoint1.position, SpawnPoint1.rotation, 0);
		Debug.Log(PhotonNetwork.countOfPlayersOnMaster);
		Player.tag = "Player." + PhotonNetwork.countOfPlayersOnMaster;
		Debug.Log(Player.tag);
>>>>>>> parent of 3bb08cf... build
		//disable the lobby camera
		LobbyCamera.SetActive(false);
	}

	// Update is called once per frame
	void Update ()
	{
		ConnectText.text = PhotonNetwork.connectionStateDetailed.ToString();
	}
}
