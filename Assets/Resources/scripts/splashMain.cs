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

	void Awake(){

		if (menu == null) {
			menu=GameObject.Find("MENU");
		}

		if (GameObject.Find ("Controller(Clone)") == null) {
			controller=(GameObject)Resources.Load("prefabs/Controller");
			Instantiate(controller, new Vector3(0, 0, 0), Quaternion.identity) ;
		}
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//I know this is a bad idea, but I'm really not good at Math
		transform.rotation = Quaternion.Euler (StickToNinty( Controller.pitch,45),StickToNinty( Controller.heading,45),StickToNinty( Controller.roll,45));
		//Debug.Log (transform.rotation.eulerAngles);
		//Debug.Log(Controller.pitch+";;"+Controller.heading+";;"+Controller.roll);

		//rotate along x axis to select
		if (transform.TransformDirection (Vector3.right)==new Vector3(1,0,0) ){
			if(Controller.roll==0){
				moveMenu(Controller.pitch);
			}
			else if(Controller.roll==-180){
				moveMenu(-Controller.pitch);

			}
		}
		//rotate along -x axis to select
		else if(transform.TransformDirection (Vector3.left)==new Vector3(1,0,0)){
			if(Controller.roll==0){
				moveMenu(-Controller.pitch);
			}
			else if(Controller.roll==180){
				moveMenu(Controller.pitch);
				
			}
		}
		//rotate along z axis to select
		else if(transform.TransformDirection(Vector3.forward)==new Vector3(1,0,0)){
			moveMenu(Controller.roll);

		}
		//rotate along -z axis to select
		else if(transform.TransformDirection(Vector3.back)==new Vector3(1,0,0)){
			moveMenu(-Controller.roll);

		}
		//rotate along y axis to select
		else if(transform.TransformDirection(Vector3.up)==new Vector3(1,0,0)){
			moveMenu(Controller.heading);

		}
		//rotate along -y axis to select
		else if(transform.TransformDirection(Vector3.down)==new Vector3(1,0,0)){
			moveMenu(-Controller.heading);

		}
		else{
			gotFirstValue=false;
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


