using UnityEngine;
using System.Collections;

public class selectLevel : MonoBehaviour {
	private GameObject VRcamera;
	private GameObject[] levels=new GameObject[8];
	private GameObject levelHolder;

	private Vector3 holderPos=new Vector3(0,0,0);

	private bool moveRight=false;
	private bool moveLeft=false;
	private bool moved = false;

	private Animator rightAni;
	private Animator leftAni;


	public static int MaxDepth=-1;

	// Use this for initialization
	void Start () {
		VRcamera = GameObject.Find ("CenterEyeAnchor");
		rightAni=GameObject.Find("arrowlowright").GetComponent<Animator>();
		leftAni=GameObject.Find("arrowlowleft").GetComponent<Animator>();
		levelHolder = GameObject.Find ("levels");

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

					if(levels[i].transform.position.x>-1 && levels[i].transform.position.x<1){
						if(GameObject.Find("bar(Clone)")==null){
							Instantiate((GameObject)Resources.Load("prefabs/bar"),levels[i].transform.position+Vector3.down*1.8f,Quaternion.identity);
							GameObject.Find("bar(Clone)").transform.localScale=new Vector3(0.7f,0.7f,0.7f);
						}
						else{
							GameObject mask = GameObject.Find("mask");
							mask.transform.localScale*=0.95f;

							if(mask.transform.localScale.x<0.05f){
								if(i==0){
									Application.LoadLevel("clibrate");

								}
								else{
									Application.LoadLevel("level"+i);
								}
							}
						}
					}
				}
			}
		}
		else{
			moved=false;
			if(GameObject.Find("bar(Clone)")!=null){
				Destroy(GameObject.Find("bar(Clone)"));
			}

		}

		if (moveRight==true) {
			//text move right
			holderPos+=Vector3.right*2;
			moveRight=false;
		}
		else if(moveLeft==true){
			holderPos-=Vector3.right*2;
			moveLeft=false;
		}




		for (int i=0; i<8; i++) {
			if(levels[i].transform.position.x<=-6){
				float error=levels[i].transform.position.x+6;
				levels[i].transform.position=new Vector3(10+error,0,0);
			}
			else if(levels[i].transform.position.x>=12){
				float error=levels[i].transform.position.x-12;
				levels[i].transform.position=new Vector3(-4+error,0,0);
			}

			if(levels[i].transform.position.x>-2 && levels[i].transform.position.x<2){
				if(MaxDepth!=i && levels[i].transform.position.x>-1 && levels[i].transform.position.x<1){
				
						MaxDepth=i;
						Destroy(GameObject.Find("fractal(Clone)"));
							
						GameObject fractal=(GameObject)Resources.Load("prefabs/fractal");
						Instantiate(fractal, new Vector3(0,0,0), Quaternion.identity) ;



				}
				levels[i].transform.GetChild(0).gameObject.renderer.material.color=
					Color.Lerp(new Color32(67,79,73,255),new Color32(60,54,59,255), Mathf.Abs(levels[i].transform.position.x)/2);
			}
		}
		levelHolder.transform.position=Vector3.Lerp(levelHolder.transform.position,holderPos,Time.deltaTime*4 );

		
	}
}
