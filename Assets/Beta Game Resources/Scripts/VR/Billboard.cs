using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	This script is used in LOD for objects that are so far away
	that they get rendered as a sprite instead.
	
	This script right here gets the player and makes the sprite
	turn to face the player

	Questions about this script are refrerred to Elias
	 */

public class Billboard : MonoBehaviour {

	private GameObject targetObj;
	private Transform target;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		// If the player has been found, look at them.
		if (targetObj) {
			Vector3 relativePos = target.position - transform.position;
			relativePos.y = 0;

			Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
			transform.rotation = rotation;
		} else {
			// If it can't find the player it tries to find the player.
			if (targetObj = PlayerBehaviour._instance.gameObject) {
				target = targetObj.transform;
			}
		}


	}
}
