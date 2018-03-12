using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.AI;

public class RespawnQuandOnTombe : MonoBehaviour
{

	private GameObject[] players;
	[SerializeField] private GameObject LePlusbas;
	[SerializeField] private Transform respawnPoint;

	void FixedUpdate()
	{
		players = GameObject.FindGameObjectsWithTag("Player");
		Debug.Log("seconde");
		foreach (GameObject player in players)
		{
			if (player.transform.position.y < LePlusbas.transform.position.y)
			{
				Debug.Log("tu tombes fdp");
				player.transform.position = respawnPoint.transform.position;
			}

		}
	}
}
