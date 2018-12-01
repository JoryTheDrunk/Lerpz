//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Pickups)/Cute Spin")]

public class cuteSpin0 : MonoBehaviour {

	void Update(){		//simple rotation script, rotates around the world and not the local axis,
		transform.Rotate (new Vector3 (0, 30, 0) * Time.deltaTime, Space.World);
	}
}
