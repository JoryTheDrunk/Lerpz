//Cale Toburen
//11/30/17
//CSG 114

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float coinRotation = 3f;
    public float speed;
    public float coinMax;
    public float coinMin;
    bool moving;
    bool atMax;


	// Use this for initialization
	void Start ()
    {
        moving = false;
        StartCoroutine(Moving());
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(0, coinRotation, 0);
	}
    IEnumerator Moving()
    {
        moving = false;
        while (moving == false)
        {
            transform.position = transform.position + new Vector3(0, speed * Time.deltaTime, 0);
            //print(Time.deltaTime);
            if (transform.position.y >= coinMax)
            {
                moving = true;
            }
            yield return null;
        }
        while (moving == true)
        {
            transform.position = transform.position + new Vector3(0, -speed * Time.deltaTime, 0);
            if (transform.position.y <= coinMin)
            {
                moving = false;
            }
            yield return null;
        }
        StartCoroutine(Moving());
        yield return null;

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
