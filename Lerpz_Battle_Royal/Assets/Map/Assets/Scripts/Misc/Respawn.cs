//Cale Toburen
//Date-5-2-18
//Project-CSG-128
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    /*
Respawn: Allows players to respawn to this point in the level, effectively saving their progress.

The Respawn object has three main states and one interim state: Inactive, Active and Respawn, plus Triggered.

- Inactive: Player hasn't reached this point and the player will not respawn here.

- Active: Player has touched this respawn point, so the player will respawn here.

- Respawn: Player is respawning at this respawn point.

Each state has its own visual effect(s).

Respawn objects also require a simple collider, so the player can activate them. The collider is set as a trigger.

*/

    public Respawn initialRespawn;	// set this to the initial respawn point for the level.

    public int RespawnState = 0;

    // Sound effects:
    public AudioClip SFXPlayerRespawn;
    public AudioClip SFXRespawnActivate;
    public AudioClip SFXRespawnActiveLoop;

    public float SFXVolume;	// volume for one-shot sounds.

    // references for the various particle emitters...
    private ParticleSystem emitterActive;
    private ParticleSystem emitterInactive;
    private ParticleSystem emitterRespawn1;
    private ParticleSystem emitterRespawn2;
    private ParticleSystem emitterRespawn3;

    // ...and for the light:
    private Light respawnLight;


    // The currently active respawn point. Static, so all instances of this script will share this variable.
    public static Respawn currentRespawn;

    void Start()
    {
        // Get some of the objects we need later.
        // This is often done in a script's Start function. That way, we've got all our initialization code in one place, 
        // And can simply count on the code being fine.
        emitterActive = transform.Find("RSParticlesActive").GetComponent<ParticleSystem>();
        emitterInactive = transform.Find("RSParticlesInactive").GetComponent<ParticleSystem>();
        emitterRespawn1 = transform.Find("RSParticlesRespawn1").GetComponent<ParticleSystem>();
        emitterRespawn2 = transform.Find("RSParticlesRespawn2").GetComponent<ParticleSystem>();
        emitterRespawn3 = transform.Find("RSParticlesRespawn3").GetComponent<ParticleSystem>();

        respawnLight = transform.Find("RSSpotlight").GetComponent<Light>();

        RespawnState = 0;

        // set up the looping "RespawnActive" sound, but leave it switched off for now:
        if (SFXRespawnActiveLoop)
        {
            GetComponent<AudioSource>().clip = SFXRespawnActiveLoop;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().playOnAwake = false;
        }

        // Assign the respawn point to be this one - Since the player is positioned on top of a respawn point, it will come in and overwrite it.
        // This is just to make sure that we always have a respawn point.
        currentRespawn = initialRespawn;
        if (currentRespawn == this)
            SetActive();
    }

    void OnTriggerEnter()
    {
        if (currentRespawn != this)     // make sure we're not respawning or re-activating an already active pad!
        {
            // turn the old respawn point off
            currentRespawn.SetInactive();

            // play the "Activated" one-shot sound effect if one has been supplied:
            if (SFXRespawnActivate)
                AudioSource.PlayClipAtPoint(SFXRespawnActivate, transform.position, SFXVolume);

            // Set the current respawn point to be us and make it visible.
            currentRespawn = this;

            SetActive();
        }
    }

    void SetActive()
    {
        emitterActive.Play(true);
        emitterInactive.Play(false);
        respawnLight.intensity = 1.5f;

        GetComponent<AudioSource>().Play();     // start playing the sound clip assigned in the inspector
    }

    void SetInactive()
    {
        emitterActive.Play(false);
        emitterInactive.Play(true);
        respawnLight.intensity = 1.5f;

        GetComponent<AudioSource>().Stop(); // stop playing the active sound clip.			
    }

    public IEnumerator FireEffect()
    {
        // Launch all 3 sets of particle systems.
        emitterRespawn1.Play();
        emitterRespawn2.Play();
        emitterRespawn3.Play();

        respawnLight.intensity = 3.5f;

        if (SFXPlayerRespawn)
        {   // if we have a 'player is respawning' sound effect, play it now.
            AudioSource.PlayClipAtPoint(SFXPlayerRespawn, transform.position, SFXVolume);
        }

        yield return new WaitForSeconds(2);

        respawnLight.intensity = 2.0f;
    }

}