using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag != "DontKill")
            Destroy(other.gameObject);
    }

}
