﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class RotationPlayer : MonoBehaviour
{
	private float Life;
    private float Damage;
	private GameObject GameL;
	private GameObject LePlusB;
	private Transform RespawnP;
	private GameObject GameO;
	float GroundDistance;
	
	public float moveSpeed = 10f;
	public float turnSpeed = 50f;
	public float jumpPower;
	private bool IsJumping = false;

	void Start()
	{
		GameL = GameObject.Find("GameLogic");
		RespawnQuandOnTombe RespawnScript = GameL.GetComponent<RespawnQuandOnTombe>();
		LePlusB = RespawnScript.LePlusBas;
		RespawnP = RespawnScript.RespawnPoint;
		GameO = RespawnScript.GameOver;
		Life = RespawnScript.LifeInThisLevel;
		Damage = RespawnScript.DamageFallOfThisLevel;
	}


	void Update () {
		
		
		if (transform.position.y < LePlusB.transform.position.y)
		{
			if (Life <= Damage)
			{
				GameO.SetActive(true);
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

		if (Life > 0 )
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
					transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * jumpPower, 0.5f * Time.deltaTime);
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
						//Debug.DrawLine(transform.position, transform.position + Vector3.down * 20, Color.green);
						//Debug.DrawLine(hit.point, hit.point + Vector3.left * 5, Color.red);
					}
				}

			}
		}
	}
}
