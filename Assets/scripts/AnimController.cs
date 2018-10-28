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
		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");

		if (
			vertical > 0.1f
			||
			vertical < -0.1f
			||
			horizontal > 0.1f
			||
			horizontal < -0.1f
			//Input.GetKeyDown (KeyCode.LeftArrow) ||
			//Input.GetKeyDown (KeyCode.RightArrow) ||
			//Input.GetKeyDown (KeyCode.UpArrow) ||
			//Input.GetKeyDown (KeyCode.DownArrow) ||
			//Input.GetKeyDown (KeyCode.W) ||
			//Input.GetKeyDown (KeyCode.A) ||
			//Input.GetKeyDown (KeyCode.S) ||
			//Input.GetKeyDown (KeyCode.D)
			) {
			animator.SetInteger ("state", 1);
		} else {
			if (animator.GetInteger ("state") == 1) {
				animator.SetInteger ("state", 5);
				print ("state stay");
			}
			//animator.SetInteger ("state", 0);
		}
		vertical = Input.GetAxis ("Vertical");
		horizontal = Input.GetAxis ("Horizontal");
		if (vertical == 0 && horizontal == 0) {
		} else {
		}
	}
}
