using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;
using UnityEngine.Experimental.UIElements;
using Debug = UnityEngine.Debug;

public class BoutonEtPlateforme : MonoBehaviour
{
	
	//Public Variables
	public Transform BassePosition;
	public PlateformeClass[] Plateformes;
	
	//Choice Button
	public bool StayPressedButton;
	public bool EvaporingButton;
	public bool OnlyGirl;
	public bool OnlyBoy;
	
	//Hiden Variables
	private bool AllPlayers;
		
	//Physics Variables
	private Vector3 BassePose;
	private Vector3 HautePose;
	private bool IsPressing;
	private Vector4 Area;
	private bool HasExitTheArea;
	
	private void Start()
	{
		HasExitTheArea = true;
		AllPlayers = !(OnlyBoy || OnlyGirl);
		
		BassePose = new Vector3(transform.position.x, BassePosition.position.y, transform.position.z);
		HautePose = transform.position;
		Area.x = transform.position.z - GetComponent<BoxCollider>().size.z * transform.localScale.z /2 - 0.2f;
		Area.y = transform.position.z + GetComponent<BoxCollider>().size.z * transform.localScale.z /2 + 0.2f;
		Area.z = transform.position.x - GetComponent<BoxCollider>().size.x * transform.localScale.x /2 - 0.2f; 
		Area.w = transform.position.x + GetComponent<BoxCollider>().size.x * transform.localScale.x /2 + 0.2f;
	}

	[PunRPC]
	private void Update()
	{
		if (IsPressing)
		{
			if (!EvaporingButton)
				ToDo();
			else 
				ToEvapore();
		}
		if (IsPressing && transform.position.y >= BassePose.y)
		{
			transform.position = Vector3.MoveTowards(transform.position, BassePose, Time.deltaTime);
		}
		if (!IsPressing && transform.position.y < HautePose.y)
		{
			transform.position = Vector3.MoveTowards(transform.position, HautePose, Time.deltaTime);
		}
	}

	private void ToEvapore()
	{
		foreach (PlateformeClass plat in Plateformes)
		{
			Destroy(plat.Plateforme);
		}
	}
	
	private void ToDo()
	{
		foreach (PlateformeClass plat in Plateformes)
		{
		
			if (plat.GetState == "Monte")
			{
				plat.Monte();
			}
			else
			{
				if (plat.GetState == "Descend")
					plat.Descend();
			}
		}
	}
	
	private bool NeedToBePressed(Collider Player)
	{
		if (Player.tag == "PlayerBoy")
		{
			return (AllPlayers || OnlyBoy);
		}
		else
		{
			return (AllPlayers || OnlyGirl);
		}
	}

	private void ExitTheArea()
	{
		/*if (AllPlayers || OnlyBoy)
		{
			if (GameObject.FindGameObjectWithTag("PlayerBoy").transform.position.z < Area.x
			    || GameObject.FindGameObjectWithTag("PlayerBoy").transform.position.z > Area.y
			    || GameObject.FindGameObjectWithTag("PlayerBoy").transform.position.x < Area.z
			    || GameObject.FindGameObjectWithTag("PlayerBoy").transform.position.x > Area.w)
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

		if (AllPlayers || OnlyGirl)
		{
			if (GameObject.FindGameObjectWithTag("PlayerGirl").transform.position.z < Area.x
			    || GameObject.FindGameObjectWithTag("PlayerGirl").transform.position.z > Area.y
			    || GameObject.FindGameObjectWithTag("PlayerGirl").transform.position.x < Area.z
			    || GameObject.FindGameObjectWithTag("PlayerGirl").transform.position.x > Area.w)
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
		}*/
	}

	private void OnTriggerStay(Collider other)
	{
		if(!StayPressedButton && NeedToBePressed(other))
		{
			IsPressing = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (!StayPressedButton && NeedToBePressed(other))
		{
			IsPressing = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(StayPressedButton && NeedToBePressed(other))
		{
			IsPressing = !IsPressing;
		}
	}
}
