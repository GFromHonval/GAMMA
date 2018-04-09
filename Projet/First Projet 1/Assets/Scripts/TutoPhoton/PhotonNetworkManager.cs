using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonNetworkManager : Photon.MonoBehaviour
{

	[SerializeField] private Text ConnectText;
	[SerializeField] private GameObject Player;
	[SerializeField] private Transform SpawnPoint;
	[SerializeField] private GameObject LobbyCamera;
	[SerializeField] private List<string> Joueurs;
	
	// Use this for initialization
	void Start ()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

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
		PhotonNetwork.Instantiate(Player.name,SpawnPoint.position, SpawnPoint.rotation, 0);
		//disable the lobby camera
		LobbyCamera.SetActive(false);
	}

	// Update is called once per frame
	void Update ()
	{

		ConnectText.text = PhotonNetwork.connectionStateDetailed.ToString();
	}
}
