using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    // for Dolly zoom
    public Transform target;
    private Camera cam;
	private float targetWidth;

    // for Spin command
    public float turnAngle = 180f;
    public float turnSpeed = 10f;
    private float targetDirection = 0f;


    float FrustumHeightAtDistance(float distance)
    {
        return 2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    float FOVForHeightAndDistance(float height, float distance)
    {
        return 2.0f * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg;
    }

    float DistanceForHeightAndFOV(float height)
    {
        return height * 0.5f / Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    float WidthFromHeight(float height)
    {
        return height * cam.aspect;
    }

    float HeightFromWidth(float width)
    {
        return width / cam.aspect;
    }

    void Start()
    {
		// get camera
        cam = gameObject.GetComponent<Camera>(); ;

		// get horizontal size of target
		var sz = target.GetComponentInChildren<Renderer>().bounds.size;
		targetWidth = Mathf.Max( sz.x, sz.z );

        // get current direction to target
        targetDirection = Quaternion.LookRotation(target.position - transform.position).eulerAngles.y;
    }

    private Vector3 velocity = Vector3.zero;
    void Update()
    {
		// world space distance
        var distance = Vector3.Distance(transform.position, target.position);
        var targetVec = target.position - transform.position;

		// get desired distance based on target width
        var moveDistance = DistanceForHeightAndFOV(targetWidth / cam.aspect);
		var movePosition = -targetVec * moveDistance / distance;

        // get current LookAt direction to target
        var lookRot = Quaternion.LookRotation(target.position - transform.position);
        var curDirection = lookRot.eulerAngles.y;

        // get tangent to LookAt for Spin movement
        var moveVector = Vector3.Cross( targetVec, Vector3.up );
        moveVector.Normalize();

        // adjust movePosition based on Spin target
        var turnMag = Mathf.Abs( curDirection - targetDirection );
        if( turnMag > 0.5f )
            movePosition += moveVector; // * turnMag;

		// smoothly move to look at target from new position
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, 1f / turnSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetVec), Time.deltaTime * turnSpeed);
    }
}
