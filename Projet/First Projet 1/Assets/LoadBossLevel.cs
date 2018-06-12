using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBossLevel : MonoBehaviour
{

	public int MusiqueLevel;

	private bool GirlIn;
	private bool BoyIn;

	private void Start()
	{
		if (!Equals(GameObject.Find("GameLogic"), null))
			GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>().GetMusiqueLevel = MusiqueLevel;
	}

	private void Update()
	{
		if (GirlIn && BoyIn)
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
