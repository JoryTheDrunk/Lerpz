using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotParticles : MonoBehaviour
{

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(GetComponent<ParticleSystem.MainModule>().duration / 2);
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule em = ps.emission;
        em.enabled = false;
    }
}