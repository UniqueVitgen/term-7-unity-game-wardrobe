using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStayController : MonoBehaviour {
	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 20.0F;

	void Update()
	{
		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");
		CharacterController controller = GetComponent<CharacterController>();
		// is the controller on the ground?
		if (controller.isGrounded) {
			//Feed moveDirection with input.
			moveDirection = new Vector3(0, 0, 0);
			moveDirection = transform.TransformDirection(moveDirection);
			//Multiply it by speed.
			//Jumping

		}



		//Applying gravity to the controller
		moveDirection.y -= gravity * Time.deltaTime;
		//Making the character move
		controller.Move(moveDirection * Time.deltaTime);
	}
}
