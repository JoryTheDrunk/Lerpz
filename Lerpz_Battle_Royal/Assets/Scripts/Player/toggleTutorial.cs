using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("(Player)/Toggle Tutorial")]	//easy locate, added by Joey

public class toggleTutorial : MonoBehaviour {
	public bool IsTutorial;
	public GameObject tut;
	// Use this for initialization


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("right shift") && IsTutorial == true)
		{
			IsTutorial = false;
		}
		else if(Input.GetKeyDown("right shift") && IsTutorial == false)
		{
			IsTutorial = true;
		}

		if (IsTutorial == true)
		{
			tut.SetActive(true);
		}
		if (IsTutorial == false)
		{
			tut.SetActive(false);
		}
	}
}

