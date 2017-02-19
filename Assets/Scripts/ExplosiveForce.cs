using UnityEngine;

public class ExplosiveForce : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;

    void OnCollisionEnter(Collision collision)
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            if (hit.tag != "Projectile")
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
            }
        }

        Object.Destroy(gameObject);
    }

}
