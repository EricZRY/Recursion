using UnityEngine;
using System.Collections;

public class block_level6 : MonoBehaviour {
	private Vector3 state0=new Vector3(-0.1236f,0.0295f,0.15f);
	private Vector3 state1=new Vector3(-0.05f,0.0295f,0.15f);

	private float vel=0;
	public float state=0;


	public static block_level6 instance { get; private set; }
	
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
		if(state==0 || state==0.5f){
			if(bottomFace=="RIGHT"){
				if((transform.localPosition.x)<-0.05f){
					blockMovingAccel();
					state=0.5f;
				}
				else{
					bounceAndStop( state1, 1 );
				}
			
				transform.position+=transform.TransformDirection(Vector3.right) * vel;

			}
		}
		if(state==1 || state==0.5f){

			if(bottomFace=="LEFT" ){
				if((transform.localPosition.x)>-0.1236F){
					blockMovingAccel();
					state=0.5f;
				}
				else{
					bounceAndStop( state0, 0 );
				}

				transform.position+=transform.TransformDirection(Vector3.left) * vel;

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
