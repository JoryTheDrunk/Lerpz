//-JAM, Cale's work, but I duplicated it for left hand
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Player)/Lefty Hook Detect")]

public class leftyHookDetector : MonoBehaviour {

    public GameObject player;
	public OVRPlayerController ovr;
	public bool actuallyShot = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hookable" && actuallyShot == true)
        {
			ovr.isGrappled = true;
			ovr.GravityModifier = 0;
            player.GetComponentInChildren<leftyHookshot0>().hooked = true;
            player.GetComponentInChildren<leftyHookshot0>().hookedObject = other.gameObject;
			print(other);
        }
		else if(other.gameObject.tag != "EnemySphere" || other.gameObject.tag != "Hookable" || other.gameObject.tag != "Bullet1" || other.gameObject.tag != "Bullet2" || other.gameObject.tag != "Bullet3"){
			player.GetComponentInChildren<leftyHookshot0>().ReturnHook();
		}
    }

}
