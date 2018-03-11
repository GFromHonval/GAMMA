using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pontlevis : MonoBehaviour {

	
// Use this for initialization
	void Start ()
	{
		GameObject.Find("SqFIXED+NET");
		GameObject.Find("Pont-Levis");
		
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "SqFIXED+NET(Clone)")
		{
			GameObject.Find("Pont-Levis").transform.Translate(0, 1, 0);

		}
	}

	

}
