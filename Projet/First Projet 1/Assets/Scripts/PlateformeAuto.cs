﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeAuto : MonoBehaviour {

	private string State;
	private Vector3 VStartPlateforme;
	public GameObject EndPlateforme;
	public float Vitesse = 1;
	
	// Use this for initialization
	void Start ()
	{
		State = "Monte";
		VStartPlateforme = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (State == "Monte")
		{
			Monte();
		}
		else
		{
			if (State == "Descend")
				Descend();
		}
	}
	
	public void Monte()
	{
		Vector3 BeforeMove = transform.position;
		transform.position = Vector3.MoveTowards(transform.position, EndPlateforme.transform.position, Time.deltaTime * Vitesse);
		Vector3 AfterMove = transform.position;
		
		List<GameObject> ToMove = GetComponent<OnMoveTakeAll>().GetContacts;
		
		foreach (GameObject player in ToMove)
		{
			Vector3 ToAdd = AfterMove - BeforeMove;
			player.transform.position += ToAdd;
		}
		if (transform.position == EndPlateforme.transform.position)
		{
			State = "Descend";
		}
	}

	public void Descend()
	{
		Vector3 BeforeMove = transform.position;
		transform.position = Vector3.MoveTowards(transform.position, VStartPlateforme, Time.deltaTime * Vitesse);
		Vector3 AfterMove = transform.position;
		
		List<GameObject> ToMove = GetComponent<OnMoveTakeAll>().GetContacts;
		
		foreach (GameObject player in ToMove)
		{
			Vector3 ToAdd = AfterMove - BeforeMove;
			player.transform.position += ToAdd;
		}
		if (transform.position == VStartPlateforme)
		{
			State = "Monte";
		}
	}
}
