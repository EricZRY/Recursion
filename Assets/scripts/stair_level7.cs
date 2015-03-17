using UnityEngine;
using System.Collections;

public class stair_level7 : MonoBehaviour {
	private Vector3 state0=new Vector3(-0.05f,-0.105f,0.1f);
	private Vector3 state1=new Vector3(-0.05f,-0.105f,-0.08f);
	private Vector3 state2=new Vector3(-0.05f,-0.105f,-0.04f);//final

	public int state=0;

	private float vel=0;
	
	private bool bounced= false;
	

	public static stair_level7  instance { get; private set; }
	
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
	
	}


	public  void moveBlock(string bottomFace){
		if(state==0){
			if(bottomFace=="UP"){
				//block on the way, move to 2
				if(block_level7.instance.state==3){

					if((transform.localPosition.z)>-0.04f){
						blockMovingAccel();
					}
					else{
						bounceAndStop( state2, 2 );
					}
					
					transform.position+=transform.TransformDirection(Vector3.back) * vel;
					
				}
				else{
					if((transform.localPosition.z)>-0.08f){
						blockMovingAccel();
					}
					else{
						bounceAndStop( state1, 1 );
					}
					
					transform.position+=transform.TransformDirection(Vector3.back) * vel;

				}
			}

		}
		else if(state==1 || state==2){
			if(bottomFace=="DOWN"){

				if((transform.localPosition.z)<0.1f){
					blockMovingAccel();
				}
				else{
					bounceAndStop( state0, 0 );
				}
				
				transform.position+=transform.TransformDirection(Vector3.forward) * vel;
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
