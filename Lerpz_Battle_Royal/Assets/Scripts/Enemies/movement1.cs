//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Enemy)/Chase the player.")]

public class movement1 : MonoBehaviour {

public float spd0 = 0;		//basic movement speed
public float spd1 = 0;		//sidestep movement speed
public float rot0 = 0;		//rotation speed
private float x = 0;
private float z = 0;
public float range0 = 0;	//how far the enemy checks for the player (forwards, far)
public float range1 = 0;	//how far the enemy checks for a wall (sideways)
public float range2 = 0;	//how far the enemy checks for a wall (downwards)
public float range3 = 0;	//how far the enemy checks for the player (forwards, close)
private Transform target0;	//the player target
private bool enter = false;	//is the player near?
private RaycastHit hit0;	//what the raycast hits
private RaycastHit hit1;	//what the raycast hits

	void Start(){
		var temp0 = GameObject.FindWithTag("Player");
		target0 = temp0.GetComponent<Transform>();
	}

	void Update(){
			//if the player enters the enemy's range of ditection (collider)
		if(enter == true){
				//set the rotation to target the player
			Vector3 turn0 = target0.position - transform.position;
				//set the targeted rotation speed
			float step0 = rot0 * Time.deltaTime;
				//follow the player transform
			Vector3 newDir0 = Vector3.RotateTowards(transform.forward, turn0, step0, 0.0f);
				//rotate towards the player
			transform.rotation = Quaternion.LookRotation(newDir0);
		}
			//send out a raycast to see if the player is in front of the enemy
		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit0, range3) && hit0.transform.tag == "Player"){
				//back away from the player
			transform.Translate(-Vector3.forward * spd0 * Time.deltaTime);
		}	//send out a longer raycast forward
		else if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit0, range0) && hit0.transform.tag == "Player"){
				//move towards the player
			transform.Translate(Vector3.forward * spd0 * Time.deltaTime);
		}
			//raycasts to the right
		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit1, range1) && hit1.transform.tag == "Wall"){
				//sidesteps to the left, away from the wall
			transform.Translate(-Vector3.right * spd1 * Time.deltaTime);
		}	//raycasts to the left (-right)
		if(Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.right), out hit1, range1) && hit1.transform.tag == "Wall"){
				//sidesteps to the right, away from the wall
			transform.Translate(Vector3.right * spd1 * Time.deltaTime);
		}
		if(Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit1, range2) && hit1.transform.tag == "Wall"){
				//sidesteps upwards, away from the floor
			transform.Translate(Vector3.up * spd1 * Time.deltaTime);
		}
			//debug lines, drawn to show the raycasts
		Vector3 forward = transform.TransformDirection(Vector3.forward) * range0;
		Debug.DrawRay(transform.position, forward, Color.red);
		Vector3 forward2 = transform.TransformDirection(Vector3.forward) * range3;
		Debug.DrawRay(transform.position, forward2, Color.green);
		Vector3 left = transform.TransformDirection(-Vector3.right) * range1;
		Debug.DrawRay(transform.position, left, Color.green);
		Vector3 right = transform.TransformDirection(Vector3.right) * range1;
		Debug.DrawRay(transform.position, right, Color.green);
		Vector3 up = transform.TransformDirection(-Vector3.up) * range2;
		Debug.DrawRay(transform.position, up, Color.blue);
	}

	void OnTriggerEnter(Collider other){
			//when the player triggers the collider
		if(other.gameObject.tag == "Player"){
			enter = true;
		}
	}
	void OnTriggerExit(Collider other){
			//when the player de-triggers the collider
		if(other.gameObject.tag == "Player"){
			enter = false;
				//resets the Y axis to default, so enemies idle sitting upright (just looks nicer)
			transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
		}
	}
}
