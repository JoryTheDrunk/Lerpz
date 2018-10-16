﻿//Mitchell Hewitt
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    /*
        EnemyRespawn.cs

        This script checks if the player is in range. If so, it instantiates the enemy prefab specified. When the player moves out of range, the prefab is automatically destroyed.

        This prevents repeated calls to the enemy's AI scripts when the AI is nowhere near the player, this improving performance.

    */


    public float spawnRange = 0.0f;   // the distance within which the enemy should be active.
    public string gizmoName;		// the type of the object. (See OnDrawGizmos() for more.)
    public GameObject enemyPrefab;	// link to the Prefab we'll be instantiating / destroying on demand.

    // Cache variables, used to speed up the code.
    private Transform player;
    private GameObject currentEnemy;
    private bool wasOutside = true;

    // Called on Scene startup. Cache a link to the Player object.
    // (Uses the tagging system to locate him.)
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Called at least once every game cycle. This is where the fun stuff happens.
    void Update()
    {
        // how far away is the player?
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // is he in range?
        if (distanceToPlayer < spawnRange)
        {
            // in range. Do we have an active enemy and the player has just come into range, instantiate the prefab at our location. 
            if (!currentEnemy && wasOutside)
                currentEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);

            // player is now inside our range, so set the flag to prevent repeatedly instantiating the prefab.
            wasOutside = false;
        }
        // player is out of range.
        else
        {
            // is player leaving the sphere of influence while our prefab is active?
            if (currentEnemy && !wasOutside)
                Destroy(currentEnemy);  // kill the prefab...

            // ...and set our flag so we re-instantiate the prefab if the player returns.
            wasOutside = true;
        }
    }

    // Called by the Unity Editor GUI every update cycle.
    // Draws an icon at our transform's location. The icon's filename is derived from the "type" variable, which allows this script to be used for any enemy.
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 1);

        // See the help docs for info on where the icon needs to be stored for this function to work.
        Gizmos.DrawIcon(transform.position, gizmoName + ".psd");
    }

    // Called by the Unity Editor GUI every update cycle, but only when the object is selected.
    // Draws a sphere showing spawnRange's setting visually.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 1);
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }
}
