using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMoveTakeAll : MonoBehaviour
{
	private List<GameObject> Contacts;

	public List<GameObject> GetContacts
	{
		get { return Contacts; }
		set { Contacts = value; }
	}

	private void Start()
	{
		Contacts = new List<GameObject>();
	}

	private void OnTriggerEnter(Collider other)
	{
		Contacts.Add(other.gameObject);
	}

	private void OnTriggerExit(Collider other)
	{
		Contacts.Remove(other.gameObject);
	}
}
