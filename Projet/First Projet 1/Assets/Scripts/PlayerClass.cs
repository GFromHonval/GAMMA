using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class PlayerClass : MonoBehaviour
{

	private static string PlayerNameInMine;
	private static string PlayerPrefab;
	private static string PrefabBoyName;
	private static string PrefabGirlName;
	[SerializeField] public GameObject PlayerBoy;
	[SerializeField] public GameObject PlayerGirl;

	public string GetPlayerName
	{
		get { return PlayerNameInMine; }
		set { PlayerNameInMine = value; }
	}

	public string GetPlayerPrefab
	{
		get { return PlayerPrefab; }
		set { PlayerPrefab = value; }
	}

	public string GetPrefabBoyName
	{
		get { return PrefabBoyName; }
	}

	public string GetPrefabGirlName
	{
		get { return PrefabGirlName; }
	}
	// Use this for initialization
	void Start ()
	{
		if (SceneManager.GetActiveScene() == SceneManager.GetSceneAt(0))
			Naming();
	}

	void Naming()
	{
		PrefabGirlName = PlayerGirl.name;
		PrefabBoyName = PlayerBoy.name;
		PlayerPrefab = PrefabBoyName;
	}

}
