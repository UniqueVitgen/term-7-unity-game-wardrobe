using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour { 
	public Camera cam1;
	public Camera cam2;
	public Camera cam1Top2;
	bool isNeed2Camera = false;

	// Use this for initialization
	void Start () {
		cam1.enabled = true;
		cam2.enabled = false;
		cam1Top2.enabled = false;
	}
	
	// Update is called once per frame
	void Update () { 
		if (Input.GetKeyDown(KeyCode.V)) {
			globalVariable.setTopView (!globalVariable.isTopView);
		}
		if (globalVariable.isTopView) {
			if (isNeed2Camera) {
				if (!cam1Top2.enabled) {
					cam1Top2.enabled = true;
					cam2.enabled = false;
					cam1.enabled = false;
				}
			} else {
				if (!cam1.enabled) {
					cam1.enabled = true;
					cam2.enabled = false;
					cam1Top2.enabled = false;
				}
			}
		} else {
			if (!cam2.enabled) {
				cam2.enabled = true;
				cam1.enabled = false;
				cam1Top2.enabled = false;
			}
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.name == "SpawnPointCamera") {
			isNeed2Camera = true;
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.name == "SpawnPointCamera") {
			isNeed2Camera = false;
		}
	}
}
