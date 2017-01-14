using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowerBird : BowerBird {
	public Camera playerCam;
	//public GameObject desiredLoc;
	//public Vector2 desiredPos {get {return (Vector2)desiredLoc.transform.position;}}

	void Start(){
		ParentStart ();
		canGetBored = false;
	}


	void Update(){
		
		ParentUpdate ();
	}

	//just overwriding parent's functions used for bird AI, to do nothing
	public void returnHome(){}
	public void IdleBoredom(float timeIdle){}
	public void SeekRandomItem(){}
}
