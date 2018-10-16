//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Enemy)/Spawner")]

public class spawn0 : MonoBehaviour {

public Rigidbody enemy0;

	void Start(){	
		Rigidbody clone0;
			clone0 = Instantiate(enemy0, transform.position, transform.rotation);
	}

}
