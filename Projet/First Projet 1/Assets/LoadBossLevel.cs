using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBossLevel : MonoBehaviour
{

	public int MusiqueLevel;
	public int NotesToCollect;

	private bool GirlIn;
	private bool BoyIn;
	private int NotesCollected;

	public int GetNotesCollected
	{
		get { return NotesCollected; }
		set { NotesCollected = value; }
	}
	
	private void Start()
	{
		NotesCollected = 0;
		if (!Equals(GameObject.Find("GameLogic"), null))
			GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>().GetMusiqueLevel = MusiqueLevel;
	}

	private void Update()
	{
		if (GirlIn && BoyIn && NotesCollected == NotesToCollect)
		{
			if (PhotonNetwork.isMasterClient)
			{
				Animator animator = GameObject.Find("FadeTransition").GetComponent<Animator>();
				animator.SetTrigger("FadeOut");
			}
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "PlayerGirl")
			GirlIn = true;

		if (other.gameObject.tag == "PlayerBoy")
			BoyIn = true;	
	}
}
