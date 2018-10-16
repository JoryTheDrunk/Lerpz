//Cale Toburen
//Date-5/2/18   
//Project-CSG-128
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera-Control/Smooth Look At")]
public class SmoothLookAt : MonoBehaviour
{


    public Transform target;
    float damping = 6.0f;
    bool smooth = true;

    void LateUpdate()
    {
        if (target)
        {
            if (smooth)
            {
                // Look at and dampen the rotation
                var rotation = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            }
            else
            {
                // Just lookat
                transform.LookAt(target);
            }
        }
    }

    void Start()
    {
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }
}