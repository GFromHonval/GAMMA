using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.AI;

public class ReturnOrigine : MonoBehaviour
{
	public float Vitesse;
	public GameObject Origine;

	private Vector3 Origin;
	private Vector3 Old;
	private bool Return;
	
	// Use this for initialization
	void Start ()
	{
		Origin = Origine.transform.position;
		Old = Origin;
	}
	
	// Update is called once per frame
	void Update () {

		if (!Return && Old - transform.position == Vector3.zero)
		{
			Return = true;
		}
		else
		{
			Old = transform.position;
		}

		if (Return)
		{
			transform.position = Vector3.MoveTowards(transform.position, Origin, Time.deltaTime * Vitesse);
			if (transform.position == Origin)
				Return = false;
		}
	}
}
