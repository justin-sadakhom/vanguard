using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour {

	private bool isTouchingGround = false;
	
	void OnTriggerEnter(Collider col) {

		if (col.gameObject.name == "Trigger")
			isTouchingGround = true;
	}

	void OnTriggerExit(Collider col) {

		if (col.gameObject.name == "Trigger")
			isTouchingGround = false;
	}

	public bool touchesGround() {
		return isTouchingGround;
	}
}