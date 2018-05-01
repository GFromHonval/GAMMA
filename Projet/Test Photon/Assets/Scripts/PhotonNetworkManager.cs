using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Collections;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class PhotonNetworkManager : Photon.PunBehaviour
{

	[SerializeField] private GameObject player;
	[SerializeField] private Transform spawnPoint;
	[SerializeField] private GameObject lobbyCamera;
	
	
	// Use this for initialization
	private void Start ()
	{
		PhotonNetwork.automaticallySyncScene = true;
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	public override void OnConnectedToMaster()
	{
		Debug.Log("Connected to Lobby");
		PhotonNetwork.JoinOrCreateRoom("Gamma", null, null);
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("Joined Room");
		PhotonNetwork.Instantiate(player.name, spawnPoint.position, spawnPoint.rotation, 0);
		PhotonNetwork.player.NickName = "Player One";
		lobbyCamera.SetActive(false);
	}

	private void Update()
	{
		
	}

	private void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}
}
