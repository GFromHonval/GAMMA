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
	public float TimeOnAir;
	
	//Hidden Variables
	private float Life;
    private float Damage;
	
	//Physics Variables
	private GameObject LePlusB;
	private Transform RespawnP;
	private bool IsJumping = false;
	float GroundDistance;
	private float TimeJump;
	
	//Canvas
	private Canvas EscapeCanvas;
	private Canvas GameOverCanvas; 
	
	public float GetLife
	{
		get { return Life; }
		set { Life = value; }
	}
	
	void Start()
	{
	
		TimeJump = TimeOnAir;
		
		GameOverCanvas = GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>().GetGameOverCanvas.GetComponent<Canvas>();
		EscapeCanvas = GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>().GetEscapeCanvas.GetComponent<Canvas>();
		
		GameParameters gameParameters = GameObject.Find("GameParameters").GetComponent<GameParameters>();
		LePlusB = gameParameters.LePlusBas;
		Life = gameParameters.LifeInThisLevel;
		Damage = gameParameters.DamageFallOfThisLevel;
		
		if (photonView.name == "SecondPlayerGirl" || photonView.name == "SecondPlayerBoy" || photonView.name == "SecondPlayer")
			RespawnP = gameParameters.RespawnPoint2;
		else
			RespawnP = gameParameters.RespawnPoint1;
		
		SceneManager.activeSceneChanged += OnLoadScenePlayer;
	}


	private void OnLoadScenePlayer(Scene preScene, Scene nextScene)
	{
		
	}

	void Update () {
		if (photonView.isMine)
		{
			if (SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKey(KeyCode.Escape))
			{
				EscapeCanvas.enabled = true;
			}
			else
			{
				EscapeCanvas.enabled = false;
				if (Life <= 0 || this.transform.position.y < LePlusB.transform.position.y)
				{
					if (Life <= Damage)
					{
						GameOverCanvas.enabled = true;
						this.transform.position = RespawnP.transform.position;
						this.transform.rotation = RespawnP.transform.rotation;
						GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
						GetComponent<Rigidbody>().velocity = Vector3.zero;
						Life = 0;
					}
					else
					{
						this.transform.position = RespawnP.transform.position;
						this.transform.rotation = RespawnP.transform.rotation;
						GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
						GetComponent<Rigidbody>().velocity = Vector3.zero;
						Life -= Damage;
					}
				}

				if (Life > 0)
				{
					if (Input.GetKey(KeyCode.UpArrow))
						this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

					if (Input.GetKey(KeyCode.DownArrow))
						this.transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);

					if (Input.GetKey(KeyCode.LeftArrow))
						this.transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

					if (Input.GetKey(KeyCode.RightArrow))
						this.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

					if (!IsJumping)
					{
						if (Input.GetKeyDown(KeyCode.Space))
						{
							IsJumping = true;
							this.transform.position = Vector3.Lerp(this.transform.position, this.transform.position + Vector3.up * jumpPower,
								0.5f * Time.deltaTime);
						}
					}
					else
					{
						TimeJump -= Time.deltaTime;
						if (TimeJump <= 0)
						{
							IsJumping = false;
							TimeJump = TimeOnAir;
						}
					}
					
					
					/*if (!IsJumping)
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
								if (Vector3.Distance(transform.position, hit.transform.position) < GroundDistance)// + 1f)
								{
									IsJumping = false;
								}
							}
						}
					}*/

				}
			}
		}
	}
}
