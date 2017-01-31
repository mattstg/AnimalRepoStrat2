using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFish : Fish {

	// Use this for initialization
	void  Start () {
		GetComponent<Rigidbody2D> ().freezeRotation = true;
		followAi = false;
	}
	
	// Update is called once per frame
	void Update () {
		FishUpdate ();
	}


}
