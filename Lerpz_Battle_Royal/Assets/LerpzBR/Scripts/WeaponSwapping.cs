using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapping : MonoBehaviour {

	private GameObject[] weapons;

	public int currentWeapon = 0;

	bool canSwitch = true;

    void Awake()
    {
        weapons = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            weapons[i] = gameObject.transform.GetChild(i).gameObject;
        }

    }
    // Use this for initialization
    void Start()
    {
        

    }

	// Update is called once per frame
	void Update () {
		SwitchWeapon ();
	}

	void SwitchWeapon() {
		if (Input.GetKeyDown (KeyCode.Tab) && canSwitch == true) {
			StartCoroutine (CanSwitchWeapons ());
			currentWeapon++;

			if (currentWeapon >= weapons.Length)
				currentWeapon = 0;

			Reset ();

			weapons [currentWeapon].SetActive (true);

		
		}
		if (OVRInput.GetDown(OVRInput.Button.One) && canSwitch == true) {
			StartCoroutine (CanSwitchWeapons ());
			currentWeapon++;

			if (currentWeapon >= weapons.Length)
				currentWeapon = 0;

			Reset ();

			weapons [currentWeapon].SetActive (true);

		
		}
	}
	void Reset(){
		for (int i = 0; i < weapons.Length; i++) {
			weapons [i].SetActive (false);
		}
	}

	IEnumerator CanSwitchWeapons(){
		canSwitch = false;
		yield return new WaitForSeconds (.1f);
		canSwitch = true;
	}
}