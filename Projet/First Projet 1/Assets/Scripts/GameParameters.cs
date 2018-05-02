using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters : MonoBehaviour
{
	public GameObject LePlusBas;
	public Transform RespawnPoint1;
	public Transform RespawnPoint2;
	public GameObject GameOver;
	public float LifeInThisLevel;
	public float DamageFallOfThisLevel;
	public float DamageAttackedThisLevel;
	
	private void Start()
	{
		GameOver.SetActive(false);
	}
}
