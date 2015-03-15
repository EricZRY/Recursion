using UnityEngine;
using System.Collections;

public class mian : MonoBehaviour {
	public GameObject character;
	public GameObject room;
	public GameObject VRcamera;

	private bool rotating=false;
	private bool moving=false;
	private bool falling=false;
	private bool BlockMoving = false;


	private Vector3 characterVel = new Vector3(0f,0f,0f);

	private int rotAngle;

	private string mode = "ControllerVR";


	void Start () {
		rotating = false;
		rotAngle = 0;


	}
	
	// Update is called once per frame
	void Update () {
		if(mode == "GamePad"){
			//rotateSpace ();
		}
		else if(mode == "ControllerVR"){
			ControllerRot();
		}
		else if(mode == "keyBoard"){
			KeyBoardRot();
		}


		//Debug.Log (BottomFace());

		if(rotating == false){
			block_level7.instance.moveBlock(BottomFace());
		}

	}


	private void KeyBoardRot(){

	}


	private void ControllerRot(){

		if(falling==false){
			room.transform.rotation=Quaternion.Lerp( room.transform.rotation,Quaternion.Euler( new Vector3(Controller.pitch,Controller.heading,Controller.roll)), Time.deltaTime*4);
		}
		character.transform.rotation = Quaternion.Euler( 0,0,0);

		Vector3 roomOrientation = room.transform.rotation.eulerAngles;
	
		if (atNinties(Controller.pitch)==true && atNinties(Controller.heading)==true && atNinties(Controller.roll)==true
		    && (Mathf.Abs( roomOrientation.x-Controller.pitch) <0.01f) && (Mathf.Abs( roomOrientation.y-Controller.heading) <0.01f)&& (Mathf.Abs( roomOrientation.z-Controller.roll) <0.1f)
		    ){
			rotating=false;

		}
		else {
			rotating=true;

		}

//		Debug.Log (room.transform.rotation.x);
//		Debug.Log (room.transform.rotation.y);
//		Debug.Log (room.transform.rotation.z);
//		Debug.Log (falling);

		if(rotating==false){
			velMax(7f,0.1f);
			characterHover(2.2f);
			characterMove();

			//Debug.Log(characterVel);
		}
	}


	private string BottomFace(){
		Vector3 roomDownDirection = room.transform.TransformDirection (Vector3.down);
		Vector3 roomForwardDirection = room.transform.TransformDirection (Vector3.forward);
		Vector3 roomRightDirection = room.transform.TransformDirection (Vector3.right);

		if(roomDownDirection == new Vector3(0,-1,0)){
			return "DOWN";
		}
		else if( -roomDownDirection == new Vector3(0,-1,0)){
			return "UP";
		}
		else if( roomForwardDirection == new Vector3(0,-1,0)){
			return "FORWARD";
		}
		else if( -roomForwardDirection == new Vector3(0,-1,0)){
			return "BACK";
		}
		else if( roomRightDirection == new Vector3(0,-1,0)){
			return "RIGHT";
		}
		else if( -roomRightDirection == new Vector3(0,-1,0)){
			return "LEFT";
		}
		else{
			return "ERROR";
		}
	}



