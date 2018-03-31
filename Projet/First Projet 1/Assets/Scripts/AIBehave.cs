﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehave : Photon.MonoBehaviour {

	//https://www.youtube.com/watch?v=OmoKw1ikAi8&t=23s
	public int DistanceDeDetection;
	public GameObject Head;
	public Material ColorInnofensif;
	public Material ColorAttack;
	private Transform StartPos;
	public GameObject[] waypoints;
	private int currentWP;
	private float rotSpeed = 10f;
	private float speed = 1.5f;
	private float accuracyWP = 0.5f;
	private GameObject[] players;

	// Use this for initialization
	void Start ()
	{
		StartPos = this.transform;
	}

	private void FixedUpdate()
	{
		players = GameObject.FindGameObjectsWithTag("Player");
	}

	// Update is called once per frame
	void Update ()
	{
		foreach (GameObject P in players)
		{
			Vector3 direction = P.transform.position - transform.position;
			direction.y = 0;
			
			if (Vector3.Distance(P.transform.position, this.transform.position) < DistanceDeDetection)
			{
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
				Head.GetComponent<Renderer>().material = ColorAttack;
			}
			else
			{
				if (waypoints.Length > 0)
				{
					Head.GetComponent<Renderer>().material = ColorInnofensif;
					if (Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP)
					{
						currentWP++;
						if (currentWP >= waypoints.Length)
						{
							currentWP = 0;
						}
					}
		
					direction = waypoints[currentWP].transform.position - transform.position;
					transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),rotSpeed*Time.deltaTime);
					transform.Translate(0,0,Time.deltaTime * speed);
				}
				else
				{
					transform.position = StartPos.position;
					transform.rotation = StartPos.rotation;
					Head.GetComponent<Renderer>().material = ColorInnofensif;
				}
			}
			
			

		}
	}
}
