using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    // Update is called once per frame
    public float speed = 10.0F;
    void Update()
    {
        float translation = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        transform.Translate(translation, 0, 0);

        if (Input.GetButtonDown("Cancel"))
        {
            if (Time.timeScale > 0)
            {
                // pause
                Time.timeScale = 0;

                // show title screen
                SequenceOfPlay.singleton.NextState(2);
            }
            else SequenceOfPlay.singleton.NextState();
        }
    }
}