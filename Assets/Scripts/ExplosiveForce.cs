using UnityEngine;

public class ExplosiveForce : MonoBehaviour
{
    public GameObject particle;
    public GameObject big_wall;

    public float radius = 5.0F;
    public float power = 10.0F;

    public float zapRatio = 0.5f;
    public float bigRatio = .1f;

    void OnCollisionEnter(Collision collision)
    {
        bool foundWall = false;
        bool isBigBoom = Random.Range(0f, 1f) < bigRatio;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Transform hit_wall = hit.transform.parent;
            if (hit_wall && hit_wall.tag == "Wall")
            {
                Rigidbody rb = hit_wall.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    if (!foundWall)
                        foundWall = true;

                    if (isBigBoom || Random.Range(0f, 1f) < zapRatio)
                        //rb.gameObject.SetActive(false);
                        Object.Destroy(gameObject);  // TODO: play nice with YugeWall
                    else
                        rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                }
            }
        }

        if (foundWall && big_wall && isBigBoom)
        {
            Instantiate(big_wall, new Vector3(explosionPos.x, 0f, 0f), Quaternion.identity);
            Object.Destroy(gameObject);
            //gameObject.SetActive(false);
        }
        Instantiate(particle, explosionPos, Quaternion.identity);

    }

}
