using UnityEngine;
using System.Collections;

public class stair_level7 : MonoBehaviour {
	private Vector3 state0=new Vector3(-0.05f,-0.105f,-0.08f);
	private Vector3 state1=new Vector3(-0.05f,-0.105f,0.01f);
	private Vector3 state2=new Vector3(-0.05f,-0.105f,-0.04f);//final

	public int state=0;

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
}
