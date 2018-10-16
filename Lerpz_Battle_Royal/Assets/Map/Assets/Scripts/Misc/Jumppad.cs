//Cale Toburen
//Date-5-2-18
//Project-CSG-128
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Third Person Props/Jump pad")]
public class Jumppad : MonoBehaviour
{

    float jumpHeight = 5.0f;

    void OnTriggerEnter(Collider col)
    {
        ThirdPersonController controller = col.GetComponent<ThirdPersonController>();
        if (controller != null)
        {
            if (GetComponent<AudioSource>())
            {
                GetComponent<AudioSource>().Play();
            }

            controller.SuperJump(jumpHeight);
        }

    }

    // Auto setup the script and associated trigger.
    void Reset()
    {
        if (GetComponent<Collider>() == null)
            gameObject.AddComponent(typeof(BoxCollider));
        GetComponent<Collider>().isTrigger = true;
    }
}