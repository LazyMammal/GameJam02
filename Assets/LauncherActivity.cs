using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherActivity : MonoBehaviour {

	public float cooldown = 1f;	// time between shots
	public bool active = true;	// can shoot or not
	public float spawn_distance = 2f; // distance from center of cannon that missile spawns

	public GameObject missile;	// the missile to shoot

	private float cooldown_finished = 0f;	// time when current cooldown completes

	// Use this for initialization
	void Start () {
		if (missile == null) {
			missile = Instantiate(Resources.Load("Projectile", typeof(GameObject))) as GameObject;
		}
	}

	// Update is called once per frame
	void Update () {
		if (Time.time > cooldown_finished) {
			cooldown_finished = Time.time + cooldown;
			Instantiate (missile, gameObject.transform.position + gameObject.transform.forward * spawn_distance, gameObject.transform.rotation);
		}
	}
}
