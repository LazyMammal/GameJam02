using UnityEngine;

public class CameraSpin : MonoBehaviour
{
    public Transform target;
    public float turnAngle = 180f;
    public float turnSpeed = 1f;

	private Camera cam;
    private float targetDirection = 0f;
	private bool isTurning = false;

    void Start()
    {
		cam = gameObject.GetComponent<Camera>();
		targetDirection = cam.transform.rotation.eulerAngles.y;
    }

    void Update()
    {
		// CameraZoom is taking care of actually looking at battlefield
		// this script needs to just move the position of the camera

		var curDirection = cam.transform.rotation.eulerAngles.y;
    }
}
