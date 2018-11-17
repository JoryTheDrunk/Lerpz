//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("(Player)/Health")]

public class playerHealth : MonoBehaviour {

public int health = 0;	//the health
public int ouch1 = 0;	//damage from tag Bullet1
public int ouch2 = 0;	//damage from tag Bullet2
public int ouch3 = 0;	//damage from tag Bullet3
public int ammo0 = 0;	//the ammo for the gun
public int grap0 = 0;	//the grapplehook cool down
public Slider pHP;		//the slider used as the player health bar

		//playerHP.text = "Health: " + health.ToString();
		

	void Update(){
			//below is a script that makes the value of the GUI slider equal to the player health
		pHP.value = health;
		
		if(health < 1){
			//death
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Bullet1"){
			health -= ouch1;
		}
		if(other.gameObject.tag == "Bullet2"){
			health -= ouch2;
		}
		if(other.gameObject.tag == "Bullet3"){
			health -= ouch3;
		}
	}
}
