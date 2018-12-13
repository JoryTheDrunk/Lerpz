//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Player)/Camera Rotation Y (ourside of VR)")]

public class cameraRotation : MonoBehaviour {

public float rot0 = 0;		//rotation speed
public float max = 0;		//positive
public float min = 0;		//negative
private float y;			//place holder for the rotation
private bool useMouse = false;

	void FixedUpdate(){
		if(useMouse == true){
				//rotate on the mouse axis
			y += Input.GetAxis("Mouse Y") * rot0;
				//clamp down on the min/max
			y = Mathf.Clamp (y, min, max);
				//apply rotation to vector, relative to the parent transform 
			transform.localEulerAngles = new Vector3(y, 0, 0);
		}
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.RightAlt)){
			useMouse = !useMouse;
			transform.rotation = Quaternion.identity;
		}
	}
}
