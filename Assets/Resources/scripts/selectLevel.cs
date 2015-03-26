using UnityEngine;
using System.Collections;

public class selectLevel : MonoBehaviour {
	private GameObject VRcamera;
	private GameObject[] levels=new GameObject[8];

	private bool moveRight=false;
	private bool moveLeft=false;
	private bool moved = false;

	private Animator rightAni;
	private Animator leftAni;

	// Use this for initialization
	void Start () {
		VRcamera = GameObject.Find ("CenterEyeAnchor");
		rightAni=GameObject.Find("arrowlowright").GetComponent<Animator>();
		leftAni=GameObject.Find("arrowlowleft").GetComponent<Animator>();

		for (int i=0; i<8; i++) {
			levels[i]=GameObject.Find("l"+i);
		}


	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast (VRcamera.transform.position, VRcamera.transform.TransformDirection (Vector3.forward), out hit)) {
			if(hit.collider.gameObject==GameObject.Find("right") && moved ==false){
				moveRight=true;
				rightAni.SetTrigger("gazed");
				moved=true;
			}
			else if(hit.collider.gameObject==GameObject.Find("left")&& moved ==false){
				moveLeft=true;
				leftAni.SetTrigger("gazed");
				moved=true;
			}

			for (int i=0; i<8; i++) {
				if (hit.collider.gameObject==levels[i]){
					moved=false;
				}
			
			}
		}
		else{
			moved=false;
		}

		if (moveRight) {
			//text move right
		}


	}
}
