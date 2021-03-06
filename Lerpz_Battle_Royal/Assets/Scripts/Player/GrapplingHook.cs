﻿//Cale Toburen
//Ethen Quandt
//11/6/18
//CSG-120

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Player)/Grapple")]	//easy locate, added by Joey

public class GrapplingHook : MonoBehaviour {

    public GameObject hook;
    public GameObject hookHolder;
    public GameObject player;

    public float hookTravelSpeed;
    public float playerTravelSpeed;

    public bool fired;
    public bool hooked;
    public GameObject hookedObject;
	
    public float maxDistance;
    private float currentDistance;

    private bool grounded;
	
	public OVRPlayerController ovr;
	
	//public bool amLeftHand = false;	//-JAM
	public HookDetector hd;			//-JAM
	
	
	// Update is called once per frame
	void Update () {
        //change to OVR controls 
        //Firing the hook
        if (Input.GetMouseButtonDown(1) && !fired)
        {
            fired = true;
			hd.actuallyShot = true;
			print("Fire button");
        }
		if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && !fired)
		{
			fired = true;
			hd.actuallyShot = true;
			print("Fire button");
		}
		if(OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)){
			ReturnHook();
		}

        if (fired && !hooked)
        {
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(player.transform.position, hook.transform.position);
             
            if(currentDistance >= maxDistance)
            {
                ReturnHook();
				print("ReturnHook");
            }
        }

        if (fired)
        {
            LineRenderer rope = hook.GetComponent<LineRenderer>();
            rope.SetVertexCount(2);
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
			print("bool fire");
        }

        if (hooked && fired)
        {
			print("Hooked & fired");
            //hook.transform.parent = hookedObject.transform;
			//ovr.isGrappled = true;
			//ovr.GravityModifier = 0;
            player.transform.position = Vector3.MoveTowards(player.transform.position, hookedObject.transform.position, Time.deltaTime * playerTravelSpeed);
            float distanceToHook = Vector3.Distance(transform.position, hookedObject.transform.position);

            //this.GetComponent<Rigidbody>().useGravity = false;
			

            if(distanceToHook < 2)
            {
                if (!grounded)
                {
                    //this.transform.Translate(Vector3.forward * Time.deltaTime * 13f);
                    //this.transform.Translate(Vector3.up * Time.deltaTime * 18f);
                }

                ReturnHook();
            }
           // else
          //  {
           //     hook.transform.position = hookHolder.transform.position;
                //this.GetComponent<Rigidbody>().useGravity = true;
				//ovr.GravityModifier = 1;
           // }
        }

	}


    public void ReturnHook()
    {
		print("Returned");
		//hook.transform.parent = hookHolder.transform;
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;
		ovr.GravityModifier = 1;
        LineRenderer rope = hook.GetComponent<LineRenderer>();
        rope.SetVertexCount(0);
		ovr.isGrappled = false;
		hd.actuallyShot = false;
    }
	void OnDisable(){
		ReturnHook();
	}
}
