using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTool : MonoBehaviour {

	public int degreesPerStep = 4;
	private int targetDegrees = 0;
	private float nextUpdate = 0f;

	// Update is called once per frame
	public void Spin ( int spin ) {
		Debug.Log ("Spinning " + spin + " degrees");
		targetDegrees += spin;
	}

	void FixedUpdate() {
		if (Time.time > nextUpdate && targetDegrees > 0) {
			gameObject.transform.RotateAround (gameObject.transform.position, Vector3.up, degreesPerStep);
			nextUpdate = Time.time + .01f;
			targetDegrees -= degreesPerStep;
		}	
	}
}
