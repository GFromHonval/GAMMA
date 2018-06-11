using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehave : Photon.MonoBehaviour {

	//https://www.youtube.com/watch?v=OmoKw1ikAi8&t=23s
	public int DistanceDeDetection;
	public GameObject BulletPrefab;
	public Transform BulletSpawn;
	public GameObject Head;
	public Material ColorInnofensif;
	public Material ColorAttack;
	private Transform StartPos;
	public GameObject[] waypoints;
	private int currentWP;
	private float rotSpeed = 10f;
	private float speed = 1.5f;
	private float accuracyWP = 0.5f;
	private List<GameObject> players;
	private string State;
	private float SpeedBullet = 2f;
	private float Timer;
	
	// Use this for initialization
	void Start ()
	{
		StartPos = transform;
		Timer = 1f;
	}

	private void FixedUpdate()
	{
		players = new List<GameObject>();

		if (!Equals(GameObject.FindWithTag("PlayerGirl"), null) )
			players.Add(GameObject.FindWithTag("PlayerGirl"));
		if (!Equals(GameObject.FindWithTag("PlayerBoy"), null))
			players.Add(GameObject.FindWithTag("PlayerBoy"));
	}

	// Update is called once per frame
	void Update ()
	{
		
		foreach (GameObject P in players)
		{
			Vector3 direction = P.transform.position - transform.position;
			direction.y = 0;
			
			if (Vector3.Distance(P.transform.position, this.transform.position) < DistanceDeDetection)
			{
				//Head.GetComponent<Renderer>().material = ColorAttack;
				State = "Attack";
				Attack(direction);
			}
			else
			{
				if (waypoints.Length > 0)
				{
					State = "Patrol";
					//Head.GetComponent<Renderer>().material = ColorInnofensif;
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
					transform.position = StartPos.position;
					transform.rotation = StartPos.rotation;
					Head.GetComponent<Renderer>().material = ColorInnofensif;
				}
			}
		}
	}

	void Attack(Vector3 direction)
	{
		transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
		Timer -= Time.deltaTime;
		if (Timer <= 0)
		{
			//Creer la balle
			GameObject bullet = PhotonNetwork.Instantiate(BulletPrefab.name, BulletSpawn.position, BulletSpawn.rotation, 0);

			//Fait bouger la balle
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * SpeedBullet;

			//Detruit la balle apres 2 sec
			Destroy(bullet, 2);
			Timer = 1f;
		}
		
	}
}
