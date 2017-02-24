using UnityEngine;

public class LauncherActivity : MonoBehaviour
{

    public AudioClip launchSound;

    private AudioSource source;
    public float cooldown = 1f; // time between shots
    public bool activate_firing = false;    // can shoot or not
    public float spawn_distance = 2f; // distance from center of cannon that missile spawns
    public float angular_uncertainty = 10f; // uncertainty in degrees
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;

    public GameObject missile;  // the missile to shoot

    private float cooldown_finished = 0f;   // time when current cooldown completes

    // Use this for initialization
    void Start()
    {
        if (missile == null)
        {
            missile = Instantiate(Resources.Load("Projectile", typeof(GameObject))) as GameObject;
        }
        source = GetComponent<AudioSource>();
    }

    float NextRandom()
    {
        return (float)(Random.value - 0.5) * angular_uncertainty;
    }

    Quaternion RandomRotation()
    {
        Quaternion rot = Quaternion.Euler(NextRandom(), NextRandom(), 0);
        return rot;

    }

    // Update is called once per frame
    public float rotationSpeed = 100.0F;
    void Update()
    {
        float rotation = Input.GetAxis("Vertical") * rotationSpeed;
        rotation *= Time.deltaTime;
        transform.Rotate(rotation, 0, 0);

        if (Time.time > cooldown_finished && Time.timeScale > 0)
        {
            if (Input.GetButton("Fire1"))
            {
                cooldown_finished = Time.time + cooldown;
                Fire();
                SequenceOfPlay.singleton.AddScore(10);
            }
            else if (activate_firing)
            {
                cooldown_finished = Time.time + cooldown;
                Fire();
            }
        }
    }

    void Fire()
    {
        Quaternion direct = gameObject.transform.rotation * RandomRotation();
        Instantiate(missile,
            gameObject.transform.position + gameObject.transform.forward * spawn_distance,
            direct);

        if (source)
        {
            source.pitch = Random.Range(lowPitchRange, highPitchRange);
            source.PlayOneShot(launchSound);
        }

    }
}
