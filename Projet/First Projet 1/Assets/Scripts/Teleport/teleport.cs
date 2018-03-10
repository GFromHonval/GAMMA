using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
	private Vector3 destination;
	// Update is called once per frame
	private void OnCollisionEnter(Collision col)
	{
		if (this.name == "Portail1")
		{
			destination = GameObject.Find(("Portail2")).transform.position;
		}
		else
		{
			destination = GameObject.Find(("Portail1")).transform.position;
		}

		col.transform.position = destination - Vector3.forward ;
		col.transform.Rotate(Vector3.up * 180);
	}
}
