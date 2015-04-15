using UnityEngine;
using System.Collections;

public class DONDIE : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (gameObject);
		if (Application.loadedLevelName == "level1") {
			transform.position=new Vector3(0,-6.27f,-8.04f);
		}
		else if (Application.loadedLevelName == "level2") {
			
		}
		else if (Application.loadedLevelName == "level3") {
			
		}
		else if (Application.loadedLevelName == "level4") {
			
		}
		else if (Application.loadedLevelName == "level5") {
			
		}
		else if (Application.loadedLevelName == "level6") {
			
		}
		else if (Application.loadedLevelName == "level7") {
			
		}
	}
	

}
