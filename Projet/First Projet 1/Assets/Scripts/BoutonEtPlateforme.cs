using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonEtPlateforme : MonoBehaviour
{

	public Transform BassePosition;
	public Transform HautePosition;
	public GameObject Plateforme;
	private Vector3 MouvementDirection;
		
	// Update is called once per frame
	private void OnCollisionEnter(Collision other)
	{
		this.transform.position = Vector3.MoveTowards(HautePosition.position, BassePosition.position, Time.deltaTime);
	}
}
