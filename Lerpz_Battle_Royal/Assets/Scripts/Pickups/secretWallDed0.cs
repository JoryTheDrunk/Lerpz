//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Pickups)/Remove Secret Wall")]

public class secretWallDed0 : MonoBehaviour {

public int five = 5;		//the int used for enemy count
public GameObject wallz;	//the wall to destroy
		//this script just destroys a wall upon collecting 5 pickups 
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
