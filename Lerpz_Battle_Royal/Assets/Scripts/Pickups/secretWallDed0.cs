//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Pickups)/Remove Secret Wall")]

public class secretWallDed0 : MonoBehaviour {

public int five = 5;
public GameObject wallz;

	public void Checker0(){
		if(five == 1){
			Destroy(wallz);
			five -= 1;
		}
		else{
			five -= 1;
		}
	}
}
