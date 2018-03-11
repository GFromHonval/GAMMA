using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class teleport : MonoBehaviour
{
	public GameObject VersPortail1;
	public GameObject VersPortail2;
	private Vector3 destination;
	
	private void OnCollisionEnter(Collision col)
	{
		if (this.name == "Portail1")
		{
			destination = VersPortail2.transform.position;
		}
		else
		{
			destination = VersPortail1.transform.position;
		}

		col.transform.position = destination - Vector3.forward ;
		col.transform.Rotate(Vector3.up * 180);
	}
}
