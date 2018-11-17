//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Enemy)/Health")]

public class enemyHealth0 : MonoBehaviour {

public int health = 0;	//the health
public int ouch = 0;	//damage from tag Bullet0

	void Update(){
		if(health < 1){
			//death
		}
	}
	
		//raycast damage script
	public void TakeDamage(){
		health -= ouch;
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Bullet0"){
			health -= ouch;
		}
	}
}
