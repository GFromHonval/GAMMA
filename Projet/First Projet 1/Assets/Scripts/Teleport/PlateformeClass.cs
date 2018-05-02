using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlateformeClass : MonoBehaviour
{

	private string State;
	private Vector3 VStartPlateforme;
	public GameObject Plateforme;
	public GameObject EndPlateforme;
	public GameObject StartPlateforme;
	public bool Unidirectionnel;
	public bool VaADroite;
	public float Vitesse = 1;

	public string GetState
	{
		get { return State; }
		set { State = value; }
	}

	// Use this for initialization
	void Start () {

		VStartPlateforme = new Vector3 (StartPlateforme.transform.position.x, StartPlateforme.transform.position.y, StartPlateforme.transform.position.z);
		if (Unidirectionnel)
		{
			if (VaADroite)
				State = "Monte";
			else
				State =  "Descend";
		}
		else
		{
			State = "Monte";
		}
		
	}

	public void Monte()
	{
		Plateforme.transform.position = Vector3.MoveTowards(Plateforme.transform.position, EndPlateforme.transform.position, Time.deltaTime * Vitesse);
		if (!Unidirectionnel && Plateforme.transform.position == EndPlateforme.transform.position)
		{
			State = "Descend";
		}
	}

	public void Descend()
	{
		Plateforme.transform.position = Vector3.MoveTowards(Plateforme.transform.position, VStartPlateforme, Time.deltaTime * Vitesse);
		if (!Unidirectionnel && Plateforme.transform.position == VStartPlateforme)
		{
			State = "Monte";
		}
	}
}

