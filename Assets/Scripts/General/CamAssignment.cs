using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAssignment : MonoBehaviour  {
	
	public Camera MainCam;
	public Camera ClawCam;
	public Camera BodyCam;

	void Start() {
		MainCam.enabled = true;
		ClawCam.enabled = false;
		BodyCam.enabled = false;
	}

	void Update() {
		
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			DisableCamera(1);
			MainCam.enabled = true;
		}

		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			DisableCamera(2);
			ClawCam.enabled = true;
		}

		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			DisableCamera(3);
			BodyCam.enabled = true;
		}
	}

	void DisableCamera(int camTag) {

		if (camTag == 1) {
			ClawCam.enabled = false;
			BodyCam.enabled = false;
		}

		else if (camTag == 2) {
			MainCam.enabled = false;
			BodyCam.enabled = false;
		}

		else if (camTag == 3) {
			MainCam.enabled = false;
			ClawCam.enabled = false;
		}
	}
}