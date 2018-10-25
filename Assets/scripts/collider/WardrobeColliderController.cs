using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeColliderController : MonoBehaviour {
	public GameObject Player;
	//public GameObject SpawnPoint;
	public GameObject GiveText;
	public GameObject TakeText;
	public GameObject Thing;
	public GameObject SpawnPoint;
	private bool IsTaken = false;

	// Use this for initialization
	void Start () {
		GiveText.SetActive (false);
		TakeText.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (IsTaken) {
			Thing.transform.position = SpawnPoint.transform.position;
		}
	}

	private void GiveThing() {
		IsTaken = true;
		Thing.transform.parent = SpawnPoint.transform;
		Thing.transform.position = SpawnPoint.transform.position;
		//Thing.SetActive (false);
	}

	private void TakeThing() {
		IsTaken = false;
		Thing.transform.parent = null;
		Thing.transform.position = Player.transform.position;
		Thing.transform.parent = Player.transform;
		//Thing.SetActive (true);
	}

	private void setState(bool value) {
		IsTaken = value;
	}

	void OnTriggerStay(Collider col) {
		if (col.name == "WardrobeTrigger") {
			if (IsTaken) {
				GiveText.SetActive (false);
				TakeText.SetActive (true);
				if (Input.GetKeyDown (KeyCode.E)) {
					TakeThing ();
				}
			} else {
				GiveText.SetActive (true);
				TakeText.SetActive (false);
				if (Input.GetKeyDown (KeyCode.E)) {
					GiveThing ();
				}
			}

		}
	}

	void OnTriggerExit(Collider col) {
		if (col.name == "WardrobeTrigger") {
			if (IsTaken) {
				TakeText.SetActive (false);
			} else {
				GiveText.SetActive (false);
			}
		}
	}
}
