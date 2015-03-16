using UnityEngine;
using System.Collections;

public class block_level7 : MonoBehaviour {
	private Vector3 state0=new Vector3(0.114f,0.06f,0.063f);
	private Vector3 state1=new Vector3(0.114f,0.06f,-0.075f);
	private Vector3 state2=new Vector3(0.114f,-0.114f,-0.075f);
	private Vector3 state3=new Vector3(-0.01f,-0.114f,-0.075f);
	private Vector3 state4=new Vector3(0.035f,-0.114f,-0.075f); 

	private float vel=0;
	private float temp;

	private bool bounced= false;

	public static int state=0;


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
					if(vel<2f){
						vel+=0.05f;
					}
					else{
						vel=0.7f;
					}
				}
				else{
					if(bounced==true){
						transform.localPosition=state1;
						state=1;
						vel=0;
						bounced=false;
					}
					else{
						vel=-vel*0.5f;
						bounced=true;
					}
				}
			
				transform.position+=transform.TransformDirection(Vector3.back) * vel;

			}
		}
	}

}
