using UnityEngine;
using System.Collections;

public class mian : MonoBehaviour {
	public GameObject character;
	public GameObject room;
	public GameObject VRcamera;

	private bool rotating=false;
	private bool moving=false;
	private bool falling=false;

	private bool right;
	private bool left;
	private bool up;
	private bool down;
	private bool forward;
	private bool back;

	private Vector3 characterVel = new Vector3(0f,0f,0f);

	private int rotAngle;

	private string mode = "ControllerVR";

	void Start () {
		rotating = false;
		rotAngle = 0;

		down = false;
		right = false;
		left = false;
		up = false;
		forward = false;
		back = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(mode == "GamePad"){
			rotateSpace ();
		}
		else if(mode == "ControllerVR"){
			ControllerRot();
		}
		else if(mode == "keyBoard"){
			KeyBoardRot();
		}


	}


	void KeyBoardRot(){

	}


	void ControllerRot(){
		room.transform.rotation=Quaternion.Lerp( room.transform.rotation,Quaternion.Euler( new Vector3(Controller.pitch,Controller.heading,Controller.roll)), Time.deltaTime*4);
		character.transform.rotation = Quaternion.Euler( 0,0,0);

	
		if (atNinties(Controller.pitch)==true && atNinties(Controller.heading)==true && atNinties(Controller.roll)==true){
			rotating=false;
		}
		else {
			rotating=true;
		}


		if(rotating==false){
			velMax(7f,0.2f);
			characterHover(2.2f);
			characterMove();

			//Debug.Log(characterVel);
		}



	}

	void characterMove(){
		if (Input.GetMouseButtonDown(1) && falling==false){
			moving=true;
		}
		else if(Input.GetMouseButtonUp(1)){
			moving=false;

		}
	

		if(moving==true && falling == false && rotating==false){
			Vector3 fowardXZ= new Vector3(VRcamera.transform.TransformDirection(Vector3.forward).x,0,VRcamera.transform.TransformDirection(Vector3.forward).z);



			Ray obscaleRay = new Ray(character.transform.position-Vector3.up*1.6f, fowardXZ);
			RaycastHit hit;
			if(Physics.Raycast(obscaleRay, out hit)) {
				if(hit.distance<0.5f){
					moving=false;
				}
				else{
					characterVel+= fowardXZ*0.01f;
					character.transform.position+=characterVel;
				}
			}

		}

		if(moving==false){
			characterVel.x=0;
			characterVel.z=0;
		}


	}

	void characterHover(float hoverHeight){
		Ray downRay = new Ray(character.transform.position, -Vector3.up);
		RaycastHit hit;

		if (Physics.Raycast(downRay, out hit)) {
			float hoverError =  -hoverHeight + hit.distance;
			if(hoverError>=-0.1f && hoverError<=0.1f){
				hoverError=0;
			}

			characterVel += -Vector3.up * hoverError*5f;


			if(characterVel.y>=0){
				characterVel.y=0;
				character.transform.position += -Vector3.up *hoverError * 0.1f;
				falling=false;
			}
			else{
				character.transform.position += characterVel * 0.05f;
				falling=true;
			}
		}
	}

	void velMax(float max, float zxMax){
		if(characterVel.magnitude > max){
			characterVel = characterVel.normalized* max;
		}

		if(characterVel.x*characterVel.x+characterVel.z*characterVel.z >= zxMax){
			characterVel = characterVel.normalized* zxMax;
		}
	}







	bool atNinties(float value){
		if (value*value!=360*360 && value*value!=270*270 && value*value!=180*180 && value*value!=90*90 && value!=0){
			return false;
		}
		else {
			return true;
		}
	}


	void rotateSpace (){


						if (Input.GetButton ("Fire2") && rotating == false) {
								rotating = true;
					
								down = false;
								right = false;
								left = false;
								up = false;
								forward = true;
								back = false;
								//			Physics.gravity = new Vector3(-1.0F, 0, 0);
								//			Vector3 currentRot=new Vector3(character.transform.rotation.eulerAngles.x,character.transform.rotation.eulerAngles.y,character.transform.rotation.eulerAngles.z);
								//			character.transform.rotation=Quaternion.Euler(new Vector3(currentRot.x,currentRot.y,-90));
						}
						if (forward == true) {
								if (rotAngle <= 90 && rotating == true) {
										rotAngle++;
										room.transform.RotateAround (character.transform.position, Vector3.forward, 1);
								}
								if (rotAngle > 90) {
										rotating = false;
							
										rotAngle = 0;
								}
						
						}



						if (Input.GetButton ("Fire3") && rotating == false) {
							rotating = true;
							
							down = false;
							right = false;
							left = false;
							up = false;
							forward = false;
							back = true;
							//			Physics.gravity = new Vector3(-1.0F, 0, 0);
							//			Vector3 currentRot=new Vector3(character.transform.rotation.eulerAngles.x,character.transform.rotation.eulerAngles.y,character.transform.rotation.eulerAngles.z);
							//			character.transform.rotation=Quaternion.Euler(new Vector3(currentRot.x,currentRot.y,-90));
						}
						if (back == true) {
							if (rotAngle <= 90 && rotating == true) {
								rotAngle++;
								room.transform.RotateAround (character.transform.position, Vector3.back, 1);
							}
							if (rotAngle > 90) {
								rotating = false;
								
								rotAngle = 0;
							}
							
						}
						



						if (Input.GetButton ("Jump") && rotating == false) {
							rotating = true;
							
							down = false;
							right = false;
							left = true;
							up = false;
							forward = false;
							back = false;
							//			Physics.gravity = new Vector3(-1.0F, 0, 0);
							//			Vector3 currentRot=new Vector3(character.transform.rotation.eulerAngles.x,character.transform.rotation.eulerAngles.y,character.transform.rotation.eulerAngles.z);
							//			character.transform.rotation=Quaternion.Euler(new Vector3(currentRot.x,currentRot.y,-90));
						}
						if (left == true) {
							if (rotAngle <= 90 && rotating == true) {
								rotAngle++;
								room.transform.RotateAround (character.transform.position, Vector3.left, 1);
							}
							if (rotAngle > 90) {
								rotating = false;
								
								rotAngle = 0;
							}
							
						}



						if (Input.GetButton ("Fire1") && rotating == false) {
							rotating = true;
							
							down = false;
							right = true;
							left = false;
							up = false;
							forward = false;
							back = false;
							//			Physics.gravity = new Vector3(-1.0F, 0, 0);
							//			Vector3 currentRot=new Vector3(character.transform.rotation.eulerAngles.x,character.transform.rotation.eulerAngles.y,character.transform.rotation.eulerAngles.z);
							//			character.transform.rotation=Quaternion.Euler(new Vector3(currentRot.x,currentRot.y,-90));
						}
						if (right == true) {
							if (rotAngle <= 90 && rotating == true) {
								rotAngle++;
								room.transform.RotateAround (character.transform.position, Vector3.right, 1);
							}
							if (rotAngle > 90) {
								rotating = false;
								
								rotAngle = 0;
							}
							
						}
				
	}
}
