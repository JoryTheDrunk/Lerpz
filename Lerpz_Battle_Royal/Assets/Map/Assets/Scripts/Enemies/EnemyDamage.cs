using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int hitPoints = 3;

    public Transform explosionPrefab;
    public Transform deadModelPrefab;
    public DroppableMover healthPrefab;
    public DroppableMover fuelPrefab;
    float dropMin = 0f;
    float dropMax = 0f;

    // sound clips
    public AudioClip struckSound;

    private bool dead = false;

    void Start()
    {

    }

    void Update()
    {

    }

    public void ApplyDamage(int damage)
    {

        // we've been hit, so play the 'struck' sound. This should be a metallic 'clang'.
        if (GetComponent<AudioSource>() && struckSound)
            GetComponent<AudioSource>().PlayOneShot(struckSound);

        if (hitPoints <= 0)
            return;

        hitPoints -= damage;
        if (!dead && hitPoints <= 0)
        {
            Die();
            dead = true;
        }
   
    }

    public void Die()
    {
       
        // Kill ourselves
        Destroy(gameObject);

        // Instantiate replacement dead character model
        Transform deadModel = Instantiate(deadModelPrefab, transform.position, transform.rotation);
        CopyTransformsRecurse(transform, deadModel);

        // create an effect to let the player know he beat the enemy
        Transform effect = Instantiate(explosionPrefab, transform.position, transform.rotation);
        effect.parent = deadModel;

        // fall away from the player, and spin like a top
        Rigidbody deadModelRigidbody = deadModel.GetComponent<Rigidbody>();
        Vector3 relativePlayerPosition = transform.InverseTransformPoint(Camera.main.transform.position);
        deadModelRigidbody.AddTorque(Vector3.up * 7);
        if (relativePlayerPosition.z > 0)
            deadModelRigidbody.AddForceAtPosition(-transform.forward * 2, transform.position + (transform.up * 5), ForceMode.Impulse);
        else
            deadModelRigidbody.AddForceAtPosition(transform.forward * 2, transform.position + (transform.up * 2), ForceMode.Impulse);

        // drop a random number of pickups in a random fashion
        var toDrop = Random.Range(dropMin, dropMax + 1);    // how many shall we drop?
        for (var i = 0; i < toDrop; i++)
        {
            Vector3 direction = Random.onUnitSphere;    // pick a random direction to throw the pickup.
            if (direction.y < 0)
                direction.y = -direction.y; // make sure the pickup isn't thrown downwards

            // initial position of the pickup
            Vector3 dropPosition = transform.TransformPoint(new Vector3(0, 1.5f, 0) + (direction / 2));

            DroppableMover dropped;

            // select a pickup type at random
            if (Random.value > 0.5)
                dropped = Instantiate(healthPrefab, dropPosition, Quaternion.identity);
            else
                dropped = Instantiate(fuelPrefab, dropPosition, Quaternion.identity);

            // set the pickup in motion
            dropped.Bounce(direction * 4f * (Random.value + 0.2f));
        }
    }

    public static void CopyTransformsRecurse(Transform src, Transform dst)
    {
        dst.position = src.position;
        dst.rotation = src.rotation;

        foreach (Transform child in dst)
        {
            // Match the transform with the same name
            var curSrc = src.Find(child.name);
            if (curSrc)
                CopyTransformsRecurse(curSrc, child);
        }
    }
}