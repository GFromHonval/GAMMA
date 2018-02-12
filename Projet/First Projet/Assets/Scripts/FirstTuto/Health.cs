using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{

	public const int MaxHealth = 100;
	[SyncVar (hook = "OnChangeHealth")] public int CurrentHealth = MaxHealth; //Pour synchro avec le serv 
	public RectTransform HealthBar; //Pour gerer la taille de la barre verte (foreground de la bar de vie)
	public bool DestroyOnDeath;
	private NetworkStartPosition[] spawnPoints;

	private void Start()
	{
		if (isLocalPlayer)
		{
			spawnPoints = FindObjectsOfType<NetworkStartPosition>();
		}
	}

	public void TakeDamage(int amout)
	{
		if (!isServer)
			return;

		CurrentHealth -= amout;
		if (CurrentHealth <= 0)
		{
			if (DestroyOnDeath)
				Destroy(gameObject);
			else
			{
				CurrentHealth = MaxHealth;
				RpcRespawm();
			}
		}
		
		HealthBar.sizeDelta = new Vector2(CurrentHealth * 2, HealthBar.sizeDelta.y);
	}

	void OnChangeHealth(int health)
	{
		HealthBar.sizeDelta = new Vector2(health * 2, HealthBar.sizeDelta.y);
	}

	[ClientRpc]
	void RpcRespawm()
	{
		if (isLocalPlayer)
		{
			Vector3 spawnPoint = Vector3.zero;

			if (spawnPoints != null && spawnPoints.Length > 0)
			{
				spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)].transform.position;
			}

			transform.position = spawnPoint;

		}
		
		
	}
}
