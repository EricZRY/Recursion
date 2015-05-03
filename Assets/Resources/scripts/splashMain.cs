using UnityEngine;
using System.Collections;

public class splashMain : MonoBehaviour {
	private GameObject controller;
	private GameObject menu;
	private int menuIndex;

	private float previousValue;
	private float currentValue;
	private float deltaValue;
	private Vector3  error;
	private bool gotFirstValue=false;

	private GameObject VRcamera;
	private GameObject[] subMenus=new GameObject[9];

	private AudioClip splashBGM;

	private bool audioPlay=true;

	
	void Awake(){

		menu=GameObject.Find("MENU");

		VRcamera=GameObject.Find("CenterEyeAnchor");
		DontDestroyOnLoad (GameObject.Find("OVRPlayerController 1"));

		for(int i=0;i<9;i++){
			string menuid="menu"+(i+1);
			subMenus[i]=GameObject.Find(menuid);
		}


		SoundManager.instance.BGMSource.Stop();
		SoundManager.instance.secondBGM=false;
		splashBGM=(AudioClip)Resources.Load("audio/bwv846");
		SoundManager.instance.PlayBGM (splashBGM);

		
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
//		//I know this is a bad idea, but I'm really not good at Math
//		transform.rotation = Quaternion.Euler (StickToNinty( Controller.pitch,45),StickToNinty( Controller.heading,45),StickToNinty( Controller.roll,45));
//		//Debug.Log (transform.rotation.eulerAngles);
//		//Debug.Log(Controller.pitch+";;"+Controller.heading+";;"+Controller.roll);
//
//		//rotate along x axis to select
//		if (transform.TransformDirection (Vector3.right)==new Vector3(1,0,0) ){
//			if(Controller.roll==0){
//				moveMenu(Controller.pitch);
//			}
//			else if(Controller.roll==-180){
//				moveMenu(-Controller.pitch);
//
//			}
//		}
//		//rotate along -x axis to select
//		else if(transform.TransformDirection (Vector3.left)==new Vector3(1,0,0)){
//			if(Controller.roll==0){
//				moveMenu(-Controller.pitch);
//			}
//			else if(Controller.roll==180){
//				moveMenu(Controller.pitch);
//				
//			}
//		}
//		//rotate along z axis to select
//		else if(transform.TransformDirection(Vector3.forward)==new Vector3(1,0,0)){
//			moveMenu(Controller.roll);
//
//		}
//		//rotate along -z axis to select
//		else if(transform.TransformDirection(Vector3.back)==new Vector3(1,0,0)){
//			moveMenu(-Controller.roll);
//
//		}
//		//rotate along y axis to select
//		else if(transform.TransformDirection(Vector3.up)==new Vector3(1,0,0)){
//			moveMenu(Controller.heading);
//
//		}
//		//rotate along -y axis to select
//		else if(transform.TransformDirection(Vector3.down)==new Vector3(1,0,0)){
//			moveMenu(-Controller.heading);
//
//		}
//		else{
//			gotFirstValue=false;
//		}


//
//		for (int i=0;i<9;i++){
//			if(i==0 || (i>=4 && i<=8)){ 
//				if(subMenus[i].transform.position.z>=20){
//					subMenus[i].transform.position=new Vector3(0,0.23f,-25);
//				}
//				else if(subMenus[i].transform.position.z<=-30){
//					subMenus[i].transform.position=new Vector3(0,0.23f,15);
//				}
//				else if(subMenus[i].transform.position.z>=-5 && subMenus[i].transform.position.z<0){
//					subMenus[i].transform.position=new Vector3(0,0.23f,10);
//				}
//				else if(subMenus[i].transform.position.z<=5 && subMenus[i].transform.position.z>0){
//					subMenus[i].transform.position=new Vector3(0,0.23f,-10);
//				}
//			}
//
//			else{
//				if(subMenus[i].transform.position.z>=10){
//					subMenus[i].transform.position=new Vector3(0,0.23f,-5);
//				}
//				else if(subMenus[i].transform.position.z<=-10){
//					subMenus[i].transform.position=new Vector3(0,0.23f,5);
//				}
//			}
//		}
//

		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}

