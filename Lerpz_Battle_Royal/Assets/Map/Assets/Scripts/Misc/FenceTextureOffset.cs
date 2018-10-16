//Jake Poshepny

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceTextureOffset : MonoBehaviour
{
    public float scrollSpeed = .25f;

    private void FixedUpdate()
    {
        float offset = Time.time * scrollSpeed;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset, offset);
    }
}
