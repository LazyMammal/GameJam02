using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Wall")
            Destroy(other.gameObject);
        else
            other.gameObject.SetActive(false);
    }

}
