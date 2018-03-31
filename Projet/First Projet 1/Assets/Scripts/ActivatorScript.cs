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
	public bool createMode;
	public GameObject n;
	private GameObject note,gm;
	
	
	// Use this for initialization
	void Start ()
	{
		old = sr.color;
		gm = GameObject.Find("GameManagerMusic");
	}

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
		if (createMode )
		{
			if (Input.GetKeyDown(key))
				Instantiate(n, transform.position , Quaternion.identity);
		}
		else
		{


			if (Input.GetKeyDown(key))
				StartCoroutine(Pressed());
			if (Input.GetKeyDown(key) && active)
			{
				Destroy(note);
				gm.GetComponent<GameManagerMusic>().AddStreak();
				AddScore();
				active = false;
			}
			else if (Input.GetKeyDown(key) && !active)
			{
				gm.GetComponent<GameManagerMusic>().ResetStreak();
			}
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
		//gm.GetComponent<GameManagerMusic>().ResetStreak();
	}

	void AddScore()
	{
		PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + gm.GetComponent<GameManagerMusic>().GetScore());
	}

	IEnumerator Pressed()
	{
		
		sr.color = new Color(0,0,0);
		yield return new WaitForSeconds(sec);
		sr.color = old;
	}
}
