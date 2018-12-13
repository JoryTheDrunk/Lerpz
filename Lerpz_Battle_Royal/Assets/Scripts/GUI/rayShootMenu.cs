//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Player)/Ray Shoot Menu")]

public class rayShootMenu : MonoBehaviour {

	private Camera _camera;
    [SerializeField]
    private GameObject firePoint;
	[SerializeField]
	private LineRenderer pewpew;	//line renderer /-JAM
	private int beam = 0;			//timer to disable line renderer /-JAM

	void Start () {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

		pewpew.enabled = false;	//disable the line renderer right away /-JAM
    }
	
	 void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger) || Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
			pewpew.enabled = true;	//enables the line renderer, giving off a shooting effect /-JAM
			beam = 10;	//sets the timer to disable the line renderer /-JAM
            Ray ray = new Ray(firePoint.transform.position, firePoint.transform.forward);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 2.0f);
			if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Button")
			{
				hit.transform.GetComponent<loadLevel0>().LoadClick();
			}
        }
			//if the beam is greater than zero, subtract one from beam /-JAM
		if(beam > 0){
			beam -= 1;
		}
			//if beam is less than one, and the line renderer is enabled, then disable the line renderer /-JAM
		if(beam < 1 && pewpew.enabled == true){
			pewpew.enabled = false; 
		}
    }

    IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }
}
