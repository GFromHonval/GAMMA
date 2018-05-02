using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeAuto : MonoBehaviour {

	private string State;
	private Vector3 VStartPlateforme;
	public GameObject EndPlateforme;
	
	// Use this for initialization
	void Start ()
	{
		State = "Monte";
		VStartPlateforme = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (State == "Monte")
		{
			Monte();
		}
		else
		{
			if (State == "Descend")
				Descend();
		}
		
	}
	
	public void Monte()
	{
		transform.position = Vector3.MoveTowards(transform.position, EndPlateforme.transform.position, Time.deltaTime);
		if (transform.position == EndPlateforme.transform.position)
		{
			State = "Descend";
		}
	}

	public void Descend()
	{
		transform.position = Vector3.MoveTowards(transform.position, VStartPlateforme, Time.deltaTime);
		if (transform.position == VStartPlateforme)
		{
			State = "Monte";
		}
	}
}
