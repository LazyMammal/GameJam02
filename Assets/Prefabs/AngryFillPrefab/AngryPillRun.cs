using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPillRun : MonoBehaviour {

	public float jumpCooldownTime = 0.5f;
	public float jumpPower = 7f;
	float nextJumpTime = 0f;

	public float lifeSpan = 3f;
	float deathTime;
	public bool touching = false;

	private Rigidbody body;

	// Use this for initialization
	void Start () {
		//nextJumpTime = Time.time + nextJumpTime;
		body = gameObject.GetComponent<Rigidbody>();
		deathTime = Time.time + lifeSpan;
	}

	void Update() {
		if ((Time.time > nextJumpTime && touching) || (Time.time > nextJumpTime + 3*jumpCooldownTime ))  {
			touching = false;
			nextJumpTime = Time.time + jumpCooldownTime;
			body.AddForce( transform.forward * jumpPower + transform.up * jumpPower, ForceMode.VelocityChange );
		}
		if (Time.time > deathTime) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter( Collider other ) {
		touching = true;
	}
		
}
