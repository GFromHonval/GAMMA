using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
	private float _life;

	public float GetLife
	{
		get { return _life; }
		set { _life = value; }
	}

	private void Awake()
	{
		_life = GameObject.Find("GameParameters").GetComponent<GameParameters>().LifeInThisLevel;
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		print("itch");
		if(stream.isWriting) {
 
			stream.SendNext(_life);
		}
		else {
			
			_life = (float)stream.ReceiveNext();
 
		}
         
	}


}
