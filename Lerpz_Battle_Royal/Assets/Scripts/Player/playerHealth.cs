//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("(Player)/Health")]

public class playerHealth : MonoBehaviour {

public int health = 0;	//the health
public int maxHP = 100;	//maximum health
public int ouch0 = 0;	//damage from tag Bullet0, player hurts enemy
public int ouch1 = 0;	//damage from tag Bullet1, enemy hurts player
public int ouch2 = 0;	//damage from tag Bullet2, enemy hurts player
public int ouch3 = 0;	//damage from tag Bullet3, enemy hurts player
public int ammo0 = 0;	//the ammo for the gun
public int grap0 = 0;	//the grapplehook cool down
public Slider pHP;		//the slider used as the player health bar

		//playerHP.text = "Health: " + health.ToString();
		
	void Start(){
		health = maxHP;
	}

	void Update(){
			//below is a script that makes the value of the GUI slider equal to the player health
		pHP.value = health;
		
		if(health < 1){
			//death
		}
		if(health > maxHP){
			health = maxHP;
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
