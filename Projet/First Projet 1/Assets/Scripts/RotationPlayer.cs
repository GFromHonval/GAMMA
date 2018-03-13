using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class RotationPlayer : MonoBehaviour
{
	private float Life;
    private float Damage;
	private GameObject GameL;
	private GameObject LePlusB;
	private Transform RespawnP;
	private GameObject GameO;
	
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

			if (Input.GetKey(KeyCode.Space) && !IsJumping)
			{
				//IsJumping = true;	
				transform.Translate(Vector3.up * jumpPower * Time.deltaTime);
			}
		}
		
	}

}
