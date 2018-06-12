using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class RockMove : MonoBehaviour
{

	public bool OnlyGirl;
	public bool OnlyBoy;

	private float Force;
	private bool AllPlayers;
	private Rigidbody Rock;
	private float OriginalMass;

	private void Start()
	{
		Force = 500;
		AllPlayers = !(OnlyBoy || OnlyGirl);
		Rock = GetComponent<Rigidbody>();
	}


	private void OnCollisionStay(Collision other)
	{
		Vector3 Push = (transform.position - other.transform.position).normalized;
		Push.y = 0;
		if (Rock.constraints == RigidbodyConstraints.FreezePositionX)
		{
			Push.x = Push.x * 10;
			Push.z = 0;
		}
		else
		{
			Push.z = Push.z * 5;
			Push.x = 0;
		}

		if (AllPlayers && other.gameObject.tag == "PlayerBoy" || AllPlayers && other.gameObject.tag == "PlayerGirl"
		   || OnlyBoy && other.gameObject.tag == "PlayerBoy"  || OnlyGirl && other.gameObject.tag == "PlayerGirl")
		{
			transform.position = Vector3.MoveTowards(transform.position, transform.position + Push, Time.deltaTime * 5);
			//Rock.AddForce(x * Force, 0, 0, ForceMode.Impulse);
			//Rock.AddForce(0, 0, z * Force, ForceMode.Impulse);
			Rock.AddForce(Physics.gravity * 400);
		}
	}
}
