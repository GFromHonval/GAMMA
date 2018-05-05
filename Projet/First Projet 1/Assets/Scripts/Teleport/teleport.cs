using System;
using System.Collections;
using System.Collections.Generic;
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
		Teleportation(col);
		
		/*Je pense plus nécessaire, en tout cas tant qu'il n'y a pas d'animations
			col.transform.position = destination - Vector3.forward ;
		*/
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
