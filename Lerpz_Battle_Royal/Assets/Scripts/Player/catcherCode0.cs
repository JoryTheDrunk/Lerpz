//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Player)/Catcher Teleport")]

public class catcherCode0 : MonoBehaviour {

public float skyPos = 200;

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			other.transform.position = new Vector3(other.transform.position.x, skyPos, other.transform.position.z);
		}
	}
}
