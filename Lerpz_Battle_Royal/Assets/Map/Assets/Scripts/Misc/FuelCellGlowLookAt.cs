//Jake Poshepny

// fuelCellGlowLookAt: Forces the object to always face the camera.
// (Used for the 'glowing halo' effect behind the collectable items.)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCellGlowLookAt : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(Camera.main.transform);
	}

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }
}
