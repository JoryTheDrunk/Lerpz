using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Third Person Props/Fallout Death")]
public class FalloutDeath : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        // Player fall out!
        if (other.GetComponent<ThirdPersonStatus>())
        {
            other.GetComponent<ThirdPersonStatus>().FalloutDeath();
        }
        // Kill all rigidibodies flying through this area
        // (Props that fell off)
        else if (other.attachedRigidbody)
            Destroy(other.attachedRigidbody.gameObject);
        // Also kill all character controller passing through
        // (enemies)
        else if (other.GetType() == typeof(CharacterController))
            Destroy(other.gameObject);
    }

    // Auto setup the pickup
    public void Reset()
    {
        if (GetComponent<Collider>() == null)
            gameObject.AddComponent<BoxCollider>();
        GetComponent<Collider>().isTrigger = true;
    }
}