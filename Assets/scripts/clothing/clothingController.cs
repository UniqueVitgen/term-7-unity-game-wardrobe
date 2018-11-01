using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clothingController : MonoBehaviour {
	public SkinnedMeshRenderer playerSkin;
	public GameObject obj;
	public GameObject rootBone;

	// Use this for initialization
	void Start () {

		SkinnedMeshRenderer[] renderers = obj.GetComponentsInChildren<SkinnedMeshRenderer> ();
		if (renderers.Length > 0) {
			for (int i = 0; i < renderers.Length; i++) {
				renderers [i].rootBone = rootBone.transform;
				//renderers[i].
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
