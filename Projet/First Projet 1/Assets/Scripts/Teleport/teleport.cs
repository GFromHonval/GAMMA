using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
	private Vector3 destination;
	public GameObject VersPortail1;
	public GameObject VersPortail2;
	// Update is called once per frame
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
