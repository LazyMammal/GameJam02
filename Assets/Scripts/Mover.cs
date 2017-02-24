using UnityEngine;

public class Mover : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public bool moving = false;

    void Update()
    {
        if (moving)
            transform.position += velocity * Time.deltaTime;
    }
}
