//Jake Poshepny

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStatus : MonoBehaviour
{
    // LevelStatus: Master level state machine script.
    public GameObject exitGateway;
    public GameObject levelGoal;
    public AudioClip unlockedSound;
    public AudioClip levelCompleteSound;
    public GameObject mainCamera;
    public GameObject unlockedCamera;
    public GameObject levelCompletedCamera;

    // This is where info like the number of items the player must collect in order to complete the level lives.
    public int itemsNeeded = 20;
    

    // Awake(): Called by Unity when the script has loaded.
    // We use this function to initialise our link to the Lerpz GameObject.
    private void Awake()
    {
   
       
    }
    void Update()
    {
     
    }

    public IEnumerator UnlockLevelExit()
    {
        mainCamera.GetComponent<AudioListener>().enabled = false;
        unlockedCamera.SetActive(true);
        unlockedCamera.GetComponent<AudioListener>().enabled = true;
        exitGateway.GetComponent<AudioSource>().Stop();

        if (unlockedSound)
            AudioSource.PlayClipAtPoint(unlockedSound, unlockedCamera.GetComponent<Transform>().position, 2.0f);

        yield return new WaitForSeconds(1);
        exitGateway.SetActive(false); // ... the fence goes down briefly...
        yield return new WaitForSeconds(.2f); //... pause for a fraction of a second...
        exitGateway.SetActive(true); //... now the fence flashes back on again...
        yield return new WaitForSeconds(.2f); //... another brief pause before...
        exitGateway.SetActive(false); //... the fence finally goes down forever!
        //levelGoal.GetComponent<MeshCollider>().isTrigger = false;
        yield return new WaitForSeconds(4); // give the player time to see the result.
        // swap the cameras back.
        unlockedCamera.SetActive(false); // this lets the NearCamera get the screen all to itself.
        unlockedCamera.GetComponent<AudioListener>().enabled = false;
        mainCamera.GetComponent<AudioListener>().enabled = true;
    }


}
