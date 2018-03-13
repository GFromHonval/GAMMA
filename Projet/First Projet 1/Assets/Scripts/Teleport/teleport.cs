using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
	private Transform destination;
	public GameObject VersPortail1;
	public GameObject VersPortail2;
	// Update is called once per frame
	private void OnCollisionEnter(Collision col)
	{

		if (name == "Portail1")
		{
			destination = VersPortail2.transform;
		}
		else
		{
			destination = VersPortail1.transform;
		}

		col.transform.position = destination.position;
		col.transform.rotation = destination.rotation;
		
		/*Je pense plus nécessaire
			col.transform.position = destination - Vector3.forward ;
			col.transform.Rotate(Vector3.up * 180);
		*/
	}
}