	private void characterMove(){
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
				if(hit.distance<0.7f){
					moving=false;
				}
				else{
					characterVel+= fowardXZ*0.03f;
					character.transform.position+=characterVel;
				}
			}

		}

		if(moving==false){
			characterVel.x=0;
			characterVel.z=0;
		}


	}

	private void characterHover(float hoverHeight){
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

	private void velMax(float max, float zxMax){
		if(characterVel.magnitude > max){
			characterVel = characterVel.normalized* max;
		}

		if(characterVel.x*characterVel.x+characterVel.z*characterVel.z >= zxMax){
			//characterVel = characterVel.normalized* zxMax;

			float times = Mathf.Sqrt( zxMax) / Mathf.Sqrt(characterVel.x*characterVel.x+characterVel.z*characterVel.z);
			characterVel.x = characterVel.x * times;
			characterVel.z = characterVel.z * times;
		}
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


//	void rotateSpace (){
//						if (Input.GetButton ("Fire2") && rotating == false) {
//								rotating = true;
//					
//								down = false;
//								right = false;
//								left = false;
//								up = false;
//								forward = true;
//								back = false;
//								//			Physics.gravity = new Vector3(-1.0F, 0, 0);
//								//			Vector3 currentRot=new Vector3(character.transform.rotation.eulerAngles.x,character.transform.rotation.eulerAngles.y,character.transform.rotation.eulerAngles.z);
//								//			character.transform.rotation=Quaternion.Euler(new Vector3(currentRot.x,currentRot.y,-90));
//						}
//						if (forward == true) {
//								if (rotAngle <= 90 && rotating == true) {
//										rotAngle++;
//										room.transform.RotateAround (character.transform.position, Vector3.forward, 1);
//								}
//								if (rotAngle > 90) {
//										rotating = false;
//							
//										rotAngle = 0;
//								}
//						
//						}
//
//
//
//						if (Input.GetButton ("Fire3") && rotating == false) {
//							rotating = true;
//							
//							down = false;
//							right = false;
//							left = false;
//							up = false;
//							forward = false;
//							back = true;
//							//			Physics.gravity = new Vector3(-1.0F, 0, 0);
//							//			Vector3 currentRot=new Vector3(character.transform.rotation.eulerAngles.x,character.transform.rotation.eulerAngles.y,character.transform.rotation.eulerAngles.z);
//							//			character.transform.rotation=Quaternion.Euler(new Vector3(currentRot.x,currentRot.y,-90));
//						}
//						if (back == true) {
//							if (rotAngle <= 90 && rotating == true) {
//								rotAngle++;
//								room.transform.RotateAround (character.transform.position, Vector3.back, 1);
//							}
//							if (rotAngle > 90) {
//								rotating = false;
//								
//								rotAngle = 0;
//							}
//							
//						}
//						
//
//
//
//						if (Input.GetButton ("Jump") && rotating == false) {
//							rotating = true;
//							
//							down = false;
//							right = false;
//							left = true;
//							up = false;
//							forward = false;
//							back = false;
//							//			Physics.gravity = new Vector3(-1.0F, 0, 0);
//							//			Vector3 currentRot=new Vector3(character.transform.rotation.eulerAngles.x,character.transform.rotation.eulerAngles.y,character.transform.rotation.eulerAngles.z);
//							//			character.transform.rotation=Quaternion.Euler(new Vector3(currentRot.x,currentRot.y,-90));
//						}
//						if (left == true) {
//							if (rotAngle <= 90 && rotating == true) {
//								rotAngle++;
//								room.transform.RotateAround (character.transform.position, Vector3.left, 1);
//							}
//							if (rotAngle > 90) {
//								rotating = false;
//								
//								rotAngle = 0;
//							}
//							
//						}
//
//
//
//						if (Input.GetButton ("Fire1") && rotating == false) {
//							rotating = true;
//							
//							down = false;
//							right = true;
//							left = false;
//							up = false;
//							forward = false;
//							back = false;
//							//			Physics.gravity = new Vector3(-1.0F, 0, 0);
//							//			Vector3 currentRot=new Vector3(character.transform.rotation.eulerAngles.x,character.transform.rotation.eulerAngles.y,character.transform.rotation.eulerAngles.z);
//							//			character.transform.rotation=Quaternion.Euler(new Vector3(currentRot.x,currentRot.y,-90));
//						}
//						if (right == true) {
//							if (rotAngle <= 90 && rotating == true) {
//								rotAngle++;
//								room.transform.RotateAround (character.transform.position, Vector3.right, 1);
//							}
//							if (rotAngle > 90) {
//								rotating = false;
//								
//								rotAngle = 0;
//							}
//							
//						}
//				
//	}
}
