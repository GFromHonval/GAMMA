using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class NewBehaviourScript : MonoBehaviour
{

	private float X;

	private float Y;

	private float Z;

	private Vector3 cube;
	// Use this for initialization
	void Start ()
	
	{ 
		X = GameObject.Find("FirstPersonCharacter").transform.position.x;
		Y = GameObject.Find("FirstPersonCharacter").transform.position.y;
		Z = GameObject.Find("FirstPersonCharacter").transform.position.z;
		cube = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 pers = new Vector3(X,Y,Z);
		if (pers == cube)
		{
			System.Threading.Thread.Sleep(10000);
			gameObject.transform.Translate(0,-1,0);
		}
		
	}
}
