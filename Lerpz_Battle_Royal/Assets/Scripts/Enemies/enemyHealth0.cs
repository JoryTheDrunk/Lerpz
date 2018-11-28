//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Enemy)/Health")]

public class enemyHealth0 : MonoBehaviour {

public int Ehealth = 0;		//the health, for the enemy
public playerHealth stat;	//the plyer health script(required for damage calculations)

	void Start(){
		var temp0 = GameObject.FindWithTag("Player");
		stat = temp0.GetComponent<playerHealth>();
	}

	void Update(){
		if(Ehealth < 1){
			//death
		}
	}
	
		//raycast damage script
	public void TakeDamage(){
		Ehealth -= stat.ouch0;
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Bullet0"){
			Ehealth -= stat.ouch0;
		}
	}
}
