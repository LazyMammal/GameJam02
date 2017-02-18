using UnityEngine;

public class FlagTiltAlert : MonoBehaviour
{
    public float tiltAngle = 45;

    // Called for physics updates
    void FixedUpdate()
    {
        // get angle from vertical

        float dot = Vector3.Dot(transform.up, Vector3.up);

        if (dot < Mathf.Cos(tiltAngle * Mathf.Deg2Rad))
        {
            InfoBox.PrependLine("Flag Destroyed");
			SequenceOfPlay.singleton.DelayedUpdateFlagCount ();
            Destroy(gameObject);
        }
    }
}
