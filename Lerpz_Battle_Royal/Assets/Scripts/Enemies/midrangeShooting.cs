//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Enemy)/Midrange Shooting")]

public class midrangeShooting : MonoBehaviour {

public Rigidbody ammo;		//bullet prefab
public float range0 = 0;	//range to shoot (forwards)
public float range1 = 0;	//range for animation detection (forwards)
private RaycastHit hit0;	//what the raycast hits
public float spd0 = 0;		//speed of the projectile
private int delay0 = 0;		//the delay timer
public int setDelay0 = 0;	//set the delay timer
public bool isMelee = false;//checks if a melee enemy or not 
public Animation anim;


	void Start(){	//set the delay timer to the default
		delay0 = setDelay0;
	}

	void FixedUpdate(){
		delay0 -= 1;
			//if the player is in the raycast, reduce the delay timer
		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit0, range0) && hit0.transform.tag == "Player"){
			if(delay0 < 0){	//when delay is less than zero, clone/shoot the projectile (bullet), and then reset delay to the default
				Rigidbody clone0;
					clone0 = Instantiate(ammo, transform.position, transform.rotation);
					clone0.velocity = transform.TransformDirection(Vector3.forward * spd0);
				delay0 = setDelay0;
				if(isMelee == false){
					anim.Play("punch");
					anim.PlayQueued("idle", QueueMode.CompleteOthers);
				}
			}
			
		}
		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit0, range1) && hit0.transform.tag == "Player"){
			if(isMelee == true){
				anim.Play("attackrun");
			}
		}
		else{
			if(isMelee == true){
				anim.Play("idle");
			}
		}

		Vector3 forward1 = transform.TransformDirection(Vector3.forward) * range1;
		Debug.DrawRay(transform.position, forward1, Color.blue);
		Vector3 forward = transform.TransformDirection(Vector3.forward) * range0;
		Debug.DrawRay(transform.position, forward, Color.red);
	}
}
