using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawn : MonoBehaviour {

	public float speed = 22f;
	public float life_span = 5f; // seconds

	private float despawn_time;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
		rb.AddForce( transform.forward * speed, ForceMode.VelocityChange );
		despawn_time = Time.time + life_span;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > despawn_time) {
			Destroy (gameObject);
		}
	}
}
