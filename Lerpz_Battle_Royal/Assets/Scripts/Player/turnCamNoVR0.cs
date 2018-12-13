//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Player)/Camera Rotation X (ourside of VR)")]

public class turnCamNoVR0 : MonoBehaviour {

public float rot0;
private bool useMouse = false;

	void FixedUpdate(){
		if(useMouse == true){
				//horizontal axis, or the side-to-side left/right mouse turning
			float Haxs = Input.GetAxis("Mouse X") * rot0;
				//use the mouse to actually turn the camera
			transform.Rotate(0, Haxs, 0);
		}
	}
	void Update(){
		if(Input.GetKeyDown(KeyCode.RightAlt)){
			useMouse = !useMouse;
			transform.rotation = Quaternion.identity;
		}
	}
}
