using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

	private int CountDestroy;

	public int GetCountDestroyed
	{
		get { return CountDestroy; }
	}
	
	// Use this for initialization
	void Start ()
	{
		CountDestroy = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Destroy(other.gameObject);
		CountDestroy++;
	}
}
