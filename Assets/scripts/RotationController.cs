using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour {
	public Camera yourCam;
	public float mouseSpeed = 6;
	public Transform player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//rotating here

		//v1
		//transform.right = Vector3.Slerp(transform.right, -Vector3.right * Input.GetAxis("Horizontal"), 0.1f);

		//v2
		//float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

		//rotation *= Time.deltaTime;

		//transform.Rotate(0, rotation, 0);

		//v3

		//Vector3 NextDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		//if (NextDir != Vector3.zero)
		//transform.rotation = Quaternion.LookRotation(NextDir);

		//v4


		//var rotationDirection = new Vector3(horizontal, 0, vertical);
		//transform.rotation = Quaternion.LookRotation(rotationDirection, Vector3.up);


		//v5 


		float X = Input.GetAxis("Mouse X") * mouseSpeed;
		float Y = Input.GetAxis("Mouse Y") * mouseSpeed;

		player.Rotate(0, X, 0); // Player rotates on Y axis, your Cam is child, then rotates too


		// To scurity check to not rotate 360º 
		if (yourCam.transform.eulerAngles.x + (-Y) > 80 && yourCam.transform.eulerAngles.x + (-Y) < 280)
		{ }
		else
		{

			yourCam.transform.RotateAround(player.position, yourCam.transform.right, -Y);
		}
		//rotation end
		
	}
}
