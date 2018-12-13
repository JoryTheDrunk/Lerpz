//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[AddComponentMenu("(Player)/Enemy Counter")]

public class enemyCounter : MonoBehaviour {

public int count;
public Text enemyLabel;

	void Start(){
		var foundObjects = FindObjectsOfType<spawn0>();	//finds all the gameobjects with the snowcone flavor script
		count = foundObjects.Length;					//adds the number of found objects to the count number
		Debug.Log("Enemies left: " + count);			//debugs out the counted snowcones 
		enemyLabel.text = "enemies left: " + count.ToString();
	}
}
