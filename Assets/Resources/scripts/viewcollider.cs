using UnityEngine;
using System.Collections;

public class viewcollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider other) {

		if (other.collider.gameObject.name == "exit") {
						//Debug.Log ("touchExit");
			main.instance.exiting=true;
		}
		else if (other.collider.gameObject.name == "circle"){
			main.instance.forwarding=true;
		}

	}

	void OnTriggerExit(Collider other) {
		if (other.collider.gameObject.name == "exit") {
			//Debug.Log ("touchExit");
			main.instance.exiting=false;
		}
		else if (other.collider.gameObject.name == "circle"){
			main.instance.forwarding=false;
		}
	}
}
