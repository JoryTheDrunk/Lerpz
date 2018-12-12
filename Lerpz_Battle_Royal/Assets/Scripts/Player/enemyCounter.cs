//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Player)/Enemy Counter")]

public class enemyCounter : MonoBehaviour {

public int count;

	void Start(){
		var foundObjects = FindObjectsOfType<spawn0>();	//finds all the gameobjects with the snowcone flavor script
		count = foundObjects.Length;					//adds the number of found objects to the count number
		Debug.Log("Enemies left: " + count);			//debugs out the counted snowcones 
	}
}
