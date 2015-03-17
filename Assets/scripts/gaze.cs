using UnityEngine;
using System.Collections;

public class gaze : MonoBehaviour {
	private Transform temp; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Ray gazeRay = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
		RaycastHit hit;
		if(Physics.Raycast(gazeRay, out hit)) {
			if(hit.collider.tag=="restart"  ){

				if(temp==null){
					temp=hit.collider.transform;
				}
				temp.localScale=new Vector3(1.2f,1.2f,1.2f);
				if( Input.GetMouseButtonDown(1)){
					Application.LoadLevel("level7");
				}
			}
			else{
				if(temp!=null){
					temp.localScale=new Vector3(1f,1f,1f);
				}
			}
		}
		else{
			if(temp!=null){
				temp.localScale=new Vector3(1f,1f,1f);
			}
		}
	}
}
