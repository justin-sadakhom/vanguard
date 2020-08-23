using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {

	private const float clawTurnRate = 90f;
	private const float clawShiftRate = 20f;
	private const float clawPinchRate = 10f;
	private const float elbowMoveRate = 20f;
	private const float shoulderMoveRate = 20f;
	private const float armTurnRate = 15f;

	private bool clockwiseRot;
	private bool isTouchingGround;

	private GameObject clawCam;
	private GameObject elbow;
	private GameObject finger1;
	private GameObject finger2;
	private GameObject shoulder;
	private GameObject terrain;
	private GameObject turret;
	private GameObject wrist;

	private float clawCamAngle;
	private float elbowAngle;
	private float fingerAngle;
	private float shoulderAngle;
	private float turretAngle;

	void Start () {

		clawCam = GameObject.Find("Claw Cam");
		elbow = GameObject.Find("Elbow");
		finger1 = GameObject.Find("Finger 1");
		finger2 = GameObject.Find("Finger 2");
		shoulder = GameObject.Find("Shoulder");
		terrain = GameObject.Find("Terrain");
		turret = GameObject.Find("Turret");
		wrist = GameObject.Find("Wrist");
	}

	void Update () {

		// Change rotation of claw.
		if (Input.GetKeyDown(KeyCode.Period))
			clockwiseRot = !clockwiseRot;
	}

	void FixedUpdate () {

		if (terrain != null)
			isTouchingGround = terrain.GetComponent<TriggerCheck>().touchesGround();
		else
			isTouchingGround = false;

		getComponentAngles();

		// Rotate claw.
		if (Input.GetKey(KeyCode.R) && !isTouchingGround) {

			if (clockwiseRot)
				wrist.transform.Rotate(-Vector3.forward, clawTurnRate * Time.deltaTime);
			else
				wrist.transform.Rotate(Vector3.forward, clawTurnRate * Time.deltaTime);
		}

		// Shift claw up.
		if (Input.GetKey(KeyCode.O) && clawCamAngle < 30)
			clawCam.transform.Rotate(Vector3.right, clawShiftRate * Time.deltaTime);

		// Shift claw down.
		if (Input.GetKey(KeyCode.L) && clawCamAngle > -30 && !isTouchingGround)
			clawCam.transform.Rotate(-Vector3.right, clawShiftRate * Time.deltaTime);

		// Pinch claw.
		if (Input.GetKey(KeyCode.Minus) && fingerAngle < 3) {
			finger1.transform.Rotate(Vector3.right, clawPinchRate * Time.deltaTime);
			finger2.transform.Rotate(Vector3.left, clawPinchRate * Time.deltaTime);
		}

		// Unpinch claw.
		if (Input.GetKey(KeyCode.Equals) && fingerAngle > -30) {
			finger1.transform.Rotate(-Vector3.right, clawPinchRate * Time.deltaTime);
			finger2.transform.Rotate(-Vector3.left, clawPinchRate * Time.deltaTime);
		}

		// Raise elbow.
		if (Input.GetKey(KeyCode.I) && elbowAngle < 90)
			elbow.transform.Rotate(Vector3.right, elbowMoveRate * Time.deltaTime);

		// Lower elbow.
		if (Input.GetKey(KeyCode.K) && elbowAngle > 0 && !isTouchingGround)
			elbow.transform.Rotate(-Vector3.right, elbowMoveRate * Time.deltaTime);

		// Raise shoulder.
		if (Input.GetKey(KeyCode.U) && shoulderAngle > -100 && !isTouchingGround)
			shoulder.transform.Rotate (-Vector3.right, shoulderMoveRate * Time.deltaTime);

		// Lower shoulder.
		if (Input.GetKey(KeyCode.J) && shoulderAngle < 0)
			shoulder.transform.Rotate (Vector3.right, shoulderMoveRate * Time.deltaTime);

		// Rotate arm counter-clockwise.
		if (Input.GetKey(KeyCode.N) && turretAngle > -13)
			turret.transform.Rotate (Vector3.down, armTurnRate * Time.deltaTime);

		// Rotate arm clockwise.
		if (Input.GetKey(KeyCode.M) && turretAngle < 15)
			turret.transform.Rotate (Vector3.up, armTurnRate * Time.deltaTime);
	}

	private void getComponentAngles() {

		clawCamAngle = clawCam.transform.localEulerAngles.x;
		elbowAngle = elbow.transform.localEulerAngles.x;
		fingerAngle = finger1.transform.localEulerAngles.x;
		shoulderAngle = shoulder.transform.localEulerAngles.x;
		turretAngle = turret.transform.localEulerAngles.y;

		if (clawCamAngle > 180)
			clawCamAngle -= 360;

		if (elbowAngle > 180)
			elbowAngle -= 360;

		if (fingerAngle > 180)
			fingerAngle -= 360;

		if (shoulderAngle > 180)
			shoulderAngle -= 360;

		if (turretAngle > 180)
			turretAngle -= 360;
	}
}