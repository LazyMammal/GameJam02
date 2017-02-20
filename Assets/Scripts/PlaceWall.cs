using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceWall : MonoBehaviour, CommandInterface
{

    public GameObject wall_prefab;
    public int x_size = 3;
    public int y_size = 2;
    public int z_size = 1;
    public float forward_distance = 10f;

    public Vector2 repeatTimer = Vector2.zero;
    private float nextTime = 0f;

    public bool nested = true;

    void Update()
    {
        if (repeatTimer != Vector2.zero && Time.time > nextTime)
        {
            nextTime = Time.time + Random.Range( repeatTimer.x, repeatTimer.y );
            DoCommand();
        }

    }

    public void PlaceSingleWall(int x, int y, int z, float forward)
    {
        Vector3 stagger = transform.right * 0.5f * (float)(y % 2);

        Vector3 offset = new Vector3(x, y, z);
        GameObject wall = (GameObject)Instantiate(wall_prefab,
            gameObject.transform.position + gameObject.transform.forward * forward + offset + stagger,
            gameObject.transform.rotation);
        if (nested)
            wall.transform.SetParent(transform);
    }

    public void DoCommand()
    {
		int xCount = x_size + Random.Range(0,2);
		int yCount = y_size + Random.Range(0,2);

		float f = Random.Range(0f, forward_distance);

        for (int y = 0; y < yCount; y++)
        {
            for (int x = 0; x < xCount - y; x++)
            {
                for (int z = 0; z < z_size; z++)
                {
                    PlaceSingleWall(x, y, z - z_size / 2, f);
                }
            }
        }
    }

}
