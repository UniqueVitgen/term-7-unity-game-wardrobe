using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrigger : MonoBehaviour {
	public GameObject Player;
	public GameObject SpawnPoint;

	void OnTriggerEnter(Collider col) {
		if (col.name == "EnterTrigger") {
			Player.transform.position = SpawnPoint.transform.position;
		}
	}

	void OnTriggerStay(Collider col) {
		//if (col.name == "EnterTrigger") {
		//	if (Input.GetKeyDown(KeyCode.E)) {
		//		Player.transform.position = SpawnPoint.transform.position;
		//	}
		//}
	}
	
}
