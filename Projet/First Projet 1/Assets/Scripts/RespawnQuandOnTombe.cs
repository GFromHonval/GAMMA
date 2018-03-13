using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;
using UnityEngine.AI;

public class RespawnQuandOnTombe : MonoBehaviour
{

	public GameObject LePlusBas;
	public Transform RespawnPoint;
	public GameObject GameOver;
	public float LifeInThisLevel;
	public float DamageFallOfThisLevel;
	
	private void Start()
	{
		GameOver.SetActive(false);
	}

}
