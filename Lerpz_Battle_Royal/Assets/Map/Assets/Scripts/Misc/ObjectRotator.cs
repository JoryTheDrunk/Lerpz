//Jake Poshepny

// objectRotater: Rotates the object to which it is attached.
// Useful for collectable items, etc.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 45 * Time.deltaTime, 0);
    }

    //OnBecameVisible is called when the renderer became visible by any camera.
    private void OnBecameVisible()
    {
        enabled = true;
    }

    //OnBecameInvisible is called when the renderer is no longer visible by any camera.
    private void OnBecameInvisible()
    {
        enabled = false;
    }
}