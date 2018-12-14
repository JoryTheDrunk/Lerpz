//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Enemy)/Health")]

public class enemyHealth0 : MonoBehaviour {

public int Ehealth = 0;		//the health, for the enemy
private int Ehealth2;		//used for a calculation
public playerHealth stat;	//the plyer health script(required for damage calculations)
public enemyCounter EC;		//the enemy counter script
public Rigidbody poof;		//the rigidbody/game object to spawn for particle effect
public Animation anim;		//animation
public AudioClip ouch;		//the hurt sound effect
private AudioSource ply;	//the audio source player
private ParticleSystem dying;
private bool halfDead = false;

	void Start(){
			//automatically finds the player object, by tag
		var temp0 = GameObject.FindWithTag("PlayerStats");
			//automatically finds the player health script
		stat = temp0.GetComponent<playerHealth>();
			//automatically finds the enemy counter script
		EC = temp0.GetComponent<enemyCounter>();
		ply = GetComponent<AudioSource>();
		dying = GetComponent<ParticleSystem>();
		Ehealth2 = Ehealth/2;
	}

	void Update(){
		if(Ehealth < 1){
			EC.count -= 1;
			Rigidbody clone;
				clone = Instantiate(poof, transform.position, Quaternion.identity);
				Debug.Log("Disabled!");
			Destroy(gameObject);
		}
		if(Ehealth < Ehealth2 && halfDead == false){
			dying.Play();
			halfDead = true;
		}
	}
	
	
		//raycast damage script, called upon by the rayshoot script
	public void TakeDamage(){
		Ehealth -= stat.ouch0;
		anim.Play("gothit");	//plays the hurt animation
		anim.PlayQueued("idle", QueueMode.CompleteOthers);	//then goes back to idle afterwards
		ply.PlayOneShot(ouch);	//plays the hurt sound effect
	}

	void OnTriggerEnter(Collider other){
			//take damage via colliders & tags, mostly for debugging/testing 
		if(other.gameObject.tag == "Bullet0"){
			Ehealth -= stat.ouch0;
		}
	}
}
