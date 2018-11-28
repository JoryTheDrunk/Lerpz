//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Pickups)/Increase Damage")]

public class increaseDamage0 : MonoBehaviour {

public int boostHurt = 0;	//how much to incease the damage by
public playerHealth stat;	//the plyer health script(required for damage calculations)
public secretWallDed0 ded;	//the secret gun shack wall thingy

	void Start(){
		var temp0 = GameObject.FindWithTag("Player");
		stat = temp0.GetComponent<playerHealth>();
		var temp1 = GameObject.FindWithTag("HUSH CHILD");
		ded = temp1.GetComponent<secretWallDed0>();
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			stat.ouch0 += boostHurt;	//increase the damage
			ded.Checker0();		//calls the checker event from the ded script
			Destroy(gameObject);
		}
	}
}
