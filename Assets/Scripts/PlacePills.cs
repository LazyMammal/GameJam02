using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePills : MonoBehaviour {

	public GameObject angry_pill;
	public int distance = 5;

	public void SpawnAngryPill (float y, int forward) {
		Vector3 offset = new Vector3 (0, 0, y);
		GameObject angryPill = (GameObject) Instantiate (angry_pill, 
			gameObject.transform.position + gameObject.transform.forward * forward + offset, 
			gameObject.transform.rotation );
		angryPill.transform.SetParent(transform);
	}

	public void Spawn() {
		int maxY = Random.Range (1, 3);

		for (int y = 0; y < maxY; y++) {
			SpawnAngryPill( .5f + 4f * ( y - maxY / 2f ) , distance);
		}
	}

}
