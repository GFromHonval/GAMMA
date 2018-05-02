using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlateformeFall : MonoBehaviour
{

	public float TimeStaying;

	private bool AsTouched;
	private float TimeFall = 2;

	private void Start()
	{
		GetComponent<Rigidbody>().useGravity = true;
		GetComponent<Rigidbody>().isKinematic = true;
	}

	private void OnCollisionEnter(Collision other)
	{
		AsTouched = true;
	}

	private void Update()
	{
		if (AsTouched)
			TimeStaying -= Time.deltaTime;
		if (TimeStaying <= 0)
		{
			TimeFall -= Time.deltaTime;
			GetComponent<Rigidbody>().isKinematic = false;
		}
		if (TimeFall <= 0)
			Destroy(this);
	}
}
