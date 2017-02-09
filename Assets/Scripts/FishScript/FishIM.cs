﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class FishIM : InputManager {
	public PlayerFish pF;

	protected override void MouseDown (Vector2 mouseWorldPos)
	{
	//	Debug.Log ("mouse down");
		if (pF != null) {
			pF.isMoving = true;
			pF.PlayerMoveTo (mouseWorldPos);
		}
	} 

	protected override void MouseClickedOnObjOfInterest ()
	{

	} 

}
