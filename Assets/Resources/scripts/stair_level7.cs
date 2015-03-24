using UnityEngine;
using System.Collections;

public class stair_level7 : MonoBehaviour {
	private Vector3 state0=new Vector3(-0.05f,-0.105f,0.1f);
	private Vector3 state1=new Vector3(-0.05f,-0.105f,-0.08f);
	private Vector3 state2=new Vector3(-0.05f,-0.105f,-0.04f);//final

	public float state=0;

	private float vel=0;
	


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
		moveBlock(main.instance.BottomFace());
	}


	public  void moveBlock(string bottomFace){
		if(state==0 || state==2 ||state ==0.5f || state==1.5f){
			if(bottomFace=="UP"){
				//block on the way, move to 2
				if(block_level7.instance.state==3.5f || block_level7.instance.state==3){

					if(state!=2){
						if((transform.localPosition.z)>-0.04f){
							blockMovingAccel();
							state=0.5f;
						}
						else{
							bounceAndStop( state2, 2 );
						}
						
						transform.position+=transform.TransformDirection(Vector3.back) * vel;
					}
				}
				else{
					if((transform.localPosition.z)>-0.08f){
						blockMovingAccel();
						if(transform.localPosition.z<=-0.04){
							state=1.5f;
						}
						else {
							state=0.5f;
						}
					}
					else{
						bounceAndStop( state1, 1 );
					}
					
					transform.position+=transform.TransformDirection(Vector3.back) * vel;

				}
			}

		}
		if(state==1 || state==2 || state==0.5f || state==1.5f){
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
