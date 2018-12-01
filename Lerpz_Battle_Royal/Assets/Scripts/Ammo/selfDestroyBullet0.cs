//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Enemy Ammo)/Self Destroy")]

public class selfDestroyBullet0 : MonoBehaviour {

public int reach = 0;	//how long until the bullet disappears

	void Update(){
		reach -= 1;		//subtracts 1 from reach every frame
		if(reach < 0){	//when reach is less than zero it destorys itself
			Destroy(gameObject);
		}
	}
		//when the bullet enters the player collider, it destroys itself
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			Destroy(gameObject);
		}
	}
}
