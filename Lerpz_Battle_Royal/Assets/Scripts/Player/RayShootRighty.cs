using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Player)/Right Hand Fire")]

public class RayShootRighty : MonoBehaviour {
 private Camera _camera;
    [SerializeField]
    private GameObject firePoint;

	// Use this for initialization
	void Start () {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    //void OnGUI()
    //{
    //    int size = 12;
    //    float posX = _camera.pixelWidth / 2 - size / 4;
    //    float posY = _camera.pixelHeight / 2 - size / 2;
    //    GUI.Label(new Rect(posX, posY, size, size), "*");
    //}

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            Ray ray = new Ray(firePoint.transform.position, firePoint.transform.forward);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 2.0f);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Enemy")
            {
				hit.transform.GetComponent<enemyHealth0>().TakeDamage();
				print("ouch");
            }
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
