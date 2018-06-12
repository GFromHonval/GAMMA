using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AIBehave : Photon.MonoBehaviour {

	//https://www.youtube.com/watch?v=OmoKw1ikAi8&t=23s
	public int DistanceDeDetection;
	public GameObject BulletPrefab;
	public Transform BulletSpawn;
	public Animator Animator;
	public GameObject[] Waypoints;
	public float speed = 1.5f;
	public float rotSpeed = 6f;
	public float SpeedBullet = 50f;
	
	private Transform StartPos;
	private int currentWP;
	private float accuracyWP = 0.5f;
	private List<GameObject> players;
	private string State;
	private bool Shot;
	private float AnimatorTimer = 2f;
	
	// Use this for initialization
	void Start ()
	{
		StartPos = transform;
		
		if (Waypoints.Length == 0)
		{
			Animator.SetBool("Running", false);
			Animator.SetBool("Standing", true);
		}
		else
		{
			Animator.SetBool("Running", true);
			Animator.SetBool("Standing", false);
		}
		
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
				Attack(direction);
			}
			else
			{
				Animator.SetBool("Attacking", false);
				AnimatorTimer = 2f;
				if (Waypoints.Length > 0)
				{
					Animator.SetBool("Running", true);
					if (Vector3.Distance(Waypoints[currentWP].transform.position, transform.position) < accuracyWP)
					{
						currentWP++;
						if (currentWP >= Waypoints.Length)
						{
							currentWP = 0;
						}
					}
		
					direction = Waypoints[currentWP].transform.position - transform.position;
					transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),rotSpeed*Time.deltaTime);
					transform.Translate(0,0,Time.deltaTime * speed);
				}
				else
				{
					transform.position = StartPos.position;
					transform.rotation = StartPos.rotation;
				}
			}
		}
	}

	void Attack(Vector3 direction)
	{
		transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
		if (AnimatorTimer == 2f)
		{
			Animator.SetBool("Attacking", true);
			AnimatorTimer -= Time.deltaTime;
		}
		else
		{
			if (AnimatorTimer <= 0)
			{
				AnimatorTimer = 2f;
				Animator.SetBool("Attacking", false);
				Shot = false;
			}
			else
			{
				AnimatorTimer -= Time.deltaTime;
				if (AnimatorTimer <= 0.5f && !Shot)
				{
					//Creer la balle
					GameObject bullet = PhotonNetwork.Instantiate(BulletPrefab.name, BulletSpawn.position, BulletSpawn.rotation, 0);

					//Fait bouger la balle
					bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * SpeedBullet;

					//Detruit la balle apres 2 sec
					Destroy(bullet, 5);

					Shot = true;
				}
			}
		}
	}
}
