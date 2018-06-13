using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class teleport : MonoBehaviour
{
	private Transform destination;
	public GameObject VersPortail1;
	public GameObject VersPortail2;
	public bool OnlyBoy;
	public bool OnlyGirl;
	
	//Hidden Variables
	private bool AllPlayers;
	
	private void Start()
	{
		AllPlayers = !(OnlyBoy || OnlyGirl);
	}

	private void OnCollisionEnter(Collision col)
	{
		if (AllPlayers)
			Teleportation(col);
		else
		{
			if (OnlyBoy && col.gameObject.tag == "PlayerBoy")
				Teleportation(col);
			else
			{
				if (OnlyGirl && col.gameObject.tag == "PlayerGirl")
					Teleportation(col);
			}
		}
	}

	private void Teleportation(Collision col)
	{
		if (this.tag == "Portail1")
		{
			destination = VersPortail2.transform;
		}
		else
		{
			destination = VersPortail1.transform;
		}

		col.transform.position = destination.position;
		col.transform.rotation = destination.rotation;
	}
}
