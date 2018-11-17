//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Enemy)/Midrange Shooting")]

public class midrangeShooting : MonoBehaviour {

public Rigidbody ammo;		//bullet prefab
public float range0 = 0;	//range to shoot (forwards)
private RaycastHit hit0;	//what the raycast hits
public float spd0 = 0;		//speed of the projectile
private int delay0 = 0;		//the delay timer
public int setDelay0 = 0;	//set the delay timer

	void Start(){	//set the delay timer to the default
		delay0 = setDelay0;
	}

	void Update(){
			//if the player is in the raycast, reduce the delay timer
		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit0, range0) && hit0.transform.tag == "Player"){
			delay0 -= 1;
		}
		if(delay0 < 1){	//when delay is less than zero, clone/shoot the projectile (bullet), and then reset delay to the default
			Rigidbody clone0;
				clone0 = Instantiate(ammo, transform.position, transform.rotation);
				clone0.velocity = transform.TransformDirection(Vector3.forward * spd0);
			delay0 = setDelay0;
		}
		
		Vector3 forward = transform.TransformDirection(Vector3.forward) * range0;
		Debug.DrawRay(transform.position, forward, Color.red);
	}

}
