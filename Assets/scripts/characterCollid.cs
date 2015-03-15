using UnityEngine;
using System.Collections;

public class characterCollid : MonoBehaviour {

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
	}

}
