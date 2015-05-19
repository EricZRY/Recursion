using UnityEngine;
using System.Collections;

public class characterCollid : MonoBehaviour {

	public static bool flagOrientation=false;
	private string toLoadScene;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (gameObject);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}

		if (Application.loadedLevelName == "level1" && toLoadScene!="level2" ) {
			gameObject.transform.position=new Vector3(0,-6.27f,-8.04f);


			toLoadScene="level2";
		}
		else if (Application.loadedLevelName == "level2" && toLoadScene!="level3") {
			gameObject.transform.position=new Vector3(0,-6.27f,5.67f);

			toLoadScene="level3";

		}
		else if (Application.loadedLevelName == "level3" && toLoadScene!="level4") {
			gameObject.transform.position=new Vector3(0,-7f,-6f);

			toLoadScene="level4";

		}
		else if (Application.loadedLevelName == "level4" && toLoadScene!="level5") {
			gameObject.transform.position=new Vector3(0,-6.27f,-7.92f);

			toLoadScene="level5";

		}
		else if (Application.loadedLevelName == "level5" && toLoadScene!="level6") {
			gameObject.transform.position=new Vector3(0,-6.27f,-8.8f);

			toLoadScene="level6";

		}
		else if (Application.loadedLevelName == "level6" && toLoadScene!="level7") {
			gameObject.transform.position=new Vector3(1.3f,-3.21f,-8.94f);
			
			toLoadScene="level7";
			
		}
		else if (Application.loadedLevelName == "level6" && toLoadScene!="level7") {
			gameObject.transform.position=new Vector3(-3.6f,-5.86f,-4.79f);

			toLoadScene="level8";

		}
		else if (Application.loadedLevelName == "level18" && toLoadScene!="Splash") {
			gameObject.transform.position=new Vector3(9.73f,-9.67f,8.24f);

			toLoadScene="Splash";

		}


		Vector3 fowardXZ= new Vector3(main.instance.VRcamera.transform.TransformDirection(Vector3.forward).x,0,main.instance.VRcamera.transform.TransformDirection(Vector3.forward).z);
		Ray obscaleRay = new Ray(main.instance.character.transform.position-Vector3.up*1.6f,fowardXZ );
		Ray downRay = new Ray(main.instance.character.transform.position-Vector3.up*1.6f,Vector3.down);
		RaycastHit hit;
		RaycastHit downhit;

		if(Physics.Raycast(obscaleRay, out hit)) {
			if(hit.distance<0.8f  ){
				if(hit.transform.gameObject.tag=="flag" && flagOrientation==true){
					transform.parent=null;
					Application.LoadLevel(toLoadScene);
				}
				if(hit.transform.gameObject.tag=="spikes" ){
					transform.parent=null;
					Application.LoadLevel(Application.loadedLevel);
				}
			}

		}
		//Debug.DrawRay(main.instance.character.transform.position-Vector3.up*1.6f,Vector3.down, Color.green);
		if (Physics.Raycast (downRay, out downhit)) {
			if(downhit.distance<1f  ){
				if(downhit.transform.gameObject.tag=="spikes" ){

					transform.parent=null;
					Application.LoadLevel(Application.loadedLevel);
				}
			}
		}
		
		
		
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.tag=="spikes"){
			//Debug.Log("Die!");
			transform.parent=null;
			Application.LoadLevel(Application.loadedLevel);
		}

		Debug.Log(other.tag);


		if(other.tag=="flag" && flagOrientation==true ){
			transform.parent=null;
			Application.LoadLevel(toLoadScene);
		}
	}

}
