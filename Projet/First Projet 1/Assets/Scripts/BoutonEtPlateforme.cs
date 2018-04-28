using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters;
using System.Text;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;
using Debug = UnityEngine.Debug;

public class BoutonEtPlateforme : MonoBehaviour
{

	public Transform BassePosition;
	public GameObject Plateforme;
	private Vector3 StartPlateforme;
	public GameObject EndPlateforme;
	private Vector3 MouvementDirection;
	private Vector3 BassePose;
	private Vector3 HautePose;
	private bool IsPressing;
	private string PlayerTag = "";
	private Vector4 Area;
	private string PlateformeState;
	public bool StayPressedButton;
	private bool HasExitTheArea;
	
	private void Start()
	{
		HasExitTheArea = true;
		PlayerTag = "Player";
		BassePose = new Vector3(transform.position.x, BassePosition.position.y, transform.position.z);
		HautePose = transform.position;
		Area.x = transform.position.z - GetComponent<BoxCollider>().size.z * transform.localScale.z /2 - 0.2f;
		Area.y = transform.position.z + GetComponent<BoxCollider>().size.z * transform.localScale.z /2 + 0.2f;
		Area.z = transform.position.x - GetComponent<BoxCollider>().size.x * transform.localScale.x /2 - 0.2f; 
		Area.w = transform.position.x + GetComponent<BoxCollider>().size.x * transform.localScale.x /2 + 0.2f;
		StartPlateforme = new Vector3 (Plateforme.transform.position.x, Plateforme.transform.position.y, Plateforme.transform.position.z);
		PlateformeState = "Monte";
	}

	private void Update()
	{
		if (IsPressing)
		{
			ToDo();
		}
		if (IsPressing && transform.position.y >= BassePose.y)
		{
			transform.position = Vector3.MoveTowards(transform.position, BassePose, Time.deltaTime);
			IsInArea();	
		}
		if (!IsPressing && transform.position.y < HautePose.y)
		{
			transform.position = Vector3.MoveTowards(transform.position, HautePose, Time.deltaTime);
			PlayerTag = "Player";
		}
		if (StayPressedButton && !HasExitTheArea)
			IsInArea();
	}

	private void ToDo()
	{
		if (PlateformeState == "Monte")
		{
			Plateforme.transform.position = Vector3.MoveTowards(Plateforme.transform.position, EndPlateforme.transform.position, Time.deltaTime);
			if (Plateforme.transform.position.y >= EndPlateforme.transform.position.y)
			{
				PlateformeState = "Descend";
			}
		}
		else
		{
			if (PlateformeState == "Descend")
			{
				Plateforme.transform.position = Vector3.MoveTowards(Plateforme.transform.position, StartPlateforme, Time.deltaTime);
				if (Plateforme.transform.position.y <= StartPlateforme.y)
				{
					PlateformeState = "Monte";
				}
			}
		}
	}
	
	private void IsInArea()
	{
	
		if (GameObject.FindGameObjectWithTag(PlayerTag).transform.position.z < Area.x
			|| GameObject.FindGameObjectWithTag(PlayerTag).transform.position.z > Area.y
			|| GameObject.FindGameObjectWithTag(PlayerTag).transform.position.x < Area.z
			|| GameObject.FindGameObjectWithTag(PlayerTag).transform.position.x > Area.w)
		{
			if (!StayPressedButton)
				IsPressing = false;
			else
			{
				HasExitTheArea = true;
			}
		}
		else
		{
			if (!StayPressedButton)
				IsPressing = true;
			else
			{
				if (HasExitTheArea)
				{
					IsPressing = !IsPressing;
					HasExitTheArea = false;
				}
			}
		}
		
	}
	
	private void OnCollisionEnter(Collision other)
	{
		PlayerTag = other.gameObject.tag;
		IsInArea();
	}
}
