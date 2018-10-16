//Cale T
//CSG 128
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdPersonStatus : MonoBehaviour
{
    // ThirdPersonStatus: Handles the player's state machine.

    // Keeps track of inventory, health, lives, etc.
    public int health = 6;
    public int maxHealth = 6;
    public int lives = 4;

    // sound effects.
    AudioClip struckSound;
    AudioClip deathSound;

    public LevelStatus levelStateMachine;		// link to script that handles the level-complete sequence.

    public int remainingItems;	// total number to pick up on this level. Grabbed from LevelStatus.
    private void Update()
    {
        if(health <= 0)
        {
            Die();
        }
        if (lives <= -1)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    void Awake()
    {

        levelStateMachine = FindObjectOfType<LevelStatus>();
        if (!levelStateMachine)
            Debug.Log("No link to Level Status");

        remainingItems = levelStateMachine.itemsNeeded;
    }

    public int GetRemainingItems()
    {
        return remainingItems;
    }

    public void ApplyDamage(int damage)
    {
      
        if (struckSound)
            AudioSource.PlayClipAtPoint(struckSound, transform.position);   // play the 'player was struck' sound.
    
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    
      
    }

    public void AddLife(int powerUp)
    {
        lives += powerUp;
        health = maxHealth;
    }

    public void AddHealth(int powerUp)
    {
        health += powerUp;

        if (health > maxHealth)     // We can only show six segments in our HUD.
        {
            health = maxHealth;
        }
    }

    public void FoundItem(int numFound)
    {
        remainingItems -= numFound;


        if(remainingItems == 0)
        {
            StartCoroutine(levelStateMachine.UnlockLevelExit());
        }
        // NOTE: We are deliberately not clamping this value to zero. 
        // This allows for levels where the number of pickups is greater than the target number needed. 
        // This also lets us speed up the testing process by temporarily reducing the collecatbles needed. 
        // Our HUD will clamp to zero for us.

    }

    public void FalloutDeath()
    {
        Die();
        return;
    }

    public void Die()
    {
        // play the death sound if available.
        if (deathSound)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);

        }

        lives--;
        health = maxHealth;

      

        // If we've reached here, the player still has lives remaining, so respawn.
        Vector3 respawnPosition = Respawn.currentRespawn.transform.position;
        Camera.main.transform.position = respawnPosition - (transform.forward * 4) + Vector3.up;    // reset camera too
                                                                                                    // Hide the player briefly to give the death sound time to finish...
        SendMessage("HidePlayer");

        // Relocate the player. We need to do this or the camera will keep trying to focus on the (invisible) player where he's standing on top of the FalloutDeath box collider.
        transform.position = respawnPosition + Vector3.up;

        StartCoroutine(WaitAFew());
    }

    public IEnumerator WaitAFew()
    {
        yield return new WaitForSeconds(1.6f);  // give the sound time to complete.
        // (NOTE: "HidePlayer" also disables the player controls.)

        SendMessage("ShowPlayer");  // Show the player again, ready for...	
                                    // ... the respawn point to play it's particle effect
        Respawn.currentRespawn.FireEffect();
    }


}