		RaycastHit hit;
		if (Physics.Raycast(VRcamera.transform.position, VRcamera.transform.TransformDirection(Vector3.forward),out hit,10)){
			if(subMenus[3].transform.position.z==0 && ( hit.collider.gameObject.name=="startc")){
				if(audioPlay==true){
					SoundManager.instance.PlaySingle(SoundManager.instance.smallHighlight);
					audioPlay=false;
				}


				//Debug.Log("load select level scene");
				//loadObject("prefabs/bar","bar(Clone)", hit.collider.transform.position+ new Vector3(0,-1,0),Quaternion.identity);

				//GameObject mask=GameObject.Find("mask");
				GameObject selection=GameObject.Find("startc");
				//mask.transform.localScale*=0.8f;
				selection.renderer.material.color= Color.Lerp(selection.renderer.material.color,new Color32(200,40,170,255),Time.deltaTime*3);

				if(selection.renderer.material.color.g*255<45){
//					if(Controller.calibrateFin!=4){
//						Controller.clibrateMode=true;
//						Application.LoadLevel("clibrate");
//					}
//					else{
					SoundManager.instance.PlaySingle(SoundManager.instance.select);
						Destroy(GameObject.Find("OVRPlayerController 1"));

						Application.LoadLevel("level1");
//					}
				}
			}
			else if(subMenus[3].transform.position.z==0 && (hit.collider.gameObject.name=="continuec" )){
				if(audioPlay==true){
					SoundManager.instance.PlaySingle(SoundManager.instance.smallHighlight);
					audioPlay=false;
				}
				GameObject selection=GameObject.Find("continuec");
				//mask.transform.localScale*=0.8f;
				selection.renderer.material.color= Color.Lerp(selection.renderer.material.color,new Color32(200,40,170,255),Time.deltaTime*3);
				
				if(selection.renderer.material.color.g*255<45){
					//					if(Controller.calibrateFin!=4){
					//						Controller.clibrateMode=true;
					//						Application.LoadLevel("clibrate");
					//					}
					//					else{
					SoundManager.instance.PlaySingle(SoundManager.instance.select);
					
					Application.LoadLevel("selectLevel");
					//					}
				}
//				loadObject("prefabs/bar","bar(Clone)", hit.collider.transform.position+ new Vector3(0,-1,0),Quaternion.identity);
//				
//				GameObject mask=GameObject.Find("mask");
//				GameObject selection=GameObject.Find("calibrate");
//				mask.transform.localScale*=0.9f;
//				selection.renderer.material.color= Color.Lerp(new Color32(88,99,96,255),new Color32(112,56,56,255),1-mask.transform.lossyScale.x);
//				
//				if(mask.transform.lossyScale.x<0.1f){
//					Controller.clibrateMode=true;
//
//						Application.LoadLevel("clibrate");
//				}
			}
			//else if(subMenus[1].transform.position.z==0 && hit.collider.gameObject.name=="quit"){

			else if(subMenus[3].transform.position.z==0 && hit.collider.gameObject.name=="quitc"){
				//Debug.Log("load select level scene");
				if(audioPlay==true){
					SoundManager.instance.PlaySingle(SoundManager.instance.smallHighlight);
					audioPlay=false;
				}


				GameObject selection=GameObject.Find("quitc");
				selection.renderer.material.color= Color.Lerp(selection.renderer.material.color,new Color32(200,40,170,255),Time.deltaTime*3);
				
				if(selection.renderer.material.color.g*255<45){
					//					if(Controller.calibrateFin!=4){
					//						Controller.clibrateMode=true;
					//						Application.LoadLevel("clibrate");
					//					}
					//					else{
					SoundManager.instance.PlaySingle(SoundManager.instance.select);
					
					Application.Quit();
					//					}
				}
			}
		}
		else{
			audioPlay=true;

			GameObject.Find("startc").renderer.material.color=Color.Lerp(GameObject.Find("startc").renderer.material.color,new Color32(0,255,128,255),Time.deltaTime*3);
			//GameObject.Find("calibrate").renderer.material.color=new Color32(88,99,96,255);
			GameObject.Find("quitc").renderer.material.color=Color.Lerp(GameObject.Find("quitc").renderer.material.color,new Color32(0,255,128,255),Time.deltaTime*3);
			GameObject.Find("continuec").renderer.material.color=Color.Lerp(GameObject.Find("continuec").renderer.material.color,new Color32(0,255,128,255),Time.deltaTime*3);

		}

	}







	private void loadObject( string path, string name,Vector3 position,Quaternion rotation){
		if (GameObject.Find (name) == null) {
			GameObject controller=(GameObject)Resources.Load(path);
			Instantiate(controller, position, rotation) ;
		}
	}



	private void moveMenu(float value){
			if(  atNinties(value)==true && gotFirstValue==false){
				error+= Vector3.back * StickToNinty( deltaValue,45) *5/90;
				deltaValue=0;
				//previousValue = value;
				gotFirstValue=true;
			}
			if(atNinties(value)==false && gotFirstValue==true){
				previousValue =StickToNinty( value,45);

				gotFirstValue=false;
			}

		if(atNinties(value)==false){
			currentValue = value ;
			Debug.Log(Controller.pitch+";;"+Controller.heading+";;"+Controller.roll);
			if(Mathf.Abs(currentValue-previousValue)<=120){

				deltaValue = (currentValue-previousValue);
				//Debug.Log (currentValue);
			}

		}

		menu.transform.position=Vector3.back * deltaValue*5/90+error;


	}


	private bool atNinties(float value ){
		float range = 0;
		if ((value*value<=(360+range)*(360+range) && value*value>=(360-range)*(360-range)) || (value*value<=(270+range)*(270+range) && value*value>=(270-range)*(270-range)) || (value*value<=(180+range)*(180+range) && value*value>=(180-range)*(180-range)) ||(value*value<=(90+range)*(90+range) && value*value>=(90-range)*(90-range)) || (value*value<=range*range && value*value>=0)){
			return true;
		}
		else {
			return false;
		}
	}



	//make the value equals to multiples of 90 if near it
	private float StickToNinty(float value, int range){
		
		if((value>90-range && value<90+range)){
			return 90;
		}
		else if(value>-range && value<range) {
			return 0;
		}
		
		else if(value>180-range && value<180+range){
			return 180;
		}
		else if(value>-90-range && value<-90+range){
			return -90;
		}
		else if(value>-180-range && value<-180+range){
			return -180;
		}
		else if(value>-270-range && value<-270+range){
			return -270;
		}
		else if(value>-360-range && value<-360+range){
			return -360;
		}
		else if(value>270-range && value<270+range){
			return 270;
		}
		else if(value>360-range && value<360+range){
			return 360;
		}
		else{
			return value;
		}
		
	}
}


