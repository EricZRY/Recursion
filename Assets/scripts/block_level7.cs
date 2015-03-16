using UnityEngine;
using System.Collections;

public class block_level7 : MonoBehaviour {
	private Vector3 state0=new Vector3(0.114f,0.06f,0.063f);
	private Vector3 state1=new Vector3(0.114f,0.06f,-0.075f);
	private Vector3 state2=new Vector3(0.114f,-0.114f,-0.075f);
	private Vector3 state3=new Vector3(-0.021f,-0.114f,-0.075f);
	private Vector3 state4=new Vector3(0.035f,-0.114f,-0.075f); 

	private float vel=0;
	private float temp;

	private bool bounced= false;

	public int state=0;


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

	}

	public  void moveBlock(string bottomFace){
		if(state==0){
			if(bottomFace=="UP"){
				if((transform.localPosition.z)>-0.075f){
					blockMovingAccel();
				}
				else{
					bounceAndStop( state1, 1 );
				}
			
				transform.position+=transform.TransformDirection(Vector3.back) * vel;

			}
		}
		else if(state==1){
			if(bottomFace=="DOWN"){
				if((transform.localPosition.z)<0.063f){
					blockMovingAccel();
				}
				else{
					bounceAndStop( state0, 0 );
				}

				transform.position+=transform.TransformDirection(Vector3.forward) * vel;

			}
			else if(bottomFace=="LEFT"){
				if(transform.localPosition.y>-0.114f){
					blockMovingAccel();
				}
				else{
					bounceAndStop(state2,2);
				}
				transform.position+=transform.TransformDirection(Vector3.down) * vel;
			}
		}
		else if (state==2){
			if(bottomFace=="FORWARD"){
				//stair at the original position
				if(stair_level7.instance.state==0){
					if(transform.localPosition.x>-0.021f){
						blockMovingAccel();
					}
					else{
						bounceAndStop(state3,3);
					}
					transform.position+=transform.TransformDirection(Vector3.left) * vel;
				}
				//stair on the way
				else if(stair_level7.instance.state==1){
					if(transform.localPosition.x>0.035f){
						blockMovingAccel();
					}
					else{
						bounceAndStop(state4,4);
					}
					transform.position+=transform.TransformDirection(Vector3.left) * vel;
				}
			}
			if(bottomFace=="RIGHT"){
				if(transform.localPosition.y<0.06f){
					blockMovingAccel();
				}
				else{
					bounceAndStop(state1,1);
				}
				transform.position+=transform.TransformDirection(Vector3.up) * vel;
			}
		}
		else if(state==3 ||state==4){
			if(bottomFace=="BACK"){
				if(transform.localPosition.x<0.114f){
					blockMovingAccel();
				}
				else{
					bounceAndStop(state2,2);
				}
				transform.position+=transform.TransformDirection(Vector3.right) * vel;
			}
		}
	}



	private void blockMovingAccel(){
		main.BlockMoving=true;
		if(vel<2f){
			vel+=0.05f;
		}
		else{
			vel=0.7f;
		}
	}

	private void bounceAndStop( Vector3 endState, int endStateIndex ){
		if(bounced==true){
			transform.localPosition=endState;
			state=endStateIndex;
			vel=0;
			bounced=false;
			main.BlockMoving=false;
		}
		else{
			vel=-vel*0.5f;
			transform.localPosition=endState;

			bounced=true;
		}
	}
}
