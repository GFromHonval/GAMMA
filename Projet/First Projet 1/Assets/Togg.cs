using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using UnityEngine;
using UnityEngine.UI;

public class Togg : MonoBehaviour
{

	public Toggle OtherToggle;
	
	// Use this for initialization
	void Start ()
	{
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (enabled)
		{
			OtherToggle.enabled = false;
		}
		
	}
}
