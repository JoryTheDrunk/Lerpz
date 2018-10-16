//Jake Poshepny

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[AddComponentMenu("Third Person Props/Pickup")]

public class Pickup : MonoBehaviour
{
    public enum PickupType { Health = 0, FuelCell = 1 }
    public PickupType pickupType = PickupType.FuelCell;
    public int amount = 1;
    public AudioClip sound;
    public float soundVolume = 2.0f;

    private bool used = false;
    private DroppableMover mover;

    // Use this for initialization
    void Start ()
    {
        // do we exist in the level or are we instantiated by an enemy dying?
        mover = GetComponent<DroppableMover>();
    }

    public bool ApplyPickup(ThirdPersonStatus playerStatus)
    {
        
        // A switch...case statement may seem overkill for this, but it makes adding new pickup types trivial.
        switch (pickupType)
        {
            case PickupType.Health:
                playerStatus.AddHealth(amount);
                break;

            case PickupType.FuelCell:

                playerStatus.FoundItem(amount);
            
                break;
        }

        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (mover && mover.enabled)
            return;

        ThirdPersonStatus playerStatus = other.GetComponent<ThirdPersonStatus>();

        //* Make sure we are running into a player
        //* prevent picking up the trigger twice, because destruction
        //  might be delayed until the animation has finished
        if (used || playerStatus == null)
            return;

        if (!ApplyPickup(playerStatus))
            return;

        used = true;
        // Play Sound
        if (sound)
            AudioSource.PlayClipAtPoint(sound, transform.position, soundVolume);

        // If there is an animation attached.
        // Play it.
        if (GetComponent<Animation>() && GetComponent<Animation>().clip)
        {
            GetComponent<Animation>().Play();
            Destroy(gameObject, GetComponent<Animation>().clip.length);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Reset()
    {
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }

        GetComponent<Collider>().isTrigger = true;
    }
}
