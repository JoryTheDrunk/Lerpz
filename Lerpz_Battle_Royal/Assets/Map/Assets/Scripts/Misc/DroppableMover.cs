//Jake Poshepny

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppableMover : MonoBehaviour
{
    public float gravity = 10.0f;
    public LayerMask collisionMask;

    private Vector3 velocity = Vector3.zero;
    private Vector3 position;

    public void Bounce(Vector3 force)
    {
        position = transform.position;
        velocity = force;
    }
	
	// Update is called once per frame
	void Update ()
    {
        velocity.y -= gravity * Time.deltaTime;
        Vector3 moveThisFrame = velocity * Time.deltaTime;
        float distanceThisFrame = moveThisFrame.magnitude;

        if (Physics.Raycast(position, moveThisFrame, distanceThisFrame, collisionMask))
        {
            enabled = false;
        }
        else
        {
            position += moveThisFrame;
            transform.position = new Vector3(position.x, position.y + 0.75f, position.z);
        }
	}
}
