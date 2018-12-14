//-JAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[AddComponentMenu("(Misc)/Level Loading Progress")]

public class loadingLevelProgress : MonoBehaviour {

public string levelName;
private float percentDone = 0;

	void Update(){
		if(Application.GetStreamProgressForLevel(levelName) == 1){
			SceneManager.LoadScene(levelName);
		}
		else{
			percentDone = Application.GetStreamProgressForLevel(levelName) * 100;
		}
	}
	
	void OnGUI(){
		GUI.Label(new Rect(200,200,600,600), "Loading... " + percentDone.ToString() + "%");
	}
}
