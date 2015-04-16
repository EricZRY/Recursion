using UnityEngine;
using System.Collections;

public class DONDIE : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (gameObject);
	}

}
