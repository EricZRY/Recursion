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

	private bool havingValue = false;

	private int calibrateFin=0;

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
		
		
		
		calculateHeading(sensorValueAtNinty,sensorValueAtPi,sensorValueAtTwoSeven,sens);
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







		//--------------------------calibrate----------------------------

		if (clibrateMode) {
			
			GameObject backGround=GameObject.Find("pCube1");
			GameObject structure=GameObject.Find("structure");
			GameObject sceneHolder=GameObject.Find("scene");
			GameObject innerScene=GameObject.Find ("innerscene");
			GameObject ctrCube=GameObject.Find("controllerCube");
			GameObject arrow=GameObject.Find("arrow");
			GameObject arrows=GameObject.Find("arrows");
			Animator animator = GameObject.Find("Cube"). GetComponent<Animator>();

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

					if(text.color.a < 0.05){
						text.color=new Color(1,1,1,0);
						gettoOriginalOrientation = true;
					}
					else{
						text.color=Color.Lerp(text.color, new Color(1,1,1,0), Time.deltaTime * 5f);
					}

				}
				else{
					if(StickToNinty( backGround.transform.rotation.eulerAngles.x,1)!=0  ||
					   StickToNinty( backGround.transform.rotation.eulerAngles.y,1)!=0  ||
					   StickToNinty( backGround.transform.rotation.eulerAngles.z,1)!=0 
					   ){
						backGround.transform.rotation=Quaternion.Lerp( backGround.transform.rotation,Quaternion.Euler( 0,0,0), Time.deltaTime*4);
					}
					else{
						backGround.transform.rotation=Quaternion.Euler( 0,0,0);
					}

					ctrCube.transform.rotation=Quaternion.Lerp( ctrCube.transform.rotation,Quaternion.Euler( new Vector3(pitch,heading,roll)), Time.deltaTime*4);
					
					

					specularLerp(backGround,new Color(0.03f,0.03f,0.03f,1),new Color32(62,21,3,255),0.5f);
					specularLerp(structure,new Color32(11,3,0,255),new Color32(58,51,13,255),0.5f);
					specularLerp(arrow,new Color32(98,94,81,255),new Color32(21,105,102,255),0.5f);
					specularLerp(arrows,new Color32(68,36,6,255),new Color32(116,122,246,255),0.5f);

					//"rotate the controller until you see the two arrows point to the same direction  "
					if(checkValues()==5){
						text.GetComponent<TextMeshPro>().SetText("rotate the cube until you see\nthe two arrows point to the same direction",0f);
					}
					else if(checkValues()==0){
						text.GetComponent<TextMeshPro>().SetText("Illegal values. Please do it again\nrotate the cube until you see\nthe two arrows point to the same direction",0f);
					}
					text.color=Color.Lerp(text.color, Color.white, Time.deltaTime * 1f);

				}
			}

			if(gettoOriginalOrientation==true && getFourValues==false){
				specularLerp(backGround,new Color32(96,99,95,255),new Color32(112,86,0,255),0.5f);
				specularLerp(structure,new Color32(79,45,32,255),new Color32(148,0,0,255),0.5f);			
				specularLerp(arrow,new Color32(118,102,32,255),new Color32(255,90,0,255),0.5f);
				specularLerp(arrows,new Color32(98,94,81,255),new Color32(21,105,102,255),0.5f);



				//"rotate the controller colckwise" 
				text.GetComponent<TextMeshPro>().SetText("keep the arrow up\nand rotate the cube colckwise",0f);
				text.color=Color.Lerp(text.color, Color.white, Time.deltaTime * 1f);

				//cube rotating animation
				animator.SetBool("getfourvalues", true);

				//controller control the room
				backGround.transform.rotation=Quaternion.Lerp( backGround.transform.rotation,Quaternion.Euler( new Vector3(pitch,heading,roll)), Time.deltaTime*4);

				//get the four values
				if(Input.GetButtonDown("X") || Input.GetMouseButtonDown("0")){
					havingValue=true;
				}
				if(havingValue){
					calibrateFin+=1;
					if(calibrateFin==1){
						sensorValueAtNinty=(Input.GetAxis("Heading" )+1)*180-180;
						Debug.Log((Input.GetAxis("Heading" )+1)*180-180);
						havingValue=false;
					}
					else if(calibrateFin==2){
						sensorValueAtPi=(Input.GetAxis("Heading" )+1)*180-180;
						Debug.Log((Input.GetAxis("Heading" )+1)*180-180);
						havingValue=false;
					}
					else if(calibrateFin==3){
						sensorValueAtTwoSeven=(Input.GetAxis("Heading" )+1)*180-180;
						Debug.Log((Input.GetAxis("Heading" )+1)*180-180);
						havingValue=false;
					}
					else if(calibrateFin==4){
						sensorValueAtZero=(Input.GetAxis("Heading" )+1)*180-180;
						Debug.Log((Input.GetAxis("Heading" )+1)*180-180);
						havingValue=false;
					}
				}

				//check the values
				if(calibrateFin==4){
					//illegal values
					if(checkValues()==0){
						calibrateFin=0;
						ctrCube.transform.rotation=Quaternion.Lerp( ctrCube.transform.rotation,Quaternion.Euler( new Vector3(pitch,heading,roll)), Time.deltaTime*4);
						backGround.transform.rotation=Quaternion.Lerp( backGround.transform.rotation,Quaternion.Euler( 0,0,0), Time.deltaTime*4);
						gettoOriginalOrientation =false;
						animator.SetBool("getfourvalues", false);
					}

					else{
						//get values successfully, back to menu

					}
				}

			
				//if rotate wrong, back to last step
				if(StickToNinty( backGround.transform.rotation.eulerAngles.x,1)!=0  ||
				   StickToNinty( backGround.transform.rotation.eulerAngles.z,1)!=0 
				   ){
						calibrateFin=0;
						ctrCube.transform.rotation=Quaternion.Lerp( ctrCube.transform.rotation,Quaternion.Euler( new Vector3(pitch,heading,roll)), Time.deltaTime*4);
						backGround.transform.rotation=Quaternion.Lerp( backGround.transform.rotation,Quaternion.Euler( 0,0,0), Time.deltaTime*4);
						gettoOriginalOrientation =false;
						animator.SetBool("getfourvalues", false);
				}
			}


			
		}
		

	}


	void calculateHeading(float value1,float value2,float value3,float value4){

		if (checkValues() == 1) {
			if (((Input.GetAxis ("Heading") + 1) * 180 - 180) >= value4 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) < value1) {
				heading = scale (value4, value1,0 , 90, (Input.GetAxis ("Heading") + 1) * 180 - 180);
			} else if ((((Input.GetAxis ("Heading") + 1) * 180 - 180) >= value1 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) < 180)) {
				heading = scale (value1, 180+180+value2, 90, 180, (Input.GetAxis ("Heading") + 1) * 180 - 180);
			} else if ((((Input.GetAxis ("Heading") + 1) * 180 - 180) >= -180 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) < value2)) {
				heading = scale (-180-180+value1, value2, 90, 180, (Input.GetAxis ("Heading") + 1) * 180 - 180);
			} else if (((Input.GetAxis ("Heading") + 1) * 180 - 180) >=value2 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) <value3) {
				heading = scale (value2, value3, -180, -90, (Input.GetAxis ("Heading") + 1) * 180 - 180);
			}else if (((Input.GetAxis ("Heading") + 1) * 180 - 180) >=value3 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) <value4) {
				heading = scale (value3, value4,-90, 0, (Input.GetAxis ("Heading") + 1) * 180 - 180);
				
			}

		}
		else if(checkValues() == 2){
			if (((Input.GetAxis ("Heading") + 1) * 180 - 180) >= value4 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) < value1) {
				heading = scale (value4, value1,0 , 90, (Input.GetAxis ("Heading") + 1) * 180 - 180);
			} else if ((((Input.GetAxis ("Heading") + 1) * 180 - 180) >= value1 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) < value2)) {
				heading = scale (value1, value2, 90, 180, (Input.GetAxis ("Heading") + 1) * 180 - 180);
			} else if (((Input.GetAxis ("Heading") + 1) * 180 - 180) >=value2 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) <180) {
				heading = scale (value2, 180+180+value3, -180, -90, (Input.GetAxis ("Heading") + 1) * 180 - 180);
			} else if (((Input.GetAxis ("Heading") + 1) * 180 - 180) >=-180 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) <value3) {
				heading = scale (-180-180+value2, value3, -180, -90, (Input.GetAxis ("Heading") + 1) * 180 - 180);
			}else if (((Input.GetAxis ("Heading") + 1) * 180 - 180) >=value3 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) <value4) {
				heading = scale (value3, value4,-90, 0, (Input.GetAxis ("Heading") + 1) * 180 - 180);
				
			}

		}
		else if(checkValues()==3){
			if (((Input.GetAxis ("Heading") + 1) * 180 - 180) >= value4 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) < value1) {
				heading = scale (value4, value1,0 , 90, (Input.GetAxis ("Heading") + 1) * 180 - 180);
			} else if ((((Input.GetAxis ("Heading") + 1) * 180 - 180) >= value1 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) < value2)) {
				heading = scale (value1, value2, 90, 180, (Input.GetAxis ("Heading") + 1) * 180 - 180);
			} else if (((Input.GetAxis ("Heading") + 1) * 180 - 180) >=value2 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) <value3) {
				heading = scale (value2, value3, -180, -90, (Input.GetAxis ("Heading") + 1) * 180 - 180);
			} else if (((Input.GetAxis ("Heading") + 1) * 180 - 180) >=value3 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) <180) {
				heading = scale (value3, 180+180+value4,-90, 0, (Input.GetAxis ("Heading") + 1) * 180 - 180);
				
			} else if (((Input.GetAxis ("Heading") + 1) * 180 - 180) >= -180 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) < value4) {
				heading = scale (-180-180+value3, value4, -90, 0, (Input.GetAxis ("Heading") + 1) * 180 - 180);
				
			}

		}
		else if(checkValues()==4){
			if (((Input.GetAxis ("Heading") + 1) * 180 - 180) >= value4 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) < 180) {
				heading = scale (value4, 180+180+value1,0 , 90, (Input.GetAxis ("Heading") + 1) * 180 - 180);
			} else if (((Input.GetAxis ("Heading") + 1) * 180 - 180) >= -180 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) < value1) {
				heading = scale (-180-180+value4, value1,0 , 90, (Input.GetAxis ("Heading") + 1) * 180 - 180);
			} else if ((((Input.GetAxis ("Heading") + 1) * 180 - 180) >= value1 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) < value2)) {
				heading = scale (value1, value2, 90, 180, (Input.GetAxis ("Heading") + 1) * 180 - 180);
			} else if (((Input.GetAxis ("Heading") + 1) * 180 - 180) >=value2 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) <value3) {
				heading = scale (value2, value3, -180, -90, (Input.GetAxis ("Heading") + 1) * 180 - 180);
			} else if (((Input.GetAxis ("Heading") + 1) * 180 - 180) >=value3 && ((Input.GetAxis ("Heading") + 1) * 180 - 180) <value4) {
				heading = scale (value3, value4,-90, 0, (Input.GetAxis ("Heading") + 1) * 180 - 180);
			} 
		}
		else{
			heading = (Input.GetAxis("Heading" )+1)*180-180-firstHeading;
		}
	}

	
	int checkValues(){
						if (sensorValueAtNinty < sensorValueAtPi && sensorValueAtPi > sensorValueAtTwoSeven && sensorValueAtTwoSeven < sensorValueAtZero && sensorValueAtZero < sensorValueAtNinty){
							return 2;
						}
						else if(sensorValueAtNinty < sensorValueAtPi && sensorValueAtPi < sensorValueAtTwoSeven && sensorValueAtTwoSeven > sensorValueAtZero && sensorValueAtZero < sensorValueAtNinty){
							return 3;
						}
						else if(sensorValueAtNinty < sensorValueAtPi && sensorValueAtPi < sensorValueAtTwoSeven && sensorValueAtTwoSeven < sensorValueAtZero && sensorValueAtZero > sensorValueAtNinty){
							return 4;
						}
						else if(sensorValueAtNinty > sensorValueAtPi && sensorValueAtPi < sensorValueAtTwoSeven && sensorValueAtTwoSeven < sensorValueAtZero && sensorValueAtZero < sensorValueAtNinty){
							return 1;
						}
						else if(sensorValueAtNinty == sensorValueAtPi && sensorValueAtPi == sensorValueAtTwoSeven && sensorValueAtTwoSeven == sensorValueAtZero && sensorValueAtZero == sensorValueAtNinty){
							return 5;
						}
						else{
							return 0;
						}

		   }						


	void fadeOutText(TextMeshPro textm){
		if(textm.color.a < 0.05){
			textm.color=new Color(1,1,1,0);
		}
		else{
			textm.color=Color.Lerp(textm.color, new Color(1,1,1,0), Time.deltaTime * 5f);
		}
	}
	

	void specularLerp(GameObject obj, Color diffColor, Color specularColor,float speed){
		obj.renderer.material.color=Color.Lerp( obj.renderer.material.color,diffColor,Time.deltaTime *speed);
		setSpecularColor(obj, Color.Lerp( getSpecularColor(obj),specularColor,Time.deltaTime *speed));
	}



	void setSpecularColor(GameObject obj, Color color){
		Renderer rend = obj.GetComponent<Renderer>();

		rend.material.SetColor("_SpecColor", color);
	}

	 Color getSpecularColor(GameObject obj){
		Renderer rend = obj.GetComponent<Renderer>();
		return rend.material.GetColor("_SpecColor");
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
