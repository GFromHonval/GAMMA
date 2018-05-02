using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class RotationPlayer : Photon.MonoBehaviour
{
	//Deplacement Variables
	public float moveSpeed = 10f;
	public float turnSpeed = 50f;
	public float jumpPower;
	
	//Hidden Variables
	private float Life;
    private float Damage;
	
	//Physics Variables
	private GameObject LePlusB;
	private Transform RespawnP;
	private bool IsJumping = false;
	float GroundDistance;
	
	//Canvas
	private GameObject EscapeCanvas;
	private GameObject GameOverCanvas; 
	
	public float GetLife
	{
		get { return Life; }
		set { Life = value; }
	}
	
	void Start()
	{
		GameOverCanvas = GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>().GetGameOverCanvas;
		EscapeCanvas = GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>().GetEscapeCanvas;
		
		GameParameters gameParameters = GameObject.Find("GameParameters").GetComponent<GameParameters>();
		LePlusB = gameParameters.LePlusBas;
		Life = gameParameters.LifeInThisLevel;
		Damage = gameParameters.DamageFallOfThisLevel;
		if (photonView.name == "FirstPlayer")
			RespawnP = gameParameters.RespawnPoint1;
		else
			RespawnP = gameParameters.RespawnPoint2;
		
		SceneManager.activeSceneChanged += OnLoadScenePlayer;
	}


	private void OnLoadScenePlayer(Scene preScene, Scene nextScene)
	{
		GameParameters gameParameters = GameObject.Find("GameParameters").GetComponent<GameParameters>();
		LePlusB = gameParameters.LePlusBas;
		Life = gameParameters.LifeInThisLevel;
		Damage = gameParameters.DamageFallOfThisLevel;

		PhotonNetworkManager gameLogic = GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>();
		
		if (PhotonNetwork.playerName == "FirstPlayer")
		{
			RespawnP = gameParameters.RespawnPoint1;
			if (photonView.isMine && gameLogic.GetPrefabFirstPlayer == "PrefabBoy")
			{
				PhotonNetwork.Instantiate(gameLogic.GetPrefabBoy.name, RespawnP.position, RespawnP.rotation, 0);
				PhotonNetwork.Destroy(gameObject);
				return;
			}
		}
		else
		{
			if (photonView.isMine && PhotonNetwork.playerName == "SecondPlayer")
			{
				print("okok");
				RespawnP = gameParameters.RespawnPoint2;
				if (gameLogic.GetPrefabFirstPlayer == "PrefabGirl")
				{
					print("Should be fine");
					PhotonNetwork.Destroy(gameObject);
					PhotonNetwork.Instantiate(gameLogic.GetPrefabBoy.name, RespawnP.position, RespawnP.rotation, 0);
					return;
				}
			}
		}

		transform.position = RespawnP.position;
		transform.rotation = RespawnP.rotation;
	}

	void Update () {
		
		if (photonView.isMine)
		{
			if (SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKey(KeyCode.Escape))
			{
				EscapeCanvas.SetActive(true);
			}
			else
			{
				EscapeCanvas.SetActive(false);
				if (Life <= 0 || transform.position.y < LePlusB.transform.position.y)
				{
					if (Life <= Damage)
					{
						GameOverCanvas.SetActive(true);
						transform.position = RespawnP.transform.position;
						transform.rotation = RespawnP.transform.rotation;
						GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
						GetComponent<Rigidbody>().velocity = Vector3.zero;
						Life = 0;
					}
					else
					{
						transform.position = RespawnP.transform.position;
						transform.rotation = RespawnP.transform.rotation;
						GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
						GetComponent<Rigidbody>().velocity = Vector3.zero;
						Life -= Damage;
					}
				}

				if (Life > 0)
				{
					if (Input.GetKey(KeyCode.UpArrow))
						transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

					if (Input.GetKey(KeyCode.DownArrow))
						transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);

					if (Input.GetKey(KeyCode.LeftArrow))
						transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

					if (Input.GetKey(KeyCode.RightArrow))
						transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

					if (!IsJumping)
					{
						RaycastHit hit;
						Vector3 GroundPosition;
						if (Physics.Raycast(transform.position, Vector3.down, out hit))
						{
							GroundPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
							GroundDistance = Vector3.Distance(transform.position, GroundPosition);
						}

						if (Input.GetKeyDown(KeyCode.Space))
						{
							IsJumping = true;
							transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * jumpPower,
								0.5f * Time.deltaTime);
						}
					}
					else
					{
						if (Input.GetKey(KeyCode.Space))
						{

						}
						else
						{
							RaycastHit hit;
							if (Physics.Raycast(transform.position, Vector3.down, out hit))
							{
								if (Vector3.Distance(transform.position, hit.transform.position) < GroundDistance + 1f)
								{
									IsJumping = false;
								}
							}
						}
					}

				}
			}
		}
	}
}
