using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using Debug = UnityEngine.Debug;

public class RotationPlayer : Photon.MonoBehaviour
{
	//Deplacement Variables
	public float moveSpeed = 10f;
	public float turnSpeed = 50f;
	public float jumpPower;
	public float GravityMultiplier;
	
	//Hidden Variables
	private float Damage;
	private bool Destroy;
	private float Life;
	private Dictionary<string, KeyCode> Controls;
	
	//Physics Variables
	private GameObject LePlusB;
	private Transform RespawnP;
	private bool IsJumping;
	private float GroundDistance;
	private float GroundDistanceReference;
	private Rigidbody ThisRigidbody;
	private float CoeffPuissance;
	private float TimeBeforeJumping = 0.483f;
	
	//Physics Objects\
	public GameObject CharacterBaseObject;
	private PhotonNetworkManager photonNetworkManager;
	
	//Canvas
	private Canvas EscapeCanvas;
	private Canvas GameOverCanvas;
	
	//Animation
	private Animator Animator;
	
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

		Animator = GetComponent<Animator>();
		
		photonNetworkManager = GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>();
		GameOverCanvas = photonNetworkManager.GetGameOverCanvas.GetComponent<Canvas>();
		EscapeCanvas = photonNetworkManager.GetEscapeCanvas.GetComponent<Canvas>();
		if (!photonNetworkManager.IsPlayingLocal)
			Controls = photonNetworkManager.GetDictionaryOne;
		else
		{
			if (SceneManager.GetActiveScene().name != "Menu without logic")
			{
				if (tag == "PlayerBoy")
				{
					Controls = photonNetworkManager.GetDictionarySecond;
					GetComponentInChildren<Camera>().rect = new Rect(0, 0, 0.5f, 1);
				}
				else
				{
					Controls = photonNetworkManager.GetDictionaryFirst;
					GetComponentInChildren<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
				}
			}
		}
		
		GameParameters gameParameters = GameObject.Find("GameParameters").GetComponent<GameParameters>();
		LePlusB = gameParameters.LePlusBas;
		Damage = gameParameters.DamageFallOfThisLevel;
		Life = gameParameters.LifeInThisLevel;

		if (photonNetworkManager.IsPlayingLocal)
		{
			if (tag == "PlayerBoy")
				RespawnP = gameParameters.RespawnPoint2;
			else
				RespawnP = gameParameters.RespawnPoint1;
		}
		else
		{
			if (PhotonNetwork.player.NickName == "SecondPlayerGirl" || PhotonNetwork.player.NickName == "SecondPlayerBoy" ||
			    PhotonNetwork.player.NickName == "SecondPlayer")
				RespawnP = gameParameters.RespawnPoint2;
			else
				RespawnP = gameParameters.RespawnPoint1;
		}

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
			if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().name != "Menu without logic" && Input.GetKey(KeyCode.Escape))
			{
				EscapeCanvas.enabled = true;
			}
			else
			{
				EscapeCanvas.enabled = false;
				if (Life <= 0 || this.transform.position.y < LePlusB.transform.position.y)
				{
					Animator.SetBool("Running", false);
					Animator.SetBool("RunningBack", false);
					Animator.SetBool("Jumping", false);
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
					if (Input.GetKey(Controls["Up"]) && TimeBeforeJumping == 0.483f)
					{
						if (!IsJumping)
							Animator.SetBool("Running", true);
						this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * CoeffPuissance);
					}
					else
						Animator.SetBool("Running", false);


					if (Input.GetKey(Controls["Down"]) && TimeBeforeJumping == 0.483f)
					{
						Animator.SetBool("RunningBack", true);
						this.transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime * CoeffPuissance);
					}
					else
						Animator.SetBool("RunningBack", false);

					if (Input.GetKey(Controls["Left"]) && TimeBeforeJumping == 0.483f)
						this.transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

					if (Input.GetKey(Controls["Right"]) && TimeBeforeJumping == 0.483f)
						this.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

					CheckGroundStatue();
					
					if (!IsJumping || TimeBeforeJumping < 0.483)
					{
						CoeffPuissance = 1f;
						if (Input.GetKeyDown(Controls["Jump"]) || TimeBeforeJumping < 0.483)
						{
							IsJumping = true;
							Animator.SetBool("Jumping", true);
							if (TimeBeforeJumping <= 0f)
							{
								ThisRigidbody.velocity = new Vector3(ThisRigidbody.velocity.x, jumpPower, ThisRigidbody.velocity.z);
								TimeBeforeJumping = 0.483f;
								CoeffPuissance = 0.5f;
							}
							else
								TimeBeforeJumping -= Time.deltaTime;
						}
						else
							Animator.SetBool("Jumping", false);
					}
					else
					{
						Vector3 extraGravity = (Physics.gravity * GravityMultiplier) - Physics.gravity;
						ThisRigidbody.AddForce(extraGravity);
					}
				}
			}
		}
		
		if (PhotonNetwork.playerList.Length == 2 || SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().name != "Menu without logic")
		{
			GameObject PlayerBoy = GameObject.FindGameObjectWithTag("PlayerBoy");
			GameObject PlayerGirl = GameObject.FindGameObjectWithTag("PlayerGirl");
			float Life1 = PlayerBoy.GetComponent<RotationPlayer>().LifePerso;
			float Life2 = PlayerGirl.GetComponent<RotationPlayer>().LifePerso;
			
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
