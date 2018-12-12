//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Enemy)/Range to detect player")]

public class playerDetection0 : MonoBehaviour {

public movement1 mover;
public Transform movT;

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			mover.enter = true;
		}
	}
	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "Player"){
			mover.enter = true;
				//resets the Y axis to default, so enemies idle sitting upright (just looks nicer)
			movT.rotation = Quaternion.Euler(0, movT.rotation.eulerAngles.y, 0);
		}
	}
}
