using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class PlayerBowerBird : BowerBird {
	public Camera playerCam;
	//public GameObject desiredLoc;
	//public Vector2 desiredPos {get {return (Vector2)desiredLoc.transform.position;}}

	void Start(){
		ParentStart ();
		canGetBored = false;
	}


	void Update(){
		Camera.main.gameObject.GetComponent<CameraFollow> ().toFollow = transform;
		Camera.main.gameObject.GetComponent<CameraFollow> ().SetZoom (6 + 1.5f*altitude);
		ParentUpdate ();
		updateAnimator ();
		if (altitude != 0) {
			autoPickup = true;
		}
	}

	//just overwriding parent's functions used for bird AI, to do nothing
	public void returnHome(){}
	public void IdleBoredom(float timeIdle){}
	public void SeekRandomItem(){}
}
