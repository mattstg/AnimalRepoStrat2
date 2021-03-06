﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class PlayerBowerBird : BowerBird {
	public Camera playerCam;
	//public GameObject desiredLoc;
	//public Vector2 desiredPos {get {return (Vector2)desiredLoc.transform.position;}}

	void Start(){
		ParentStart ();
		canGetBored = false;
		isPlayer = true;
	}


	void Update(){
		Camera.main.gameObject.GetComponent<CameraFollow> ().toFollow = transform;
		Camera.main.gameObject.GetComponent<CameraFollow> ().SetZoom (6 + 1.5f*altitude);
		ParentUpdate ();
		updateAnimator ();
		if (altitude != 0) {
			autoPickup = true;
		}

		if (isHolding && isInBower() && altitude == 0) {
			DropItem ();
		}

	}

	//just overwriding parent's functions used for bird AI, to do nothing
	public void returnHome(){}
	public void IdleBoredom(float timeIdle){}
	public void SeekRandomItem(){}

	public bool isInBower(){
		Vector3 posB = bower.transform.position;
		return Vector3.Distance (posB, transform.position) < 4;
	}
}
