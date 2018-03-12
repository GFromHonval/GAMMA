using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.AI;

public class RespawnQuandOnTombe : MonoBehaviour
{

	private GameObject[] players;
	[SerializeField] private GameObject LePlusbas;
	[SerializeField] private Transform respawnPoint;
	[SerializeField] private GameObject GameOver;
	[SerializeField] private float Life;
	[SerializeField] private float Damage;
	
	private void Start()
	{
		GameOver.SetActive(false);
	}

	void FixedUpdate()
	{
		
		players = GameObject.FindGameObjectsWithTag("Player");
		Debug.Log("seconde");
		foreach (GameObject player in players)
		{
			if (player.transform.position.y < LePlusbas.transform.position.y)
			{
				if (Life <= Damage)
				{
					GameOver.SetActive(true);
					player.transform.position = respawnPoint.transform.position;
					
				}
				else
				{
					player.transform.position = respawnPoint.transform.position;
					Life -= Damage;
				}
				
			}

		}
	}
}
