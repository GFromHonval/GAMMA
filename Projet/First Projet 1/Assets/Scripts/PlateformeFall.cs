using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.AccessControl;
using UnityEngine;

public class PlateformeFall : MonoBehaviour
{

	public float TimeStaying;
	public float TimeFall;

	private bool AsTouched;
	private Vector3 StartPosition;
	private float _timeStaying;
	private float _timeFall;

	private void Start()
	{
		_timeFall = TimeFall;
		_timeStaying = TimeStaying;
		StartPosition = transform.position;
		GetComponent<Rigidbody>().useGravity = true;
		GetComponent<Rigidbody>().isKinematic = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		AsTouched = true;
	}

	private void Update()
	{
		if (AsTouched)
			_timeStaying -= Time.deltaTime;
		if (_timeStaying<= 0)
		{
			_timeFall -= Time.deltaTime;
			GetComponent<Rigidbody>().isKinematic = false;
		}

		if (_timeFall <= 0)
		{
			AsTouched = false;
			_timeStaying = TimeStaying;
			_timeFall = TimeFall;
			GetComponent<Rigidbody>().isKinematic = true;
			transform.position = StartPosition;
		}
	}
}
