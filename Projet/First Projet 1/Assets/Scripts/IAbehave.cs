using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IAbehave : Photon.MonoBehaviour
{

	public Transform Player;
	public GameObject Head;
	public Material ColorInnofensif;
	public Material ColorAttack;
	private Vector3 StartPos;
	private GameObject[] players;

	// Use this for initialization
	void Start ()
	{
		StartPos = this.transform.position;

	}
	
	// Update is called once per frame
	void Update ()
	{
		players = GameObject.FindGameObjectsWithTag("Player");
		Player = players[0].transform;
		
		if (Vector3.Distance(Player.position , this.transform.position) < 10)
		{
			Vector3 direction = Player.position - this.transform.position;
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
			Head.GetComponent<Renderer>().material = ColorAttack;
		}
		else
		{
			this.transform.position = StartPos;
			Head.GetComponent<Renderer>().material = ColorInnofensif;
		}
	}
}
