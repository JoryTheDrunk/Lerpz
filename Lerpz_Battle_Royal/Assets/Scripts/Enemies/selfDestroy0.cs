//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Enemy)/Self Destroy Bleedout")]

public class selfDestroy0 : MonoBehaviour {

	void Start(){	//simple self destroy script, after 20 seconds of existing
		Destroy(gameObject, 20);
	}
}
