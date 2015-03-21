using UnityEngine;
using System.Collections;
using TMPro;


public class Controller : MonoBehaviour {
	public bool clibrateMode = false;


	private float firstHeading=0;
	private bool sceneInit=false;
	private bool gettoOriginalOrientation = false;
	private bool getFourValues=false;
	
	public static bool getFirstValue=false;

	private float pitchError = 0;
	private float headingError = 0;
	private float rollError = 0;
	
	public static float heading;
	public static float pitch;
	public static float roll;


	private float sensorValueAtNinty=0;
	private float sensorValueAtPi=0;
	private float sensorValueAtTwoSeven=0;
	private float sensorValueAtZero=0;

	void Start () {
	}
	void Awake() {
		//keep the first heading value over scenes
		DontDestroyOnLoad(transform.gameObject);
	}

	void Update () {
		//--------------------------get euler angles--------------------------------------
		//set heading value to zero
		if (getFirstValue==false && Input.GetAxis("Heading" )!=0) {
			
			firstHeading=(Input.GetAxis("Heading" )+1)*180-180;
			getFirstValue=true;
			//Debug.Log (firstHeading);
		}
		
		
		
		heading = (Input.GetAxis("Heading" )+1)*180-180-firstHeading;
		pitch = (Input.GetAxis("Pitch")+1)*90-90;
		roll = (Input.GetAxis ("Roll")+1)*180-180; 

//		if ((Input.GetButtonDown ("X")) || (Input.GetButtonDown ("Y")) || (Input.GetButtonDown ("Z"))) {
//			
//			pitchError = getError(pitch);
//			headingError = getError(heading);
//			rollError = getError(roll);
//		}
//		heading = heading + headingError;
//		pitch = pitch + pitchError;
//		roll = roll + rollError;

		heading = StickToNinty (heading, 25	);
		pitch = StickToNinty (pitch, 20);
		roll = StickToNinty (roll, 20);
		//Debug.Log (heading);







		//--------------------------clibrate----------------------------

		if (clibrateMode) {
			
			GameObject backGround=GameObject.Find("pCube1");
			GameObject sceneHolder=GameObject.Find("scene");
			GameObject innerScene=GameObject.Find ("innerscene");
			GameObject ctrCube=GameObject.Find("controllerCube");
			TextMeshPro text=  GameObject.Find("Text1").GetComponent<TextMeshPro>();

			if(sceneInit==false){
				if((heading!=0 || pitch!=0 || roll!=0) ){
					gettoOriginalOrientation=false;
					ctrCube.transform.rotation=Quaternion.Euler( new Vector3(pitch,heading,roll));

				}
				else {
					gettoOriginalOrientation=true;
				}
				
				sceneInit=true;
			}

			//if not in original orientation
			if(gettoOriginalOrientation ==false){

				if(StickToNinty( ctrCube.transform.rotation.eulerAngles.x,1)==0  &&
				   StickToNinty( ctrCube.transform.rotation.eulerAngles.y,1)==0  &&
				   StickToNinty( ctrCube.transform.rotation.eulerAngles.z,1)==0 
				   ){
					backGround.renderer.material.color=Color.Lerp( backGround.renderer.material.color,new Color(scale(0,255,0,1,67),scale(0,255,0,1,79),scale(0,255,0,1,73),1),Time.deltaTime *0.5f);
					text.color=Color.Lerp(text.color, new Color(255,255,255,0), Time.deltaTime * 1f);

					
					if(backGround.renderer.material.color.r >= scale(0,255,0,1,65)){
						gettoOriginalOrientation = true;
					}
				}
				else{
					ctrCube.transform.rotation=Quaternion.Lerp( ctrCube.transform.rotation,Quaternion.Euler( new Vector3(pitch,heading,roll)), Time.deltaTime*4);
					
					
					backGround.renderer.material.color=new Color(0.05f,0.05f,0.05f,1);
					//"rotate the controller until you see the two arrows point to the same direction  "
					text.GetComponent<TextMeshPro>().SetText("rotate the cube until you see\nthe two arrows point to the same direction",0f);
					text.color=Color.Lerp(text.color, Color.white, Time.deltaTime * 1f);

				}
			}

			if(gettoOriginalOrientation==true && getFourValues==false){
				//"rotate the controller colckwise" 
				text.GetComponent<TextMeshPro>().SetText("rotate the cube colckwise four times",0f);
				text.color=Color.Lerp(text.color, Color.white, Time.deltaTime * 1f);

				//cube rotating animation
				Animator animator = GameObject.Find("Cube"). GetComponent<Animator>();
				animator.SetBool("getfourvalues", true);
				//controller control the room

				backGround.transform.rotation=Quaternion.Lerp( backGround.transform.rotation,Quaternion.Euler( new Vector3(pitch,heading,roll)), Time.deltaTime*4);

			}
			//back to menu automatically
			
			
		}
		

	}


	
	float getError(float value){
		float Error;
		Error = StickToNinty (value, 45) - value;
		return Error;
	}

	//mpping values
	float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue){
		float OldRange = (OldMax - OldMin);
		float NewRange = (NewMax - NewMin);
		float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
		
		return(NewValue);
	}


	//make the value equals to multiples of 90 if near it
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
