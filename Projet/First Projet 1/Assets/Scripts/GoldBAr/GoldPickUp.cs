using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickUp : MonoBehaviour
{
	public int value; //Valeur d'un Coin

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other) //Tester si le joueur est sur un coin et lui ajouter la valeur du coin
	{
		if (other.tag == "Player")
		{
			FindObjectOfType<GameManager>().AddGold(value);
			
			Destroy(gameObject); //enlever le coin du terrain
		}
	}
}
