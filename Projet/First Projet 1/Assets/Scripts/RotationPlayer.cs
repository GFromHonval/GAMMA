using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
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
	public float GravityMultiplier;
	
	//Hidden Variables
	private float Damage;
	private bool Destroy;
	private float Life;
	
	//Physics Variables
	private GameObject LePlusB;
	private Transform RespawnP;
	private bool IsJumping;
	private float GroundDistance;
	private float GroundDistanceReference;
	private Rigidbody ThisRigidbody;
	private float CoeffPuissance;
	
	//Physics Objects\
	public GameObject CharacterBaseObject;
	private PhotonNetworkManager photonNetworkManager;
	
	//Canvas
	private Canvas EscapeCanvas;
	private Canvas GameOverCanvas;
	
	public bool GetDestroy
	{
		get { return Destroy; }
	}

	public float LifePerso
	{
		get { return Life; }
		set { Life = value; }
	}
	
	void Start()
	{
		GroundDistanceReference = (transform.position - CharacterBaseObject.transform.position).y + 0.1f;
		ThisRigidbody = GetComponent<Rigidbody>();
		
		photonNetworkManager = GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>();
		GameOverCanvas = photonNetworkManager.GetGameOverCanvas.GetComponent<Canvas>();
		EscapeCanvas = photonNetworkManager.GetEscapeCanvas.GetComponent<Canvas>();
		
		GameParameters gameParameters = GameObject.Find("GameParameters").GetComponent<GameParameters>();
		LePlusB = gameParameters.LePlusBas;
		Damage = gameParameters.DamageFallOfThisLevel;
		Life = gameParameters.LifeInThisLevel;
		
		if (PhotonNetwork.player.NickName == "SecondPlayerGirl" || PhotonNetwork.player.NickName == "SecondPlayerBoy" || PhotonNetwork.player.NickName == "SecondPlayer")
			RespawnP = gameParameters.RespawnPoint2;
		else
			RespawnP = gameParameters.RespawnPoint1;
		
		SceneManager.activeSceneChanged += OnLoadScenePlayer;
	}


	private void OnLoadScenePlayer(Scene preScene, Scene nextScene)
	{
		Destroy = true;
	}

	void Update () 
	{
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

				if (photonNetworkManager.GetLife > 0)
				{
					if (Input.GetKey(KeyCode.UpArrow))
						this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * CoeffPuissance);
						
					if (Input.GetKey(KeyCode.DownArrow))
						this.transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime * CoeffPuissance);

					if (Input.GetKey(KeyCode.LeftArrow))
						this.transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

					if (Input.GetKey(KeyCode.RightArrow))
						this.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

					CheckGroundStatue();
					
					if (!IsJumping)
					{
						CoeffPuissance = 1f;
						if (Input.GetKeyDown(KeyCode.Space))
						{
							IsJumping = true;
							ThisRigidbody.velocity = new Vector3(ThisRigidbody.velocity.x, jumpPower, ThisRigidbody.velocity.z);
							CoeffPuissance = 0.5f;
						}
					}
					else
					{
						Vector3 extraGravity = (Physics.gravity * GravityMultiplier) - Physics.gravity;
						ThisRigidbody.AddForce(extraGravity);
					}
				}
			}
		}
		
		if (PhotonNetwork.playerList.Length == 2)
		{
			GameObject PlayerBoy = GameObject.FindGameObjectWithTag("PlayerBoy");
			GameObject PlayerGirl = GameObject.FindGameObjectWithTag("PlayerGirl");
			float Life1 = PlayerBoy.GetComponent<RotationPlayer>().LifePerso;
			float Life2 = PlayerGirl.GetComponent<RotationPlayer>().LifePerso;
			print(Life1);
			print(Life2);

			if (Math.Abs(Life1 - Life2) <= Damage)
			{
				Life = Math.Min(Life1, Life2);
			}
		}
		
		photonNetworkManager.GetLife = Life;
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(Life);
		}
		else
		{
			Life = (float) stream.ReceiveNext();
		}
	}

	private void CheckGroundStatue()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit))
		{
			Vector3 v = transform.position;
			v.y -= hit.distance;
			GroundDistance = hit.distance;
			IsJumping = GroundDistance > GroundDistanceReference;
		}
	}

}
