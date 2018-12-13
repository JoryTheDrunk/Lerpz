using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour {

    public GameObject player;
	public OVRPlayerController ovr;
	public bool actuallyShot = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hookable" && actuallyShot == true)
        {
			ovr.isGrappled = true;
			ovr.GravityModifier = 0;
            player.GetComponentInChildren<GrapplingHook>().hooked = true;
            player.GetComponentInChildren<GrapplingHook>().hookedObject = other.gameObject;
			print(other);
        }
		else{
			player.GetComponentInChildren<GrapplingHook>().ReturnHook();
		}
    }

}
