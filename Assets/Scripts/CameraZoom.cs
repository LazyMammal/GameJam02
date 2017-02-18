using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Transform target;
    private Camera cam;
	private float targetWidth;
	public float smoothTime = 0.3F;

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
    }

    private Vector3 velocity = Vector3.zero;
    void Update()
    {
		// world space distance
        var distance = Vector3.Distance(transform.position, target.position);

		// world space frustrum height at that distance
        var Fheight = FrustumHeightAtDistance(distance);

		// get desired distance based on target width
        var moveDistance = DistanceForHeightAndFOV(targetWidth / cam.aspect);
		var movePosition = (transform.position - target.position) * moveDistance / distance;

		// smoothly move to look at target from new position
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, smoothTime);
		transform.LookAt(target);
    }
}
