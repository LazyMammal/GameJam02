using UnityEngine;

public class LauncherActivity : MonoBehaviour {

	public float cooldown = 1f;	// time between shots
	public bool activate_firing = false;	// can shoot or not
	public float spawn_distance = 2f; // distance from center of cannon that missile spawns
	public float angular_uncertainty = 10f; // uncertainty in degrees

	public GameObject missile;	// the missile to shoot

	private float cooldown_finished = 0f;	// time when current cooldown completes
	
	// Use this for initialization
	void Start () {
		if (missile == null) {
			missile = Instantiate(Resources.Load("Projectile", typeof(GameObject))) as GameObject;
		}
	}

	float NextRandom() {
		return (float)( Random.value - 0.5 ) *angular_uncertainty;
	}

	Quaternion RandomRotation() {
		Quaternion rot = Quaternion.Euler ( NextRandom() , NextRandom()  , 0 );
		return rot;

	}

	// Update is called once per frame
    public float rotationSpeed = 100.0F;
    void Update() {
        float rotation = Input.GetAxis("Vertical") * rotationSpeed;
        rotation *= Time.deltaTime;
        transform.Rotate(rotation, 0, 0);
    
		if ((Input.GetButton("Fire1") || activate_firing) && Time.time > cooldown_finished) {
			cooldown_finished = Time.time + cooldown;
			Fire ();
			SequenceOfPlay.singleton.AddScore(10);
		}
	}

	void Fire() {
		Quaternion direct = gameObject.transform.rotation * RandomRotation ();
		Instantiate (missile, 
			gameObject.transform.position + gameObject.transform.forward * spawn_distance, 
			direct );
		
	}
}
