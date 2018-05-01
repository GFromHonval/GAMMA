using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviour
{

	[SerializeField] private GameObject playerCamera;
	[SerializeField] private MonoBehaviour[] playerControllerScripts;

	private PhotonView photonView;
	public int playerHealth = 100;
	public int currentScene = -1;
	
	private void Start()
	{
		photonView = GetComponent<PhotonView>();

		initialize();
	}


	private void Update()
	{
		if (!photonView.isMine)
		{
			return;
		}
		currentScene = SceneManager.GetActiveScene().buildIndex;
		if (Input.GetKeyDown(KeyCode.L))
			PhotonNetwork.LoadLevel(1);
		if (Input.GetKeyDown(KeyCode.E))
		{
			playerHealth -= 5;
		}
	}


	private void initialize()
	{
		if (photonView.isMine)
		{
			
		}
		else
		{
			playerCamera.SetActive(false);

			foreach (MonoBehaviour m in playerControllerScripts)
			{
				m.enabled = false;
			}
		}
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(playerHealth);
			stream.SendNext(currentScene);
		}
		else
		{
			if (stream.isReading)
			{
				playerHealth = (int) stream.ReceiveNext();
				currentScene = (int) stream.ReceiveNext();
			}
		}
	}
	
}
