using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorScript : MonoBehaviour
{

	public KeyCode key;
	private bool active = false;
	private SpriteRenderer sr;
	Color old ;
	public float sec;

	private GameObject note;
	// Use this for initialization
	void Start ()
	{
		old = sr.color;
	}

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(key))
			StartCoroutine(Pressed());
		if (Input.GetKeyDown(key) && active)
		{
			Destroy(note);
			AddScore();
			active = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		active = true;
		if (col.gameObject.tag == "Note")
		{
			note = col.gameObject;
		}
		
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		active = false;
	}

	void AddScore()
	{
		PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 100);
	}

	IEnumerator Pressed()
	{
		
		sr.color = new Color(0,0,0);
		yield return new WaitForSeconds(sec);
		sr.color = old;
	}
}
