using UnityEngine;
using System.Collections;

public class splashMain : MonoBehaviour {
	public GameObject controller;
	private GameObject menu;
	private int menuIndex;

	void Awake(){

		if (menu == null) {
			menu=GameObject.Find("MENUS");
		}

		if (GameObject.Find ("Controller(Clone)") == null) {
			Instantiate(controller, new Vector3(0, 0, 0), Quaternion.identity) ;
		}
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
