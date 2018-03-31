using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BoutonEtPlateforme : MonoBehaviour
{

	public Transform BassePosition;
	public GameObject Plateforme;
	private Vector3 MouvementDirection;
	private Vector3 BassePose;
	private Vector3 HautePose;
	private bool IsPressing;
	private bool IsInArea;
	private Vector3 WherePlayer;
	private string PlayerTag;
	// Update is called once per frame

	private void Start()
	{
		BassePose = new Vector3(transform.position.x, BassePosition.position.y, transform.position.z);
		HautePose = transform.position;
	}

	private void Update()
	{
		Debug.Log(IsPressing);
		if (IsPressing && transform.position.y > BassePose.y)
		{
			transform.position = Vector3.MoveTowards(transform.position, BassePose, Time.deltaTime);
		}
		if (GameObject.FindGameObjectWithTag(PlayerTag).transform.position.y < WherePlayer.y)
		{
			IsPressing = false;
		}
		if (!IsInArea && transform.position.y < HautePose.y)
		{
			transform.position = Vector3.MoveTowards(transform.position, HautePose, Time.deltaTime);
		}
	}
	
	private void OnCollisionEnter(Collision other)
	{
		float high = GetComponent<BoxCollider>().size.y * transform.localScale.y / 2;
		Vector3 contact = other.contacts[0].point;
		WherePlayer = other.transform.position;
		PlayerTag = other.gameObject.tag;
		if (contact.y >= transform.position.y + high)
		{
			IsPressing = true;
		}
	}
}
