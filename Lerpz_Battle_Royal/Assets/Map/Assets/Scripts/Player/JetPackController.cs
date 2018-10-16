//Jake Poshepny

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackController : MonoBehaviour
{
    public GameObject[] jetPacks;

    private ThirdPersonController tpc;

    private void Awake()
    {
        tpc = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        if (tpc.IsJumping() == true)
        {
            foreach (GameObject jp in jetPacks)
            {
                jp.SetActive(true);
            }
        }

        if (tpc.IsJumping() == false)
        {
            foreach (GameObject jp in jetPacks)
            {
                jp.SetActive(false);

            }
        }
    }
}