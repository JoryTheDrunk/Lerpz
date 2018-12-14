//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Enemy)/Range to detect player")]

public class playerDetection0 : MonoBehaviour {

public movement1 mover;
public Transform movT;
public Transform player;

	void Start(){
		var temp = GameObject.FindWithTag("Player");
		player = temp.gameObject.GetComponent<Transform>();
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			mover.enter = true;
		}
		if(player.transform.position.y > 199){
			Resetti();
		}
	}
	
	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "Player"){
			mover.enter = false;
				//resets the Y axis to default, so enemies idle sitting upright (just looks nicer)
			movT.rotation = Quaternion.Euler(0, movT.rotation.eulerAngles.y, 0);
		}
	}
	
	void Resetti(){
		mover.enter = false;
				//resets the Y axis to default, so enemies idle sitting upright (just looks nicer)
		movT.rotation = Quaternion.Euler(0, movT.rotation.eulerAngles.y, 0);
	}
}
