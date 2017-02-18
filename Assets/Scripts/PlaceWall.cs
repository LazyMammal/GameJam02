using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceWall : MonoBehaviour, CommandInterface {

	public GameObject wall_prefab;
	public int x_size = 1;
	public int y_size = 3;
	public int z_size = 10;
	public float forward_distance = 10f;


	public void PlaceSingleWall(int x, int y, int z) {
		Vector3 offset = new Vector3 (x, y, z);
		GameObject wall = (GameObject) Instantiate (wall_prefab, 
			gameObject.transform.position + gameObject.transform.forward * forward_distance + offset, 
			gameObject.transform.rotation );
		wall.transform.SetParent(transform);
	}

	public void DoCommand() {
		for (int x = 0; x < x_size; x++) {
			for (int y = 0; y < y_size; y++) {
				for (int z = 0; z < z_size; z++) {
					PlaceSingleWall (x, y, z - z_size/2);
				}
			}
		}
	}

}
