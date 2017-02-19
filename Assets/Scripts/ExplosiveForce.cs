using UnityEngine;

public class ExplosiveForce : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;

	public float zapRatio = 0.5f;

    void OnCollisionEnter(Collision collision)
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            if (hit.transform.parent && hit.transform.parent.tag == "Wall")
            {
                Rigidbody rb = hit.transform.parent.GetComponent<Rigidbody>();

                if (rb != null)
				{
					if( Random.Range(0f,1f) > zapRatio )
                    	rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
					else
						rb.gameObject.SetActive(false);
				}
            }
        }

        Object.Destroy(gameObject);
    }

}
