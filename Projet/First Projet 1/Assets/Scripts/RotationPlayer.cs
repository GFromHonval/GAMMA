using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class RotationPlayer : MonoBehaviour {
	public float moveSpeed = 10f;
	public float turnSpeed = 50f;
	public float jumpPower;
	private bool IsJumping = false;
   
	void Update () {
		
		IsJumping = OnCollisionEnter(IsJumping);
		
		if(Input.GetKey(KeyCode.UpArrow))
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        
		if(Input.GetKey(KeyCode.DownArrow))
			transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        
		if(Input.GetKey(KeyCode.LeftArrow))
			transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        
		if(Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

		if (Input.GetKey(KeyCode.Space) && !IsJumping)
		{
			IsJumping = true;	
			transform.Translate(Vector3.up* jumpPower * Time.deltaTime);
		}
	}

	private bool OnCollisionEnter(bool jump)
	{
		return jump = !jump;
	}
}
