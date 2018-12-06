using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour {
	//Variables
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F; 
	public float gravity = 20.0F;
	public float rotationSpeed = 6.0f;
	private Vector3 moveDirection = Vector3.zero;

	void Update() {
		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");
		CharacterController controller = GetComponent<CharacterController>();
		// is the controller on the ground?
		if (controller.isGrounded) {
			//Feed moveDirection with input.
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			//Multiply it by speed.
			//Jumping
			moveDirection *= speed;
			if (globalVariable.isTopView) {
				transform.rotation = Quaternion.LookRotation (moveDirection);
				transform.Translate (moveDirection * rotationSpeed * Time.deltaTime, Space.World);


			} else {
				moveDirection = transform.TransformDirection(moveDirection);
			}
			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;

		}



		//Applying gravity to the controller
		moveDirection.y -= gravity * Time.deltaTime;
		//Making the character move
		controller.Move(moveDirection * Time.deltaTime);
	}
}
