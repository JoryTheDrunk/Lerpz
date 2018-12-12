//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Enemy)/Spawner")]

public class spawn0 : MonoBehaviour {

private int rand = 0;
public Transform[] enemy0 = new Transform[1];

	void Start(){
		rand = Random.Range(0, enemy0.Length);
		Instantiate(enemy0[rand], transform.position, transform.rotation);
	}
}
