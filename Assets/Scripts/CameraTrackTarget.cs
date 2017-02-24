using UnityEngine;

public class CameraTrackTarget : MonoBehaviour
{
    public Transform target;
    public float speed = 25f;

    private Vector3 velocity = Vector3.zero;
    void Update()
    {
        var movePosition = transform.position;
        movePosition.x = target.transform.position.x;

        if (speed > 0)
        {

            // smoothly move to look at target from new position
            transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, 1f / speed);
        }
        else
        {
            transform.position = movePosition;
        }
    }
}
