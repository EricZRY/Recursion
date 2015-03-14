using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	private float firstHeading=0;
	private bool getFirstValue=false;


	public static float heading;
	public static float pitch;
	public static float roll;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (getFirstValue==false && Input.GetAxis("Heading" )!=0) {
			
			firstHeading=(Input.GetAxis("Heading" )+1)*180-180;
			getFirstValue=true;
			//Debug.Log (firstHeading);
			
		}
		
		
		
		heading = (Input.GetAxis("Heading" )+1)*180-180-firstHeading;
		pitch = (Input.GetAxis("Pitch")+1)*90-90;
		roll = (Input.GetAxis ("Roll")+1)*180-180; 
		
		heading = StickToNinty (heading, 30);
		pitch = StickToNinty (pitch, 20);
		roll = StickToNinty (roll, 20);
		
		
		//RotRoom.transform.rotation=Quaternion.Lerp( transform.rotation,Quaternion.Euler( new Vector3(pitch,heading,roll)), Time.deltaTime*4);
		//Debug.Log (heading);
	}
	
	
	float StickToNinty(float value, int range){
		
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
