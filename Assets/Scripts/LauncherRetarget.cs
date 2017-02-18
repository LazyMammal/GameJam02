using System.Collections.Generic;
using UnityEngine;

public class LauncherRetarget : MonoBehaviour
{

    public float cooldown = 10f;        // minimum time between retargets
    public bool active = true;          // can retarget or not
    public float angular_speed = 1f;    // rotation speed limit

    private float cooldown_finished = 0f;   // time when current cooldown completes
    private Vector3 targetPos = Vector3.down;

    void Update()
    {
        if (Time.time > cooldown_finished)
        {
            cooldown_finished = Time.time + cooldown * (Random.value + 1f);
            Retarget();
        }

        if (targetPos != Vector3.down)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos - transform.position), Time.deltaTime * angular_speed);
        }
    }

    void Retarget()
    {
        List<GameObject> flags = FlagUtils.FindAllEnemyFlags(transform.parent);

        if (flags.Count > 0)
        {
            // get random index in filtered array
            int index = Random.Range(0, flags.Count);
            targetPos = flags[index].transform.position;
            InfoBox.PrependLine("Retarget successful: " + index + " / " + flags.Count);
        }

    }
}
