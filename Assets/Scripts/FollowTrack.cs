using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTrack : MonoBehaviour
{
    public float trackSpeed = 25f;
    private float Ztarget;
	private Quaternion rotationTarget;
    void Start()
    {
		Ztarget = transform.position.z;
		rotationTarget = transform.rotation;
    }

    private Vector3 velocity = Vector3.zero;
    void Update()
    {
		Vector3 movePosition = new Vector3(transform.position.x, transform.position.y, Ztarget);
		transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, 1f / trackSpeed);

		transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, Time.deltaTime * trackSpeed);
    }
}
