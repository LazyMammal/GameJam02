﻿using UnityEngine;

public class SpawnItems : MonoBehaviour , CommandInterface
{
    public float radius = 15f;
    public int count = 1;
    public GameObject[] prefabs;
    public bool randomizeRotation = false;
    public bool nestItem = true;

    public Vector2 repeatTimer = Vector2.zero;
    private float nextTime = 0f;

    void Update()
    {
        if (repeatTimer != Vector2.zero && Time.time > nextTime)
        {
            nextTime = Time.time + Random.Range( repeatTimer.x, repeatTimer.y );
            DoCommand();
        }

    }

    public void DoCommand()
    {
        foreach (var item in prefabs)
        {
            for (int i = 0; i < count; i++)
            {
                Quaternion rot = randomizeRotation ? Quaternion.Euler(0, Random.value * 360, 0) : transform.rotation;
                Vector2 pos2 = Random.insideUnitCircle;
                Vector3 pos3 = new Vector3(pos2.x, 0, pos2.y);
                pos3 *= radius;
                GameObject go = (GameObject)Instantiate(item, transform.position + pos3, rot);
                if (nestItem)
                {
                    go.transform.SetParent(transform);
                }
            }
        }
    }
}
