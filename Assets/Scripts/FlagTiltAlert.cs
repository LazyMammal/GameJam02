using System.Collections;
using UnityEngine;

public class FlagTiltAlert : MonoBehaviour
{
    public AudioClip crashSound;
    public AudioClip reviveSound;

    public float tiltAngle = 45, blinkTime = 2.0f, reviveDelay = .5f;
    private Renderer[] rlist;
    //private GameObject truck;
    private bool isBlinking = false;
    private Quaternion rotationTarget;
    public float trackSpeed = 2f, flipSpeed = 15f, killFloor = 0f, reviveHover = 1f;
    private AudioSource source;
    private Vector3 target;
    private Rigidbody rb;
    private float speed;
    
    void Start()
    {
        //truck = transform.Find("Truck").gameObject;
        rlist = (Renderer[])gameObject.GetComponentsInChildren<Renderer>();
        target = transform.position;
        rotationTarget = transform.rotation;
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        speed = trackSpeed;
    }

    void Update()
    {
        // check if we passed through the floor
        if (transform.position.y < killFloor)
        {
            DoRevive();
        }
        else
        {
            // get angle from vertical
            float dot = Vector3.Dot(transform.up, Vector3.up);
            if (dot < Mathf.Cos(tiltAngle * Mathf.Deg2Rad))
            {
                DoRevive();
            }
        }

        // get back on track
        Vector3 moveTarget = new Vector3(transform.position.x, transform.position.y, target.z);
        transform.position = Vector3.Slerp(transform.position, moveTarget, Time.deltaTime * speed );
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, Time.deltaTime * speed);
    }

    private void DoRevive()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            speed = flipSpeed;

            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            transform.position = new Vector3(transform.position.x, target.y + reviveHover, target.z);

            SequenceOfPlay.singleton.AddScore(1000);
            source.PlayOneShot(crashSound);

            StartCoroutine("ReviveSound");
            StartCoroutine("Blink");
        }
    }


    IEnumerator ReviveSound()
    {
        yield return new WaitForSeconds(reviveDelay);
        source.PlayOneShot(reviveSound);
    }

    IEnumerator Blink()
    {
        if (rlist.Length > 0)
        {
            var endTime = Time.time + blinkTime;
            while (Time.time < endTime)
            {
                SetVisibility(false);
                yield return new WaitForSeconds(.2f);
                SetVisibility(true);
                yield return new WaitForSeconds(.2f);
            }
        }

        // reset from "blink" state
        isBlinking = false;
        rb.useGravity = true;
        speed = trackSpeed;
    }

    private void SetVisibility(bool enabled)
    {
        foreach(var r in rlist)
        {
            r.enabled = enabled;
        }
    }
}
