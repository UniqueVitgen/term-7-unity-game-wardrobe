using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttendantController : MonoBehaviour {

	NavMeshAgent nNavMeshAgent;
	public Camera camera;

	// Use this for initialization
	void Start () {
		nNavMeshAgent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (camera != null) {
			Ray ray = camera.ScreenPointToRay (Input.mousePosition);

			RaycastHit hit;

			if (Input.GetMouseButtonDown (0)) {
				if (Physics.Raycast (ray, out hit, 100)) {
					nNavMeshAgent.destination = hit.point;
				}
			}

			if (nNavMeshAgent.remainingDistance < nNavMeshAgent.stoppingDistance) {
			} else {
			}
		}
	}
}
