using UnityEngine;
using System.Collections;

public class characterCollid : MonoBehaviour {

	public static bool flagOrientation=false;
	public string toLoadScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag=="spikes"){
			//Debug.Log("Die!");
			Application.LoadLevel(Application.loadedLevel);
		}

		if(other.tag=="flag" && flagOrientation==true ){
			Application.LoadLevel(toLoadScene);
		}
	}

}
