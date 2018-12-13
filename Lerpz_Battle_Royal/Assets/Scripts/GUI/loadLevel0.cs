//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[AddComponentMenu("(Misc)/GUI Load Scenes")]

public class loadLevel0 : MonoBehaviour {

public string levelName;	//the scene/level to load
public bool amQuit = false;

	public void LoadClick(){	//called upon by Unitys default GUI system
		if(amQuit == false){
			SceneManager.LoadScene(levelName);
		}	//just a quick game exit script
		else if(amQuit == true){
			Application.Quit();
		}
	}
}
