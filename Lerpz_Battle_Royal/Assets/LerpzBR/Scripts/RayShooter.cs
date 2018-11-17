using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour {
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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(firePoint.transform.position, firePoint.transform.forward);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 2.0f);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                //ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                ////if (target != null)
                //{
                //    target.ReactToHit();
                //    target.bossHp--;
                //    print("damage to enemy");
                //}
                //else
                //{
                    //StartCoroutine(SphereIndicator(hit.point));
                //}
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