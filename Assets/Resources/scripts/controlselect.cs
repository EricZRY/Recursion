using UnityEngine;
using System.Collections;

public class controlselect : MonoBehaviour {
	private GameObject VRcamera;
	private GameObject gamepad;
	private GameObject cubic;
	// Use this for initialization
	void Awake () {
//		DontDestroyOnLoad (gameObject);
		VRcamera = GameObject.Find ("CenterEyeAnchor");
		gamepad = GameObject .Find ("gamepad");
		cubic = GameObject.Find ("cubic");
		loadObject ("prefabs/Controller","Controller(Clone)", new Vector3(0, 0, 0), Quaternion.identity);

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}
		if(Application.loadedLevelName=="intro2"){
			RaycastHit hit;
			if (Physics.Raycast (VRcamera.transform.position, VRcamera.transform.TransformDirection (Vector3.forward), out hit, 100)) {
				if( hit.collider.gameObject.name=="gamepad"){
					hit.collider.gameObject.renderer.material.color=Color.Lerp(hit.collider.gameObject.renderer.material.color,new Color32(200,40,170,255),Time.deltaTime*3);
					if(hit.collider.gameObject.renderer.material.color.g*255<45){
						Controller.controlMode="gamepad";
						Application.LoadLevel("Splash");
					}
				}
				else if( hit.collider.gameObject.name=="cubic"){
					hit.collider.gameObject.renderer.material.color=Color.Lerp(hit.collider.gameObject.renderer.material.color,new Color32(200,40,170,255),Time.deltaTime*3);
					if(hit.collider.gameObject.renderer.material.color.g*255<45){
						Controller.controlMode="cubic";
						Application.LoadLevel("Splash");
					}
				}

			}
			else{
				gamepad .renderer.material.color=Color.Lerp(gamepad.renderer.material.color,new Color32(0,255,128,255),Time.deltaTime*3);
				cubic.renderer.material.color=Color.Lerp(cubic.renderer.material.color,new Color32(0,255,128,255),Time.deltaTime*3);
			}
		}
	}


	private void loadObject( string path, string name,Vector3 position,Quaternion rotation){
		if (GameObject.Find (name) == null) {
			GameObject controller=(GameObject)Resources.Load(path);
			Instantiate(controller, position, rotation) ;
		}
	}
}
