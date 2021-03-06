﻿using System.Collections;
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
		else if(other.gameObject.tag != "EnemySphere" || other.gameObject.tag != "Hookable" || other.gameObject.tag != "Bullet1" || other.gameObject.tag != "Bullet2" || other.gameObject.tag != "Bullet3"){
			player.GetComponentInChildren<GrapplingHook>().ReturnHook();
		}
    }

}
