//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Pickups)/Supah Gun!")]

public class supergun0 : MonoBehaviour {

public playerHealth stat;	//the plyer health script(required for damage calculations)
	//below are the game objects for the old and new gun models
	//used to replace the gun models, for the upgraded gun
public GameObject oldGunL;
public GameObject oldGunR;
public GameObject newGunL;
public GameObject newGunR;

	void Start(){
		var temp0 = GameObject.FindWithTag("Player");
		stat = temp0.GetComponent<playerHealth>();
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			stat.ouch0 = stat.ouch0 * 2;	//multiplies the damage
			SwapModels();	//calls the model swapping event
			//play sound?
			Destroy(gameObject);
		}
	}
	
	void SwapModels(){	//simple event to swap the gun model game objects, called upon during on trigger enter
		oldGunL.SetActive(false);
		oldGunR.SetActive(false);
		newGunL.SetActive(true);
		newGunR.SetActive(true);
	}
}
