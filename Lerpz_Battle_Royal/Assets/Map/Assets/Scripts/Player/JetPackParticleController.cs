using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class JetPackParticleController : MonoBehaviour
{

    private float litAmount = 0.00f;
    ThirdPersonController playerController;
    Light childLight;
    ParticleSystem.EmissionModule[] particles;

    IEnumerator Start()
    {
        playerController = GetComponent<ThirdPersonController>();


        // The script ensures an AudioSource component is always attached.

        // First, we make sure the AudioSource component is initialized correctly:
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().Stop();


        // Init the particles to not emit and switch off the spotlights:
        particles = GetComponentsInChildren<ParticleSystem.EmissionModule>();
        childLight = GetComponentInChildren<Light>();

        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].enabled = false;
        }

        childLight.enabled = false;

        // Once every frame  update particle emission and lights
        while (true)
        {
            bool isFlying = playerController.IsJumping();

            // handle thruster sound effect
            if (isFlying)
            {
                if (!GetComponent<AudioSource>().isPlaying)
                {
                    GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                GetComponent<AudioSource>().Stop();
            }

            if (isFlying)
            {
                //em.enabled = true;
            }
            else
            {
                //em.enabled = false;
            }

            if (isFlying)
                litAmount = Mathf.Clamp01(litAmount + Time.deltaTime * 2);
            else
                litAmount = Mathf.Clamp01(litAmount - Time.deltaTime * 2);
            childLight.enabled = isFlying;
            childLight.intensity = litAmount;

            yield return new WaitForSeconds(0.01f);
        }
    }
}
