using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceWall : MonoBehaviour, CommandInterface {

	public GameObject wall_prefab;
	int x_size = 2;
	public int y_size = 10;
	public int z_size = 50;
	public int distance = -5;


	public void PlaceSingleWall(int x, int y, int z, int forward) {
		Vector3 offset = new Vector3 (x, y, z);
		GameObject wall = (GameObject) Instantiate (wall_prefab, 
			gameObject.transform.position + gameObject.transform.forward * forward + offset, 
			gameObject.transform.rotation );
		wall.transform.SetParent(transform);
	}
		
	public void DoCommand() {
		int maxX = Random.Range (0, x_size);
		int maxY = Random.Range (1, y_size);
		int maxZ = Random.Range (1, z_size);
		for (int x = 0; x < maxX; x++) {
			for (int y = 0; y < maxY; y++) {
				for (int z = 0; z < maxZ; z++) {
					PlaceSingleWall (x, y, z - maxZ/2, distance);
				}
			}
		}
	}

}
