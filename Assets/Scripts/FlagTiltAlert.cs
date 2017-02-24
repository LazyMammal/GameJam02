using System.Collections;
using UnityEngine;

public class FlagTiltAlert : MonoBehaviour
{
    public float tiltAngle = 45, blinkTime = 2.0f;
    private GameObject truck;
    private bool isBlinking = false;
	private Quaternion rotationTarget;
    public float flipSpeed = 15f;

    void Start()
    {
        truck = transform.Find("Truck").gameObject;
		rotationTarget = transform.rotation;
    }
    void FixedUpdate()
    {
        // get angle from vertical
        float dot = Vector3.Dot(transform.up, Vector3.up);

        if (dot < Mathf.Cos(tiltAngle * Mathf.Deg2Rad))
        {
            if (!isBlinking)
            {
                StartCoroutine("Blink");
                SequenceOfPlay.singleton.AddScore(1000);
            }
        }

        if( isBlinking )
    		transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, Time.deltaTime * flipSpeed);

    }

    IEnumerator Blink()
    {
        if (truck && !isBlinking)
        {
            isBlinking = true;
            var endTime = Time.time + blinkTime;
            while (Time.time < endTime)
            {
                truck.SetActive(false);
                yield return new WaitForSeconds(.2f);
                truck.SetActive(true);
                yield return new WaitForSeconds(.2f);
            }
            isBlinking = false;
        }
    }
}
