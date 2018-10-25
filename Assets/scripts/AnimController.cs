using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour {

	Animator animator;
	float vertical;
	float horizontal;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (
			Input.GetKeyDown (KeyCode.LeftArrow) ||
			Input.GetKeyDown (KeyCode.RightArrow) ||
			Input.GetKeyDown (KeyCode.UpArrow) ||
			Input.GetKeyDown (KeyCode.DownArrow) ||
			Input.GetKeyDown (KeyCode.W) ||
			Input.GetKeyDown (KeyCode.A) ||
			Input.GetKeyDown (KeyCode.S) ||
			Input.GetKeyDown (KeyCode.D)) {
			animator.SetBool ("Run", true);
		} else {
			animator.SetBool ("Run", false);
		}
		vertical = Input.GetAxis ("Vertical");
		horizontal = Input.GetAxis ("Horizontal");
		if (vertical == 0 && horizontal == 0) {
		} else {
		}
	}
}
