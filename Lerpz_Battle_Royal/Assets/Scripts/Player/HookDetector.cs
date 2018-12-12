using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour {

    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hookable")
        {
            player.GetComponentInChildren<GrapplingHook>().hooked = true;
            player.GetComponentInChildren<GrapplingHook>().hookedObject = other.gameObject;
			print(other);
        }
		else{
			player.GetComponentInChildren<GrapplingHook>().ReturnHook();
		}
    }

}
