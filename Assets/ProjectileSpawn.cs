using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawn : MonoBehaviour {

	public float speed = 22f;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
		rb.AddForce( transform.forward * speed, ForceMode.VelocityChange );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
