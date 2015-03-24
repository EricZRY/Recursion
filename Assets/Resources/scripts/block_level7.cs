using UnityEngine;
using System.Collections;

public class block_level7 : MonoBehaviour {
	private Vector3 state0=new Vector3(0.114f,0.06f,0.063f);
	private Vector3 state1=new Vector3(0.114f,0.06f,-0.075f);
	private Vector3 state2=new Vector3(0.114f,-0.114f,-0.075f);
	private Vector3 state3=new Vector3(-0.021f,-0.114f,-0.075f);
	private Vector3 state4=new Vector3(0.035f,-0.114f,-0.075f); 

	private float vel=0;


	public float state=0;


	public static block_level7 instance { get; private set; }
	
	//When the object awakens, we assign the static variable
	void Awake() 
	{
		instance = this;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (transform.localPosition);
		moveBlock(main.instance.BottomFace());
		//Debug.Log (state);
	}

	public  void moveBlock(string bottomFace){
		if(state==0 || state == 0.5f){
			if(bottomFace=="UP"){
				if((transform.localPosition.z)>-0.075f){
					blockMovingAccel();
					state=0.5f;
				}
				else{
					bounceAndStop( state1, 1 );
				}
			
				transform.position+=transform.TransformDirection(Vector3.back) * vel;

			}
		}
		if(state==1 || state ==0.5f || state ==1.5f){

			if(bottomFace=="DOWN" && (state ==1 || state ==0.5f)){
				if((transform.localPosition.z)<0.063f){
					blockMovingAccel();
					state=0.5f;
				}
				else{
					bounceAndStop( state0, 0 );
				}

				transform.position+=transform.TransformDirection(Vector3.forward) * vel;

			}
			else if(bottomFace=="LEFT" && (state ==1 || state ==1.5f)){
				if(transform.localPosition.y>-0.114f){
					blockMovingAccel();
					state=1.5f;
				}
				else{
					bounceAndStop(state2,2);
				}
				transform.position+=transform.TransformDirection(Vector3.down) * vel;
			}
		}
		if (state==2|| state ==2.5f || state ==3.5f || state ==3f || state ==1.5f){
			if(bottomFace=="FORWARD"  &&  (state ==2|| state ==3f  || state ==2.5f || state ==3.5f)){

				//stair on the way
				if(stair_level7.instance.state==1  ||  stair_level7.instance.state==1.5f){
					if(transform.localPosition.x>0.035f){
						blockMovingAccel();
						state=2.5f;
					}
					else{
						bounceAndStop(state4,4);
					}
					transform.position+=transform.TransformDirection(Vector3.left) * vel;
				}
				//stair at the original position
				else{
					if(transform.localPosition.x>-0.021f){
						blockMovingAccel();
						if(transform.localPosition.x<=0.035f){
							state=3.5f;
						}
						else{
							state=2.5f;
						}
					}
					else{
						bounceAndStop(state3,3);
					}
					transform.position+=transform.TransformDirection(Vector3.left) * vel;
				}

			}
			if(bottomFace=="RIGHT" &&  (state ==2 || state ==1.5f )){
				if(transform.localPosition.y<0.06f){
					blockMovingAccel();
					state=1.5f;
				}
				else{
					bounceAndStop(state1,1);
				}
				transform.position+=transform.TransformDirection(Vector3.up) * vel;
			}
		}
		if(state==3 ||state==4 ||state==3.5f ||state==2.5f ){
			if(bottomFace=="BACK"){
				if(transform.localPosition.x<0.114f){
					blockMovingAccel();
					if(transform.localPosition.x<=0.035f){
						state=3.5f;
					}
					else{
						state=2.5f;
					}
				}
				else{
					bounceAndStop(state2,2);
				}
				transform.position+=transform.TransformDirection(Vector3.right) * vel;
			}
		}
	}



	private void blockMovingAccel(){
		if(vel<0.7f){
			vel+=0.05f;
		}
		else{
			vel=0.7f;
		}
	}

	private void bounceAndStop( Vector3 endState, int endStateIndex ){
			transform.localPosition=endState;
			state=endStateIndex;
			vel=0;
	

	}
}
