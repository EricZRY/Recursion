using UnityEngine;
using System.Collections;

public class main : MonoBehaviour {
	public GameObject character;
	public GameObject room;
	public GameObject innerRoom;
	public GameObject VRcamera;

	public string WinFace;

	private GameObject forwardAim;

	public GameObject controller;

	private bool sceneInit=false;

	private bool rotating=false;
	private bool moving=false;
	private bool falling=false;

	private float pit = 0;
	private float hea = 0;
	private float rol = 0;


	private Vector3 characterVel = new Vector3(0f,0f,0f);

	private int rotAngle;

	private string mode = "ControllerVR";

	public static main instance { get; private set; }

	void Awake(){
		sceneInit=false;
		Controller.getFirstValue = false;
		instance = this;

		if (GameObject.Find ("Controller(Clone)") == null) {
			Instantiate(controller, new Vector3(0, 0, 0), Quaternion.identity) ;
		}
		forwardAim = GameObject.Find ("faim");
	}

	void Start () {
		rotating = false;
		rotAngle = 0;

	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetMouseButtonDown (0)) {
//			Application.LoadLevel(Application.loadedLevel);
//
//		}


		if(mode == "GamePad"){
			//rotateSpace ();
		}
		else if(mode == "ControllerVR"){

			if(Input.GetButtonDown("X") || Input.GetButtonDown("Y") || Input.GetButtonDown("Z"))
			Debug.Log(Input.GetButtonDown("X")+""+Input.GetButtonDown("Y")+""+Input.GetButtonDown("Z"));

			if(sceneInit==true){
				ControllerRot();
			}
			else{
				if(Controller.getFirstValue==true){
				   if(Controller.pitch!=0 || Controller.heading!=0 || Controller.roll!=0 ){
						room.transform.rotation=Quaternion.Euler(new Vector3(Controller.pitch,Controller.heading,Controller.roll));
						//Debug.Log(new Vector3(Controller.pitch,Controller.heading,Controller.roll));
						innerRoom.transform.rotation=Quaternion.Euler(0,0,0);
					
					}
					sceneInit=true;

				}
				
			}
		}
		else if(mode == "keyBoard"){
			KeyBoardRot();
		}


		//Debug.Log (BottomFace());

		if(rotating == false){
			//Debug.Log(BottomFace());
//			block_level7.instance.moveBlock(BottomFace());
//			stair_level7.instance.moveBlock(BottomFace());


			if(BottomFace()==WinFace){
				characterCollid.flagOrientation=true;
			}
			else{
				characterCollid.flagOrientation=false;
			}

		}



	}


	private void KeyBoardRot(){

	}


	private void ControllerRot(){

			if(falling==false ){
				pit=Controller.pitch;
				hea=Controller.heading;
				rol=Controller.roll;

	//			Debug.Log(room.transform.rotation.eulerAngles.x);
	//			Debug.Log(room.transform.rotation.eulerAngles.y);
	//			Debug.Log(room.transform.rotation.eulerAngles.z);

				//Debug.Log(pit);
				//Debug.Log(rol);
				//Debug.Log(hea);
				//Debug.Log(Mathf.Abs( room.transform.rotation.eulerAngles.x-pit)+","+Mathf.Abs( room.transform.rotation.eulerAngles.y-hea)+","+Mathf.Abs( room.transform.rotation.eulerAngles.z-rol));
			}

		if (Application.loadedLevelName != "level1") {
						room.transform.rotation = Quaternion.Lerp (room.transform.rotation, Quaternion.Euler (new Vector3 (pit, hea, rol)), Time.deltaTime * 4);
				}

		

		forwardAim.transform.rotation =
			Quaternion.Euler (0,VRcamera.transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z);



		character.transform.rotation = Quaternion.Euler( 0,0,0);

		Vector3 roomOrientation = room.transform.rotation.eulerAngles;
		roomOrientation.x = StickToNinty (roomOrientation.x,2);
		roomOrientation.y = StickToNinty (roomOrientation.y,2);
		roomOrientation.z = StickToNinty (roomOrientation.z,2);
		room.transform.rotation = Quaternion.Euler( roomOrientation);

		if (atNinties(pit)==true && atNinties(hea)==true && atNinties(rol)==true
		    && (atNinties(roomOrientation.x-pit) == true ) 
		    && (atNinties( roomOrientation.y-hea) == true )
		    && (atNinties(roomOrientation.z-rol) == true )
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
			velMax(7f,0.03f);
			characterHover(2.2f);
			characterMove();

			//Debug.Log(characterVel);
		}
	}


	public string BottomFace(){
		Vector3 roomDownDirection = innerRoom.transform.TransformDirection (Vector3.down);
		Vector3 roomForwardDirection = innerRoom.transform.TransformDirection (Vector3.forward);
		Vector3 roomRightDirection = innerRoom.transform.TransformDirection (Vector3.right);

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
		//trigger moving
		if (Input.GetMouseButtonDown(1) && falling==false){
			moving=true;
		}
		else if(Input.GetMouseButtonUp(1)){
			moving=false;
		}

		//Debug.Log (VRcamera.transform.rotation.eulerAngles.x);
		if (Mathf.Abs (VRcamera.transform.rotation.eulerAngles.x) < 2.5f || Mathf.Abs (360-VRcamera.transform.rotation.eulerAngles.x)  < 2.5f) {
			moving=true;
		}
		else {
			moving=false;
		}


		//show aim
		if(falling==false && rotating==false){
			forwardAim.transform.GetChild(0).gameObject.renderer.material.color=Color.Lerp(forwardAim.transform.GetChild(0).gameObject.renderer.material.color,new Color32(0,255,128,98),Time.deltaTime*4);
			if(GameObject.Find("level1hint")!=null){
			forwardAim.transform.GetChild(1).gameObject.renderer.material.color=Color.Lerp(forwardAim.transform.GetChild(1).gameObject.renderer.material.color,new Color32(255,255,255,255),Time.deltaTime*4);
			}
		}
		else{
			forwardAim.transform.GetChild(0).gameObject.renderer.material.color=Color.Lerp(forwardAim.transform.GetChild(0).gameObject.renderer.material.color,new Color32(0,255,128,0),Time.deltaTime*4);

			if(GameObject.Find("level1hint")!=null){
				GameObject.Find("level1hint").renderer.material.color=Color.Lerp(forwardAim.transform.GetChild(1).gameObject.renderer.material.color,new Color32(255,255,255,0),Time.deltaTime*4);
			}
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
