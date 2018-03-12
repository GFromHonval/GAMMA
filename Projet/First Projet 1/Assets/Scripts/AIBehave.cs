using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AIBehave : MonoBehaviour {

	//https://www.youtube.com/watch?v=OmoKw1ikAi8&t=23s
	public Transform Player;
	public int DistanceDeDetection;
	public GameObject Head;
	public Material ColorInnofensif;
	public Material ColorAttack;
	private Vector3 StartPos;
	public GameObject[] waypoints;
	private int currentWP;
	private float rotSpeed = 10f;
	private float speed = 1.5f;
	private float accuracyWP = 0.5f;
	//private GameObject[] players;

	// Use this for initialization
	void Start ()
	{
		StartPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//players = GameObject.FindGameObjectsWithTag("Player");
		//Player = players[0].transform;
		Vector3 direction = Player.position - transform.position;
		direction.y = 0;
		if (waypoints.Length > 0)
		{
			Debug.Log(currentWP);
			if (Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP)
			{
				currentWP++;
				if (currentWP >= waypoints.Length)
				{
					currentWP = 0;
				}
			}

			direction = waypoints[currentWP].transform.position - transform.position;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),rotSpeed*Time.deltaTime);
			transform.Translate(0,0,Time.deltaTime * speed);
		}
		else
		{
			if (Vector3.Distance(Player.position, this.transform.position) < DistanceDeDetection)
			{
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
}
