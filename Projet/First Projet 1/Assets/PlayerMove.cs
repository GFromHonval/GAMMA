using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	public float speed = 10f;
	public float jumpForce = 8f;
	public float gravity = 30f;
	public Vector3 moveDir = Vector3.zero;

	void Start()
	{

	}

	void Update()
	{
		CharacterController controller = gameObject.GetComponent<CharacterController>();

		if (controller.isGrounded)
		{
			moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

			moveDir = transform.TransformDirection(moveDir);

			moveDir *= speed;

			if (Input.GetButtonDown("Jump"))
			{
				moveDir.y = jumpForce;
			}
		}


		moveDir.y -= gravity * Time.deltaTime;
		controller.Move(moveDir * Time.deltaTime);

	}
}
