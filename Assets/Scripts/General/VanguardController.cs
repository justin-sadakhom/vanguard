using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardController : MonoBehaviour {

	private GameObject vanguard;

	private const float acceleration = 0.1f;
	private const float deceleration = -7.3575f;
	private const float maxSpeed = 0.312928f;

	private float speed;
	private float turnSpeed;

	void Start() {
		vanguard = GameObject.Find("Vanguard");
		DontDestroyOnLoad(vanguard);
		speed = 0;
	}

	void FixedUpdate() {

		turnSpeed = 15f * speed;

		if (validPosition()) {

			// Drive forward.
			if (Input.GetKey(KeyCode.W)) {
				setSpeed(finalSpeed(speed, acceleration, Time.deltaTime), false, true);
				transform.Translate(Vector3.back * displacement(speed, Time.deltaTime, acceleration)); // Vector3.back because 3D model faces wrong way.

				// Turn left.
				if (Input.GetKey(KeyCode.A) && speed > 0)
					transform.Rotate(-Vector3.up, turnSpeed * Time.deltaTime);

				// Turn right.
				else if (Input.GetKey(KeyCode.D) && speed > 0)
					transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
			}

			// Decelerate.
			else if (speed > 0) {
				setSpeed(finalSpeed(speed, deceleration, Time.deltaTime), true, true);
				transform.Translate(Vector3.back * displacement(speed, Time.deltaTime, deceleration));
			}
			
			// Drive backward.
			if (Input.GetKey(KeyCode.S)) {
				setSpeed(finalSpeed(speed, -acceleration, Time.deltaTime), false, false);
				transform.Translate(Vector3.back * displacement(speed, Time.deltaTime, -acceleration)); // Same issue as above.

				// Turn left.
				if (Input.GetKey(KeyCode.A) && speed < 0)
					transform.Rotate(-Vector3.up, turnSpeed * Time.deltaTime);

				// Turn right.
				else if (Input.GetKey(KeyCode.D) && speed < 0)
					transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
			}

			// Decelerate.
			else if (speed < 0) {
				setSpeed(finalSpeed(speed, -deceleration, Time.deltaTime), true, false);
				transform.Translate(Vector3.back * displacement(speed, Time.deltaTime, -deceleration));
			}
		}
	}

	/*
	 * Check if treads are touching the ground.
	 */
	private bool validPosition() {

		float rotationX = transform.localEulerAngles.x;
		float rotationZ = transform.localEulerAngles.z;

		if (rotationX > 180)
			rotationX -= 360;

		if (rotationZ > 180)
			rotationZ -= 360;

		return rotationX < 80 && rotationX > -80 && rotationZ < 60 && rotationZ > -60;
	}

	private float displacement(float speed, float time, float acceleration) {
		return speed * time + 0.5f * acceleration * (float)Math.Pow(Time.deltaTime, 2);
    }

	private float finalSpeed(float initialSpeed, float acceleration, float time) {
		return initialSpeed + acceleration * time;
    }

	private void setSpeed(float newSpeed, bool friction, bool forward) {

		if (forward && newSpeed > speed)
			speed = maxSpeed;
		else if (!forward && -newSpeed > -speed)
			speed = -maxSpeed;
		else if (friction && forward && newSpeed < 0)
			speed = 0;
		else if (friction && !forward && newSpeed > 0)
			speed = 0;
		else
			speed = newSpeed;
    }
}