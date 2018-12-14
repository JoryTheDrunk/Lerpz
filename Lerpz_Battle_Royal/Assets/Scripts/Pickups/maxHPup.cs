//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Pickups)/Increase Health")]

public class maxHPup : MonoBehaviour {

public int boostHP = 0;		//how much to incease health by
public int healing = 0;		//how much health to restore(if any)
public playerHealth stat;	//the plyer health script
public secretWallDed0 ded;	//the secret gun shack wall thingy
public Rigidbody poof;		//the rigidbody/game object to spawn for particle effect

	void Start(){
		var temp0 = GameObject.FindWithTag("PlayerStats");
		stat = temp0.GetComponent<playerHealth>();
		var temp1 = GameObject.FindWithTag("HUSH CHILD");
		ded = temp1.GetComponent<secretWallDed0>();
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			stat.health += healing;		//restore health, AKA, healing
			stat.maxHP += boostHP;		//increase max health
			ded.Checker0();		//calls the checker event from the ded script
			Rigidbody clone;
				clone = Instantiate(poof, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

}
