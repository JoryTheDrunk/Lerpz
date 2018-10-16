using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameHud : MonoBehaviour {

    public GameObject[] healthPieImages;
    [HideInInspector] public ThirdPersonStatus playerInfo;
    public Text FuelCellsLeft;
    public Text liveLeftText;
    private int playerHealth = 6;


    // Use this for initialization

    void Start () {
        
        playerInfo = FindObjectOfType<ThirdPersonStatus>();
        playerHealth = playerInfo.health;
        
    }
	
	// Update is called once per frame
	void Update () {


        liveLeftText.text = playerInfo.lives.ToString();
        FuelCellsLeft.text = playerInfo.remainingItems.ToString();

        if (playerInfo.health < playerHealth)
        {
            playerHealth--;
            healthPieImages[playerHealth].SetActive(false);
        }
        if(playerInfo.health == 6)
        {
            healthPieImages[0].SetActive(true);
            healthPieImages[1].SetActive(true);
            healthPieImages[2].SetActive(true);
            healthPieImages[3].SetActive(true);
            healthPieImages[4].SetActive(true);
            healthPieImages[5].SetActive(true);
            playerHealth = 6;
        }

    }


}
