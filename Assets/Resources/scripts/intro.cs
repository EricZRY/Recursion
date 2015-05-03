using UnityEngine;
using System.Collections;

public class intro : MonoBehaviour {

	private GameObject VRcamera;
	private GameObject text;
	private GameObject circle;

	private bool init=false;
	// Use this for initialization
	void Start () {
		VRcamera = GameObject.Find ("CenterEyeAnchor");
		text = GameObject.Find ("introText");
		circle = GameObject.Find ("circle");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}
		if(VRcamera.transform.rotation.eulerAngles!= new Vector3(0,0,0)){
			init=true;
		}
		//Debug.Log (VRcamera.transform.TransformDirection(Vector3.forward));
		//Debug.Log (VRcamera.transform.rotation.eulerAngles);
		if(init){
			if((VRcamera.transform.rotation.eulerAngles.x>=358 || VRcamera.transform.rotation.eulerAngles.x<=2) &&
			   (VRcamera.transform.rotation.eulerAngles.y>=358 || VRcamera.transform.rotation.eulerAngles.y<=2)){
				circle .renderer.material.color=Color.Lerp(circle.renderer.material.color,new Color32(200,40,170,255),Time.deltaTime*3);
				text.renderer.material.color=Color.Lerp(text.renderer.material.color,new Color(1,1,1,0),Time.deltaTime*3);
			}
			else{
				circle .renderer.material.color=Color.Lerp(circle.renderer.material.color,new Color32(0,255,128,255),Time.deltaTime*3);
				text.renderer.material.color=Color.Lerp(text.renderer.material.color,new Color(1,1,1,1),Time.deltaTime*3);

			}
			if(text.renderer.material.color.a<0.05){
				Application.LoadLevel("intro2");
			}



		}
	}
}
