using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainCam : MonoBehaviour 
{
	private const float panSpeed = 40;
	private const float tiltSpeed = 40;

	private GameObject paner;
	private GameObject tilter;
	private float tilterAngle;

	void Start() {
		paner = GameObject.Find("Paner");
		tilter = GameObject.Find("Tilter");
	}

	void FixedUpdate() {
		tilterAngle = tilter.transform.localEulerAngles.x;

		if (tilterAngle > 180)
			tilterAngle -= 360;

		// Pan camera to the left.
		if (Input.GetKey(KeyCode.LeftArrow))
			paner.transform.Rotate(-Vector3.up, panSpeed * Time.deltaTime);
		
		// Pan camera to the right.
		else if (Input.GetKey (KeyCode.RightArrow))
			paner.transform.Rotate(Vector3.up, panSpeed * Time.deltaTime);

		// Tilt camera up.
		if (Input.GetKey(KeyCode.UpArrow) && tilterAngle < 80)
			tilter.transform.Rotate(Vector3.right, tiltSpeed * Time.deltaTime);

		// Tilt camera down.
		else if (Input.GetKey(KeyCode.DownArrow) && tilterAngle > -25)
			tilter.transform.Rotate(-Vector3.right, tiltSpeed * Time.deltaTime);
	}
}